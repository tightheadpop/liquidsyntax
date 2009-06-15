using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests.ForTesting {
    [TestFixture]
    public class HaveTests {
        [Test]
        public void ShouldExtendHasStatically() {
            typeof(Has).IsAssignableFrom(typeof(Have)).Should(Be.True);
        }
    }
}