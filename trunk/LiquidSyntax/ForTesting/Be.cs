using System.Linq;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

namespace LiquidSyntax.ForTesting {
    public class Be : Is {
        public static Constraint EquivalentTo(params object[] items) {
            return Is.EquivalentTo(items.ToList());
        }

        public static Constraint InstanceOfType<T>() {
            return InstanceOfType(typeof(T));
        }
    }
}