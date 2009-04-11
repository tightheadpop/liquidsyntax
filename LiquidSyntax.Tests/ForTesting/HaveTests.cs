using System.Collections.Generic;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests.ForTesting {
    [TestFixture]
    public class HaveTests {
        [Test]
        public void ShouldExtendHasStatically() {
            new List<int> {7, 9}.Should(Have.Count(2));
            try {
                new List<int> {7, 9}.ShouldNot(Have.Count(2));
            }
            catch (AssertionException) {}
        }
    }
}