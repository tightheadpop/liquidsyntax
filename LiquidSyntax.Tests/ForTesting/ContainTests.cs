using NUnit.Framework;
using LiquidSyntax.ForTesting;

namespace LiquidSyntax.Tests.ForTesting {
    [TestFixture]
    public class ContainTests {
        
        [Test]
        public void ShouldContainSubString() {
            "Hello I am cool".Should(Contain.Text("Hello"));
        }

        [Test]
        public void ShouldNotContainSubString() {
            "Hello I am cool".ShouldNot(Contain.Text("xxxHello"));
        }
    }
}