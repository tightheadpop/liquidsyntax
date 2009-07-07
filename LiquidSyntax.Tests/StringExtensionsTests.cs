using System.Text;
using NUnit.Framework;
using LiquidSyntax.ForTesting;
using System.Linq;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class StringExtensionsTests {
        [Test]
        public void ShouldSubstitutePlaceholders() {
            "{0}-{2}-1-{1}".Substitute(new StringBuilder("hi"), 2, "peanut").Should(Be.EqualTo("hi-peanut-1-2"));
        }

        [Test]
        public void CanReverseString() {
            "123".Reversed().Should(Be.EqualTo("321"));
            "".Reversed().Should(Be.EqualTo(""));
        }

        [Test]
        public void DoesNotAttemptToReverseNullString() {
            ((string)null).Reversed().Should(Be.Null);
        }
    }
}