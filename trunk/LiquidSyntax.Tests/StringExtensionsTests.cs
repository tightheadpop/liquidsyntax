using System.Text;
using NUnit.Framework;
using LiquidSyntax.ForTesting;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class StringExtensionsTests {
        [Test]
        public void ShouldSubstitutePlaceholders() {
            "{0}-{2}-1-{1}".Substitute(new StringBuilder("hi"), 2, "peanut").Should(Be.EqualTo("hi-peanut-1-2"));
        }
    }
}