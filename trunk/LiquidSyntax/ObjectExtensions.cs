using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace LiquidSyntax {
    /// <summary>
    /// Function bucket providing quick access to System.Reflection actions
    /// </summary>
    public static class ObjectExtensions {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        /// <summary>
        /// Converts input to specified Enum type
        /// </summary>
        /// <typeparam name="T">Enum or value type</typeparam>
        /// <param name="value">value to convert to T</param>
        public static T As<T>(this string value) where T : struct {
            if (typeof(T).IsEnum)
                return (T) Enum.Parse(typeof(T), value);
            return (T) Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Converts input to specified Enum type
        /// </summary>
        /// <typeparam name="T">Enum or value type</typeparam>
        /// <param name="value">value to convert to T</param>
        public static T As<T>(this object value) where T : struct {
            return Convert.ToString(value).As<T>();
        }

        /// <summary>
        /// Converts input to specified Enum value
        /// </summary>
        /// <param name="value">input to convert</param>
        /// <param name="enumerationType">the enumeration defining the desired value</param>
        /// <returns>a single value defined in given enumeration</returns>
        private static object ConvertToEnum(object value, Type enumerationType) {
            if (value.GetType().IsInstanceOfType(enumerationType))
                return value;

            object enumerationValue = null;
            if (value is string) {
                enumerationValue = Enum.Parse(enumerationType, (string) value, true);
            } else if (value is int) {
                enumerationValue = Enum.Parse(enumerationType, Enum.GetName(enumerationType, value), true);
            }
            return enumerationValue;
        }

        public static bool FieldExists(this object obj, string fieldName) {
            return obj.GetField(fieldName) != null;
        }

        public static bool FieldExists(this Type domainObjectType, string fieldName) {
            return GetField(domainObjectType, fieldName) != null;
        }

        public static Type GetCrossAssemblyType(this string assemblyName, string fullName) {
            return assemblyName.GetCrossAssemblyType(fullName, false);
        }

        public static Type GetCrossAssemblyType(this string assemblyName, string fullName, bool throwExceptionOnFailure) {
            var assembly = Assembly.Load(assemblyName);
            return assembly.GetType(fullName, throwExceptionOnFailure, true);
        }

        public static string GetDisplayName(this object obj) {
            return obj.GetType().GetDisplayName();
        }

        public static string GetDisplayName(this Type type) {
            var attribute = (DisplayNameAttribute) type.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
            return attribute != null ? attribute.DisplayName : null;
        }

        public static FieldInfo GetField(this Type domainObjectType, string fieldName) {
            FieldInfo field = null;
            var type = domainObjectType;
            while (field == null) {
                field = type.GetField(fieldName, Flags);
                if (type.BaseType.Name == "Object") {
                    break;
                }
                type = type.BaseType;
            }
            return field;
        }

        public static FieldInfo GetField(this object obj, string fieldName) {
            return GetField(obj.GetType(), fieldName);
        }

        public static FieldInfo[] GetFields(this object obj) {
            return obj.GetType().GetFields(Flags);
        }

        public static FieldInfo[] GetFields(this Type type) {
            return type.GetFields(Flags);
        }

        /// <summary>
        /// Gets the value of a field from a given object
        /// </summary>
        /// <param name="obj">the object to examine</param>
        /// <param name="fieldName">the field to query</param>
        /// <returns>the value of <c>fieldName</c> on <c>obj</c></returns>
        public static object GetFieldValue(this object obj, string fieldName) {
            return obj.GetField(fieldName).GetValue(obj);
        }

        public static PropertyInfo[] GetProperties(this object obj) {
            return obj.GetType().GetProperties(Flags);
        }

        public static PropertyInfo GetProperty(this object obj, string propertyName) {
            return obj.GetProperty(propertyName, Flags);
        }

        public static PropertyInfo GetProperty(this object obj, string propertyName, BindingFlags bindingFlags) {
            return obj.GetType().GetProperty(propertyName, bindingFlags);
        }

        public static Type GetPropertyType(this object obj, string propertyName) {
            object value;
            PropertyInfo info;
            TryNavigate(obj, propertyName, out info, out value);
            return info.PropertyType;
        }

        /// <summary>
        /// Gets the value of a property from a given object
        /// </summary>
        /// <param name="obj">the object to examine</param>
        /// <param name="propertyName">the property to query (handles OGNL-ish syntax using
        /// public properties)</param>
        /// <returns>the value of <c>propertyName</c> on <c>obj</c></returns>
        public static object GetPropertyValue(this object obj, string propertyName) {
            object value;
            PropertyInfo unused;
            if (!TryNavigate(obj, propertyName, out unused, out value))
                throw new NotImplementedException("Property does not exist: " + propertyName);
            return value;
        }

        /// <summary>
        /// Gets the value of a property from a given object
        /// </summary>
        /// <param name="obj">the object to examine</param>
        /// <param name="propertyName">the property to query</param>
        /// <param name="flags">BindingFlags to use in finding the property</param>
        /// <returns>the value of <c>propertyName</c> on <c>obj</c></returns>
        public static object GetPropertyValue(this object obj, string propertyName, BindingFlags flags) {
            var property = obj.GetProperty(propertyName, flags);
            return property.GetValue(obj, null);
        }

        public static bool HasAttribute(this PropertyInfo property, Type attributeType) {
            return property.GetCustomAttributes(attributeType, true).Length > 0;
        }

        public static bool HasProperty(this object obj, string propertyName) {
            object value;
            PropertyInfo info;
            return TryNavigate(obj, propertyName, out info, out value);
        }

        /// <summary>
        /// Determines if a given Type implements a specified interface
        /// </summary>
        /// <param name="implementingType">the Type to examine</param>
        /// <returns>true if <c>implementingType</c> implements <c>interfaceType</c></returns>
        public static bool Implements<TInterface>(this Type implementingType) {
            return implementingType.GetInterface(typeof(TInterface).FullName) != null;
        }

        public static object InvokeMethod(this object obj, string methodName) {
            return obj.InvokeMethod(methodName, null);
        }

        public static object InvokeMethod(this object obj, string methodName, object[] parameters) {
            var method = obj.GetType().GetMethod(methodName, Flags);
            return method.Invoke(obj, parameters);
        }

        public static T NullOr<T>(this T t) where T : class {
            var options = new ProxyGenerationOptions(new NonVirtualMemberHatingHook());
            var interceptor = new NullInstanceInterceptor(t);
            return (T) new ProxyGenerator().CreateClassProxy(typeof(T), options, interceptor);
        }

        public static bool PropertyExists(this object obj, string propertyName) {
            return obj.GetProperty(propertyName) != null;
        }

        public static void SetFieldValue(this object obj, string fieldName, object fieldValue) {
            try {
                var field = obj.GetField(fieldName);
                var value = fieldValue;
                if (field.FieldType.IsEnum) {
                    value = ConvertToEnum(value, field.FieldType);
                }
                field.SetValue(obj, value);
            }
            catch (Exception ex) {
                throw new ApplicationException(
                    string.Format("field {0} of object {1} cannot be set with value {2}", fieldName, obj.GetType(), fieldValue), ex);
            }
        }

        /// <summary>
        /// Sets a property's value on an object
        /// </summary>
        /// <param name="obj">the object to operate on</param>
        /// <param name="propertyName">the property to set</param>
        /// <param name="propertyValue">the value to assign to the property</param>
        public static void SetPropertyValue(this object obj, string propertyName, object propertyValue) {
            var property = obj.GetProperty(propertyName);
            var value = propertyValue;
            if (property.PropertyType.IsEnum) {
                value = ConvertToEnum(value, property.PropertyType);
            }
            property.SetValue(obj, value, null);
        }

        private static bool TryNavigate(object obj, string propertyName, out PropertyInfo propertyInfo, out object value) {
            propertyInfo = null;
            value = null;

            var property = obj;
            PropertyInfo info = null;
            foreach (var s in propertyName.Split('.')) {
                if (property == null) return false;
                info = property.GetType().GetProperty(s, Flags);
                if (info == null)
                    return false;
                property = info.GetValue(property, null);
            }
            propertyInfo = info;
            value = property;
            return true;
        }

        private class NullInstanceInterceptor : IInterceptor {
            private object Target { get; set; }

            public NullInstanceInterceptor(object target) {
                Target = target;
            }

            public void Intercept(IInvocation invocation) {
                if (Target == null) {
                    var expectedType = invocation.Method.ReturnType;
                    invocation.ReturnValue = expectedType.IsClass ? null : Activator.CreateInstance(expectedType);
                } else {
                    invocation.Proceed();
                }
            }
        }

        private class NonVirtualMemberHatingHook : IProxyGenerationHook {
            public bool ShouldInterceptMethod(Type type, MethodInfo memberInfo) {
                return true;
            }

            public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo) {
                throw new NotSupportedException("Member {0} on type {1} must be virtual.".Substitute(memberInfo.Name, type.FullName));
            }

            public void MethodsInspected() {
            }
        }
    }
}