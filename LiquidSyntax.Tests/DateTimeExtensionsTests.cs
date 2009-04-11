using System;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class DateTimeExtensionsTests {
        [Test]
        public void AfterShouldGiveDateInFuture() {
            2.Days().After(new DateTime(2005, 5, 9)).Should(Be.EqualTo(new DateTime(2005, 5, 11)));
        }

        [Test]
        public void AgoShouldGiveDateInPast() {
            2.Hours().Ago().Should(Be.EqualTo(DateTime.Now.AddHours(-2)).Within(100.Milliseconds()));
        }

        [Test]
        public void BeforeShouldGiveDateInFuture() {
            2.Days().Before(new DateTime(2005, 5, 9)).Should(Be.EqualTo(new DateTime(2005, 5, 7)));
        }

        [Test]
        public void GetFirstDayOfWeek() {
            const int sunday = 4;
            var originalDate = new DateTime(2004, 7, sunday);
            var firstDayOfWeek = originalDate.FirstDayOfWeek();
            Assert.IsTrue(firstDayOfWeek.DayOfWeek == DayOfWeek.Sunday);
            Assert.IsTrue(firstDayOfWeek.Year == originalDate.Year);
            Assert.IsTrue(firstDayOfWeek.Month == originalDate.Month);
            Assert.IsTrue(firstDayOfWeek.Day == originalDate.Day);
            Assert.IsTrue(firstDayOfWeek.DayOfWeek == originalDate.DayOfWeek);

            const int monday = 5;
            originalDate = new DateTime(2004, 7, monday);
            firstDayOfWeek = originalDate.FirstDayOfWeek();
            Assert.IsTrue(firstDayOfWeek.DayOfWeek == DayOfWeek.Sunday);
            Assert.IsTrue(firstDayOfWeek.Year == originalDate.Year);
            Assert.IsTrue(firstDayOfWeek.Month == originalDate.Month);
            Assert.IsTrue(firstDayOfWeek.Day != originalDate.Day);
            Assert.IsTrue(firstDayOfWeek.DayOfWeek != originalDate.DayOfWeek);

            const int thursday = 8;
            originalDate = new DateTime(2004, 7, thursday);
            firstDayOfWeek = originalDate.FirstDayOfWeek();
            Assert.IsTrue(firstDayOfWeek.DayOfWeek == DayOfWeek.Sunday);
            Assert.IsTrue(firstDayOfWeek.Year == originalDate.Year);
            Assert.IsTrue(firstDayOfWeek.Month == originalDate.Month);
            Assert.IsTrue(firstDayOfWeek.Day != originalDate.Day);
            Assert.IsTrue(firstDayOfWeek.DayOfWeek != originalDate.DayOfWeek);
        }

        [Test]
        public void ShouldFindNextDayOfWeek() {
            var expected = new DateTime(2005, 5, 9);

            new DateTime(2005, 5, 2).Next(DayOfWeek.Monday).Should(Be.EqualTo(expected));
            new DateTime(2005, 5, 8).Next(DayOfWeek.Monday).Should(Be.EqualTo(expected));
        }

        [Test]
        public void ShouldFindPreviousDayOfWeek() {
            var expected = new DateTime(2005, 5, 9);

            new DateTime(2005, 5, 12).Previous(DayOfWeek.Monday).Should(Be.EqualTo(expected));
            new DateTime(2005, 5, 16).Previous(DayOfWeek.Monday).Should(Be.EqualTo(expected));
        }

        [Test]
        public void FromNowShouldGiveDateInFuture() {
            2.Seconds().FromNow().Should(Be.EqualTo(DateTime.Now.AddSeconds(2)).Within(50.Milliseconds()));
        }
    }
}