using System;

namespace LiquidSyntax {
    public static class DateTimeExtensions {
        public static DateTime At(this DateTime dateTime, int time24Hour) {
            var timeAsString = time24Hour.ToString();
            var lengthOfHourSubstring = timeAsString.Length - 2;
            var hour = Convert.ToInt32(timeAsString.Substring(0, lengthOfHourSubstring));
            var minutes = Convert.ToInt32(timeAsString.Substring(lengthOfHourSubstring));
            return dateTime.AddHours(hour).AddMinutes(minutes);
        }

        public static DateTime At(this DateTime dateTime, string time) {
            var day = dateTime.Date;
            return day + TimeSpan.Parse(time);
        }

        public static DateTime Before(this TimeSpan span, DateTime start) {
            return start.Subtract(span);
        }

        public static DateTime After(this TimeSpan span, DateTime start) {
            return start.Add(span);
        }

        public static DateTime Ago(this TimeSpan span) {
            return DateTime.Now.Subtract(span);
        }

        public static DateTime FromNow(this TimeSpan span) {
            return span.After(DateTime.Now);
        }

        public static TimeSpan Days(this int days) {
            return new TimeSpan(days, 0, 0, 0);
        }

        public static TimeSpan Hours(this int hours) {
            return new TimeSpan(0, hours, 0, 0);
        }

        public static TimeSpan Minutes(this int minutes) {
            return new TimeSpan(0, 0, minutes, 0);
        }

        public static TimeSpan Seconds(this int seconds) {
            return new TimeSpan(0, 0, 0, seconds);
        }

        public static TimeSpan Milliseconds(this int milliseconds) {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }

        public static DateTime FirstDayOfWeek(this DateTime dateTime) {
            return dateTime.AddDays((int) dateTime.DayOfWeek * -1);
        }

        public static DateTime LastDayOfWeek(this DateTime dateTime) {
            return dateTime.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime Next(this DateTime start, DayOfWeek dayOfWeek) {
            if (dayOfWeek > start.DayOfWeek)
                return start.AddDays(dayOfWeek - start.DayOfWeek);
            return 7.Days().After(start).FirstDayOfWeek().AddDays((int) dayOfWeek);
        }

        public static DateTime Previous(this DateTime start, DayOfWeek dayOfWeek) {
            return 8.Days().Before(start).Next(dayOfWeek);
        }

        public static DateTime January(this int dayOfMonth, int year) {
            return new DateTime(year, 1, dayOfMonth);
        }

        public static DateTime February(this int dayOfMonth, int year) {
            return new DateTime(year, 2, dayOfMonth);
        }

        public static DateTime March(this int dayOfMonth, int year) {
            return new DateTime(year, 3, dayOfMonth);
        }

        public static DateTime April(this int dayOfMonth, int year) {
            return new DateTime(year, 4, dayOfMonth);
        }

        public static DateTime May(this int dayOfMonth, int year) {
            return new DateTime(year, 5, dayOfMonth);
        }

        public static DateTime June(this int dayOfMonth, int year) {
            return new DateTime(year, 6, dayOfMonth);
        }

        public static DateTime July(this int dayOfMonth, int year) {
            return new DateTime(year, 7, dayOfMonth);
        }

        public static DateTime August(this int dayOfMonth, int year) {
            return new DateTime(year, 8, dayOfMonth);
        }

        public static DateTime September(this int dayOfMonth, int year) {
            return new DateTime(year, 9, dayOfMonth);
        }

        public static DateTime October(this int dayOfMonth, int year) {
            return new DateTime(year, 10, dayOfMonth);
        }

        public static DateTime November(this int dayOfMonth, int year) {
            return new DateTime(year, 11, dayOfMonth);
        }

        public static DateTime December(this int dayOfMonth, int year) {
            return new DateTime(year, 12, dayOfMonth);
        }
    }
}