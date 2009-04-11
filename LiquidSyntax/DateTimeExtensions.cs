using System;

namespace LiquidSyntax {
    public static class DateTimeExtensions {
        public static DateTime After(this TimeSpan span, DateTime start) {
            return start.Add(span);
        }

        public static DateTime Ago(this TimeSpan span) {
            return DateTime.Now.Subtract(span);
        }

        public static DateTime Before(this TimeSpan span, DateTime start) {
            return start.Subtract(span);
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
            return start.AddDays(7).FirstDayOfWeek().AddDays((int) dayOfWeek);
        }

        public static DateTime Previous(this DateTime start, DayOfWeek dayOfWeek) {
            return 8.Days().Before(start).Next(dayOfWeek);
        }
    }
}