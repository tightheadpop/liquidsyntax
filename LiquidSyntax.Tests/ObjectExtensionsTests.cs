using System;
using System.Collections;
using System.ComponentModel;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class ObjectExtensionsTests {
        [Test]
        public void ConvertToEnum() {
            Assert.AreEqual(TestEnum.Tchotchke, "Tchotchke".As<TestEnum>());
            Assert.AreEqual(TestEnum.Dingsbums, 1.As<TestEnum>());
            Assert.AreEqual(TestEnum.Tchotchke, "Tchotchke".As<TestEnum>());
        }

        [Test]
        public void ConvertToValueType() {
            Assert.AreEqual(true, "True".As<bool>());
            Assert.AreEqual(1, "1".As<int>());
        }

        [Test]
        public void FieldExists() {
            var testObject = new TestObject();
            Assert.IsTrue(testObject.FieldExists("_list"));
            Assert.IsFalse(testObject.FieldExists("_nofieldhere"));
        }

        [Test]
        public void GetCrossAssemblyType() {
            Assert.IsNotNull("nunit.framework".GetCrossAssemblyType("NUnit.Framework.Constraints.NUnitComparer"));
        }

        [Test]
        public void GetFieldValue() {
            new TestObject().GetFieldValue("_list").ShouldNot(Be.Null);
        }

        [Test]
        public void GetPropertyTypeNavigatesObjectGraph() {
            Assert.AreEqual(typeof(string), new TestObject().GetPropertyType("ComplexProperty.StringProperty"));
        }

        [Test]
        public void GetPropertyValueNavigatesObjectGraph() {
            Assert.AreEqual("expected", new TestObject().GetPropertyValue("ComplexProperty.StringProperty"));
        }

        [Test]
        [Ignore("Make it so")]
        public void GetPropertyValueNavigatesObjectGraphUsingPropertiesAndFields() {
            Assert.AreEqual("expected", new TestObject().GetPropertyValue("child.ComplexProperty.StringProperty"));
        }

        [Test]
        [Ignore("Make it so")]
        public void GetPropertyValueRecognizesIndexer() {
            Assert.AreEqual(5, (int) new TestObject().GetPropertyValue("StringArrayProperty[0].Length"));
        }

        [Test]
        public void HasProperty() {
            var obj = new TestObject();
            Assert.IsTrue(obj.HasProperty("List.Count"));
            Assert.IsFalse(obj.HasProperty("Won't have it"));
        }

        [Test]
        public void Implements() {
            Assert.IsTrue(typeof(TestObject).Implements<IDisposable>());
            Assert.IsFalse(typeof(TestObject).Implements<IList>());
        }

        [Test]
        public void InvokeNonPublicMethod() {
            var testObject = new TestObject();

            var result = testObject.InvokeMethod("Set");
            Assert.IsNull(result);
            Assert.IsTrue(testObject.VerifySet);
        }

        [Test]
        public void InvokePublicMethod() {
            var testObject = new TestObject();
            var result = testObject.InvokeMethod("Get");
            Assert.IsTrue(result.GetType().Equals(typeof(string)));
            Assert.AreEqual("set", result);
        }

        [Test]
        public void SetFieldValue() {
            var testObject = new TestObject();

            var innerList = new ArrayList {"Ben"};
            testObject.SetFieldValue("_list", innerList);
            Assert.AreEqual(innerList, testObject.List);
        }

        [Test]
        public void SetFieldValueAsEnum() {
            var testObject = new TestObject();
            testObject.SetFieldValue("_testEnum", "Dingsbums");
            Assert.AreEqual(TestEnum.Dingsbums, testObject.TestEnum);

            testObject.SetFieldValue("_testEnum", TestEnum.Tchotchke);
            Assert.AreEqual(TestEnum.Tchotchke, testObject.TestEnum);

            testObject.SetFieldValue("_testEnum", 1);
            Assert.AreEqual(TestEnum.Dingsbums, testObject.TestEnum);
        }

        [Test]
        public void SetPropertyValue() {
            var testObject = new TestObject();
            testObject.SetPropertyValue("VerifySet", true);
            Assert.AreEqual(true, testObject.VerifySet);
        }

        [Test]
        public void SetPropertyValueAsEnum() {
            var testObject = new TestObject();
            testObject.SetPropertyValue("TestEnum", "Dingsbums");
            Assert.AreEqual(TestEnum.Dingsbums, testObject.TestEnum);

            testObject.SetPropertyValue("TestEnum", TestEnum.Tchotchke);
            Assert.AreEqual(TestEnum.Tchotchke, testObject.TestEnum);

            testObject.SetPropertyValue("TestEnum", 1);
            Assert.AreEqual(TestEnum.Dingsbums, testObject.TestEnum);
        }

        [Test]
        public void ShouldProvideAccessToDisplayNameAttributeForInstances() {
            new TestObject().GetDisplayName().Should(Be.EqualTo("Test instance"));
        }

        [Test]
        public void ShouldProvideAccessToDisplayNameAttributeForTypes() {
            typeof(TestObject).GetDisplayName().Should(Be.EqualTo("Test instance"));
        }

        [Test]
        public void NullOrShouldReturnDefaultValueForPropertyOfNullInstance() {
            TestObject t = null;
            t.NullOr().StringArrayProperty.Should(Be.Null);
            t.NullOr().TestEnum.Should(Be.EqualTo(TestEnum.Tchotchke));
        }

        [Test]
        public void NullOrShouldReturnActualPropertyValueOfNonNullInstance() {
            var t = new TestObject();
            t.NullOr().StringArrayProperty.ShouldNot(Be.Null);
        }

        [Test]
        public void NullOrShouldThrowExceptionForNonVirtualMemberInvocation() {
            var t = new ComplexType();
            try {
                Console.Write(t.NullOr().StringProperty);
                Assert.Fail();
            }
            catch (NotSupportedException) {}
        }

        public class ComplexType {
            public string StringProperty {
                get { return "expected"; }
            }
        }

        public enum TestEnum {
            Tchotchke = 0,
            Dingsbums = 1
        }

        [DisplayName("Test instance")]
        public class TestObject : IDisposable {
            private IList _list = new ArrayList();
            private TestEnum _testEnum = TestEnum.Tchotchke;
            private bool _verifySet;

            public virtual bool Disposed { get; private set; }
            public virtual bool Poked { get; private set; }

            public virtual ComplexType ComplexProperty {
                get { return new ComplexType(); }
            }

            public virtual IList List {
                get { return _list; }
                set { _list = value; }
            }

            public virtual string[] StringArrayProperty {
                get { return new[] {"first", "second"}; }
            }

            public virtual TestEnum TestEnum {
                get { return _testEnum; }
                set { _testEnum = value; }
            }

            public virtual bool VerifySet {
                get { return _verifySet; }
                set { _verifySet = value; }
            }

            public virtual void Dispose() {
                Disposed = true;
            }

            public virtual string Get() {
                return "set";
            }

            public virtual void Poke() {
                Poked = true;
            }

            protected virtual void Set() {
                _verifySet = true;
            }
        }
    }
}