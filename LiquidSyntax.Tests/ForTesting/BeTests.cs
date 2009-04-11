using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests.ForTesting {
    [TestFixture]
    public class BeTests {
        [Test]
        public void ShouldExtendIsStatically() {
            ((object) null).Should(Be.Null);
            try {
                ((object) null).ShouldNot(Be.Null);
                Assert.Fail("should have thrown exception");
            }
            catch (AssertionException) {}
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
            7.Should(Be.InstanceOfType<int>());
            try {
                7.ShouldNot(Be.InstanceOfType<int>());
                Assert.Fail();
            }
            catch (AssertionException) {}
        }
    }
}