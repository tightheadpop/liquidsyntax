using System;
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
            return new List<T>{obj};
        }
    }
}