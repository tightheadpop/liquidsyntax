using System;
using System.Collections.Generic;
using System.Linq;

namespace LiquidSyntax {
    public static class NumberExtensions {
        public static List<int> To(this int start, int end) {
            if (end < start)
                return Enumerable.Range(end, start - end + 1).Reverse().ToList();
            return Enumerable.Range(start, end - start + 1).ToList();
        }

        public static void Times(this int numberOfIterations, Action action) {
            for (var i = 0; i < numberOfIterations; i++) {
                action();
            }
        }

        public static void Times(this int numberOfIterations, Action<int> action) {
            for (var i = 1; i <= numberOfIterations; i++) {
                action(i);
            }
        }
    }
}