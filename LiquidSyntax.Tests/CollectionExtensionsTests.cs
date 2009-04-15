using System.Collections.Generic;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class CollectionExtensionsTests {
        [Test]
        public void ShouldSortNaturally() {
            new List<int> {3, 4, 2}.Sorted().Should(Be.EqualTo(new[] {2, 3, 4}));
        }

        [Test]
        public void ShouldJoinWithDelimiter() {
            new List<int> {3, 4, 2}.Join(", ").Should(Be.EqualTo("3, 4, 2"));
        }
    }
}