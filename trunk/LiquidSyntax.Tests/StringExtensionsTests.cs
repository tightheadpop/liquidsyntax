using System.Text;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

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
            ((string) null).Reversed().Should(Be.Null);
        }

        [Test]
        public void RepeatsString() {
            7.Times("a").Should(Be.EqualTo("aaaaaaa"));
        }

        [Test]
        public void ShouldNotRepeatStringIfCountIsLessThanOrEqualToZero() {
            0.Times("a").Should(Be.EqualTo(string.Empty));
            (-1).Times("a").Should(Be.EqualTo(string.Empty));
        }
        [Test]
        public void Contains() {
            "this test".Contains("test").Should(Be.True);
            "this test".Contains("TEST").Should(Be.False);
            "this test".Contains("foo").Should(Be.False);
        }

        [Test]
        public void ContainsIgnoreCase() {
            "this test is neat".ContainsIgnoreCase("TEST").Should(Be.True);
            "this test is neat".ContainsIgnoreCase("rat").Should(Be.False);
        }

        [Test]
        public void EqualsIgnoreCase() {
            "test".EqualsIgnoreCase("TEST").Should(Be.True);
            "test".EqualsIgnoreCase("rat").Should(Be.False);
        }

        [Test]
        public void ForcePrefix() {
            Assert.AreEqual("testrat", "rat".WithPrefix("test"));
            Assert.AreEqual("testrat", "testrat".WithPrefix("test"));
        }

        [Test]
        public void ForceSuffix() {
            Assert.AreEqual("rattest", "rat".WithSuffix("test"));
            Assert.AreEqual("rattest", "rattest".WithSuffix("test"));
        }

        [Test]
        public void IsEmpty() {
            Assert.IsTrue(((string)null).IsEmpty());
            Assert.IsTrue(string.Empty.IsEmpty());
            Assert.IsFalse("foo".IsEmpty());
        }

        [Test]
        public void IsNumeric() {
            Assert.IsTrue("1".IsNumeric());
            Assert.IsTrue("1,000.00".IsNumeric());
            Assert.IsTrue(" 1 ".IsNumeric());
            Assert.IsTrue("-1".IsNumeric());
            Assert.IsTrue("1.".IsNumeric());
            Assert.IsTrue("1.0".IsNumeric());
            Assert.IsTrue(".0".IsNumeric());
            Assert.IsFalse("1a".IsNumeric());
            Assert.IsFalse("1.0.0".IsNumeric());
            Assert.IsFalse("(1.0)".IsNumeric());
            Assert.IsFalse("$1.0".IsNumeric());
            Assert.IsFalse(string.Empty.IsNumeric());
        }

        [Test]
        public void NullToEmpty() {
            Assert.AreEqual(string.Empty, ((string)null).NullToEmpty());
            Assert.AreEqual(string.Empty, string.Empty.NullToEmpty());
            Assert.AreEqual("test", "test".NullToEmpty());
        }

        [Test]
        public void RemovePrefix() {
            Assert.AreEqual("test", "ratstest".WithoutPrefix("rats"));
            Assert.AreEqual("myratstest", "myratstest".WithoutPrefix("rats"), "should ignore missing prefix");
        }

        [Test]
        public void RemovePrefixByPattern() {
            Assert.AreEqual("test", "ratstest".WithoutPrefixPattern("RAT."));
            Assert.AreEqual("ratstest", "ratstest".WithoutPrefixPattern("test"));
        }

        [Test]
        public void RemoveSuffixByPattern() {
            Assert.AreEqual("test", "testrat".WithoutSuffixPattern("rAt"));
            Assert.AreEqual("testrat", "testrat".WithoutSuffixPattern("rrAt"));
        }

        [Test]
        public void StringAfter() {
            Assert.AreEqual("extension", "file.name.extension".StringAfterLast("."));
            Assert.AreEqual("file.name.extension", "file.name.extension".StringAfterLast(@"\"));
        }

        [Test]
        public void StringAfterFirst() {
            Assert.AreEqual("?foo?bar", "foo?foo?bar".StringAfterFirst("foo"));
            Assert.AreEqual(string.Empty, "foo".StringAfterFirst("foo"));
            Assert.AreEqual("foo?foo?bar", "foo?foo?bar".StringAfterFirst("zoinks"));
        }

        [Test]
        public void StringBeforeFirst() {
            Assert.AreEqual(string.Empty, "foo?foo?bar".StringBeforeFirst("foo"));
            Assert.AreEqual("foo?foo?", "foo?foo?bar".StringBeforeFirst("bar"));
            Assert.AreEqual("foo?foo?bar", "foo?foo?bar".StringBeforeFirst("zoinks"), "should ignore missing token");
        }

        [Test]
        public void StringBeforeLast() {
            Assert.AreEqual("foo?", "foo?foo?bar".StringBeforeLast("foo"));
            Assert.AreEqual("foo?", "foo?foo?bar".StringBeforeLast("foo"));
            Assert.AreEqual("foo?foo?bar", "foo?foo?bar".StringBeforeLast("zoinks"), "should ignore missing token");
        }

        [Test]
        public void TrimToEmpty() {
            Assert.AreEqual(string.Empty, ((string)null).TrimToEmpty());
            Assert.AreEqual(string.Empty, string.Empty.TrimToEmpty());
            Assert.AreEqual("paul", "  paul ".TrimToEmpty());
        }

        [Test]
        public void TrimToNull() {
            Assert.IsNull(((string)null).TrimToNull());
            Assert.IsNull(string.Empty.TrimToNull());
            Assert.IsNull(" ".TrimToNull());
            Assert.AreEqual("v", " v ".TrimToNull());
        }
    }
}