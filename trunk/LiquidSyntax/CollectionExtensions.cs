using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LiquidSyntax {
    public static class CollectionExtensions {
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> collectionToAdd) {
            collectionToAdd.ForEach(collection.Add);
        }

        public static List<T> AsList<T>(this T obj) {
            return new List<T> {obj};
        }

        public static HashSet<T> AsSet<T>(this T foo) {
            return foo.AsList().ToSet();
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T, int> action) {
            var index = 0;
            foreach (var item in items) {
                action(item, index);
                index++;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) {
            foreach (var item in items) {
                action(item);
            }
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

        public static string Join<T>(this IEnumerable<T> items, string delimiter) {
            return string.Join(delimiter, items.ToList().ConvertAll(item => item.ToString()).ToArray());
        }

        public static List<TComparable> Sorted<TComparable>(this IEnumerable<TComparable> comparables) where TComparable : IComparable<TComparable> {
            var sorted = comparables.ToList();
            sorted.Sort();
            return sorted;
        }

        public static List<T> SortedBy<T>(this IEnumerable<T> collection, Comparison<T> comparison) {
            var list = collection.ToList();
            list.Sort(comparison);
            return list;
        }

        public static List<T> SortedBy<T>(this IEnumerable<T> collection, Comparer<T> comparer) {
            var list = collection.ToList();
            list.Sort(comparer);
            return list;
        }

        public static HashSet<T> ToSet<T>(this ICollection<T> collection) {
            return new HashSet<T>(collection);
        }
    }
}