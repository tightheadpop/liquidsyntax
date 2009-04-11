using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace LiquidSyntax.ForTesting {
    public static class ShouldExtensions {
        public static void Should(this object o, Constraint constraint) {
            Assert.That(o, constraint);
        }

        public static void Should(this object o, Constraint constraint, string messageTemplate, params object[] messageArgs) {
            Assert.That(o, constraint, messageTemplate, messageArgs);
        }

        public static void ShouldNot(this object o, Constraint constraint) {
            Assert.That(o, new NotConstraint(constraint));
        }

        public static void ShouldNot(this object o, Constraint constraint, string messageTemplate, params object[] messageArgs) {
            Assert.That(o, new NotConstraint(constraint), messageTemplate, messageArgs);
        }
    }
}