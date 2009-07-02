using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LiquidSyntax {
    public static class CollectionExtensions {
        public static List<TComparable> Sorted<TComparable>(this IEnumerable<TComparable> comparables) where TComparable : IComparable<TComparable> {
            var sorted = comparables.ToList();
            sorted.Sort();
            return sorted;
        }

        public static string Join<T>(this IEnumerable<T> items, string delimiter) {
            return string.Join(delimiter, items.ToList().ConvertAll(item => item.ToString()).ToArray());
        }

        public static List<T> AsList<T>(this T obj) {
            return new List<T> {obj};
        }

        public static bool IsEmpty(this IEnumerable enumerable) {
#pragma warning disable 168
            foreach (var item in enumerable)
#pragma warning restore 168
                return false;
            return true;
        }

        public static bool IsNotEmpty(this IEnumerable enumerable) {
            return !enumerable.IsEmpty();
        }

        public static List<int> To(this int start, int end) {
            if (end < start)
                return Enumerable.Range(end, start - end + 1).Reverse().ToList();
            return Enumerable.Range(start, end - start + 1).ToList();
        }
    }
}