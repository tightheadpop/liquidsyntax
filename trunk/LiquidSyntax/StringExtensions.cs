using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace LiquidSyntax {
    public static class StringExtensions {
        public static string Substitute(this string format, params object[] args) {
            return string.Format(format, args);
        }

        public static string Reversed(this string original) {
            if (original == null) return null;
            return original.Reverse().Join(string.Empty);
        }

        public static string Times(this int count, string toRepeat) {
            return string.Join(toRepeat, new string[count + 1]);
        }

        public static bool ContainsIgnoreCase(this string searchIn, string subString) {
            return searchIn.ToLower().IndexOf(subString.ToLower(), 0) >= 0;
        }

        public static bool EqualsIgnoreCase(this string string1, string string2) {
            return string.Compare(string1, string2, true) == 0;
        }

        public static bool IsEmpty(this string s) {
            return s.TrimToEmpty() == string.Empty;
        }

        public static bool IsNotEmpty(this string s) {
            return !s.IsEmpty();
        }

        public static bool IsNullOrEmpty(this string s) {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNumeric(this string value) {
            double number;
            return double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out number);
        }

        public static string NullToEmpty(this string value) {
            return value ?? string.Empty;
        }

        public static string StringAfterFirst(this string s, string token) {
            if (s.Contains(token)) {
                return s.Substring(s.ToLower().IndexOf(token.ToLower()) + token.Length);
            }
            return s;
        }

        public static string StringAfterLast(this string masterString, string token) {
            if (masterString.Contains(token)) {
                return masterString.Substring(masterString.ToLower().LastIndexOf(token.ToLower()) + 1);
            }
            return masterString;
        }

        public static string StringBeforeFirst(this string s, string token) {
            if (s.Contains(token)) {
                return s.Substring(0, s.ToLower().IndexOf(token.ToLower()));
            }
            return s;
        }

        public static string StringBeforeLast(this string s, string token) {
            if (s.Contains(token)) {
                return s.Substring(0, s.ToLower().LastIndexOf(token.ToLower()));
            }
            return s;
        }

        public static string TrimToEmpty(this string s) {
            if (s == null) {
                return string.Empty;
            }
            return s.Trim();
        }

        public static string TrimToNull(this string s) {
            if (s == null) {
                return null;
            }
            var result = s.Trim();
            if (result == string.Empty)
                return null;
            return result;
        }

        public static string WithoutPrefix(this string s, string prefix) {
            return s.IsNullOrEmpty() || prefix.IsNullOrEmpty() || !s.StartsWith(prefix) ? s : s.StringAfterFirst(prefix);
        }

        public static string WithoutSuffix(this string s, string suffix) {
            return s.IsNullOrEmpty() || suffix.IsNullOrEmpty() || !s.EndsWith(suffix) ? s : s.StringBeforeLast(suffix);
        }

        public static string WithoutPrefixPattern(this string stringToTrim, string prefixPattern) {
            return Regex.Replace(stringToTrim, "^" + prefixPattern, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string WithoutSuffixPattern(this string stringToTrim, string suffixPattern) {
            return Regex.Replace(stringToTrim, suffixPattern + "$", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string WithPrefix(this string stringToAlter, string prefix) {
            var result = stringToAlter;
            if (!result.StartsWith(prefix)) {
                result = prefix + result;
            }
            return result;
        }

        public static string WithSuffix(this string stringToAlter, string suffix) {
            var result = stringToAlter;
            if (!result.EndsWith(suffix)) {
                result += suffix;
            }
            return result;
        }
    }
}