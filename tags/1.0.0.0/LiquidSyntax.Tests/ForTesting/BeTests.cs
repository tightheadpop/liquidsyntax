using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests.ForTesting {
    [TestFixture]
    public class BeTests {
        [Test]
        public void ShouldExtendIsStatically() {
            Assert.IsTrue(typeof(Is).IsAssignableFrom(typeof(Be)));
        }

        [Test]
        public void ShouldSupportEquivalentToSyntaxForParamArrays() {
            new[] {7, 8, 9}.Should(Be.EquivalentTo(9, 7, 8));
            try {
                new[] {7, 8, 9}.ShouldNot(Be.EquivalentTo(9, 7, 8));
                Assert.Fail();
            }
            catch (AssertionException) {}
        }

        [Test]
        public void ShouldProvideGenericFormOfInstanceOfTypeConstraint() {
            7.Should(Be.InstanceOf<int>());
            try {
                7.ShouldNot(Be.InstanceOf<int>());
                Assert.Fail();
            }
            catch (AssertionException) {}
        }
    }
}