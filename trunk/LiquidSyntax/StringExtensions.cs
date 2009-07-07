using System.Linq;

namespace LiquidSyntax {
    public static class StringExtensions {
        public static string Substitute(this string format, params object[] args) {
            return string.Format(format, args);
        }

        public static string Reversed(this string original) {
            if (original == null) return null;
            return original.Reverse().Join(string.Empty);
        }
    }
}