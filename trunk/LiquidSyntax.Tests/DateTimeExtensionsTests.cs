using System;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class DateTimeExtensionsTests {
        [Test]
        public void AfterShouldGiveDateInFuture() {
            2.Days().After(9.May(2005)).Should(Be.EqualTo(11.May(2005)));
        }

        [Test]
        public void AgoShouldGiveDateInPast() {
            2.Hours().Ago().Should(Be.EqualTo(DateTime.Now.AddHours(-2)).Within(100.Milliseconds()));
        }

        [Test]
        public void BeforeShouldGiveDateInFuture() {
            2.Days().Before(9.May(2005)).Should(Be.EqualTo(7.May(2005)));
        }

        [Test]
        public void FirstDayOfWeekShouldBeEqualToGivenDateIfDateIsSunday() {
            4.July(2004).FirstDayOfWeek().Should(Be.EqualTo(4.July(2004)));
        }

        [Test]
        public void FirstDayOfWeekShouldReturnFirstSundayPriorToDate() {
            1.January(2009).FirstDayOfWeek().Should(Be.EqualTo(28.December(2008)));
        }

        [Test]
        public void ShouldFindNextDayOfWeek() {
            2.May(2005).Next(DayOfWeek.Monday).Should(Be.EqualTo(9.May(2005)));
            8.May(2005).Next(DayOfWeek.Monday).Should(Be.EqualTo(9.May(2005)));
        }

        [Test]
        public void ShouldFindPreviousDayOfWeek() {
            12.May(2005).Previous(DayOfWeek.Monday).Should(Be.EqualTo(9.May(2005)));
            16.May(2005).Previous(DayOfWeek.Monday).Should(Be.EqualTo(9.May(2005)));
        }

        [Test]
        public void FromNowShouldGiveDateInFuture() {
            2.Seconds().FromNow().Should(Be.EqualTo(DateTime.Now.AddSeconds(2)).Within(50.Milliseconds()));
        }

        [Test]
        public void ShouldBuildDatesUsingNaturalSyntax() {
            2.January(2000).Should(Be.EqualTo(new DateTime(2000, 1, 2)));
            3.February(2000).Should(Be.EqualTo(new DateTime(2000, 2, 3)));
            9.March(2009).Should(Be.EqualTo(new DateTime(2009, 3, 9)));
            9.April(2009).Should(Be.EqualTo(new DateTime(2009, 4, 9)));
            9.May(2009).Should(Be.EqualTo(new DateTime(2009, 5, 9)));
            9.June(2009).Should(Be.EqualTo(new DateTime(2009, 6, 9)));
            9.July(2009).Should(Be.EqualTo(new DateTime(2009, 7, 9)));
            9.August(2009).Should(Be.EqualTo(new DateTime(2009, 8, 9)));
            9.September(2009).Should(Be.EqualTo(new DateTime(2009, 9, 9)));
            9.October(2009).Should(Be.EqualTo(new DateTime(2009, 10, 9)));
            9.November(2009).Should(Be.EqualTo(new DateTime(2009, 11, 9)));
            9.December(2009).Should(Be.EqualTo(new DateTime(2009, 12, 9)));
        }
    }
}