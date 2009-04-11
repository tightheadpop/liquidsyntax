using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests.ForTesting {
    [TestFixture]
    public class BeTests {
        [Test]
        public void ShouldExtendIsStatically() {
            ((object) null).Should(Be.Null);
        }

        [Test]
        public void ShouldSupportEquivalentToSyntaxForParamArrays() {
            new[]{7,8,9}.Should(Be.EquivalentTo(9,7,8));
            new[]{7,8,9}.ShouldNot(Be.EquivalentTo(9,7,8,9));
        }

        [Test]
        public void ShouldProvideGenericFormOfInstanceOfTypeConstraint() {
            7.Should(Be.InstanceOfType<int>());
            7.ShouldNot(Be.InstanceOfType<double>());
        }
    }
}