namespace LiquidSyntax {
    public static class StringExtensions {
        public static string Substitute(this string format, params object[] args) {
            return string.Format(format, args);
        }
    }
}