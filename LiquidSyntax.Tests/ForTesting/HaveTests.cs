using System.Collections.Generic;
using NUnit.Framework;
using LiquidSyntax.ForTesting;

namespace LiquidSyntax.Tests.ForTesting {
    [TestFixture]
    public class HaveTests {
        [Test]
        public void ShouldExtendHasStatically() {
            new List<int> {7, 9}.Should(Have.Count(2));
        }
    }
}