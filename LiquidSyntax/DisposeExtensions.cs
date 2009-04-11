using System;
using System.Collections.Generic;

namespace LiquidSyntax {
    public static class DisposeExtensions {
        /// <summary>
        /// Disposes all items in the list, throwing any exceptions encountered, and ignoring null items.
        /// </summary>
        public static void Dispose<TDisposable>(this IEnumerable<TDisposable> disposables) where TDisposable : IDisposable {
            if (disposables == null)
                return;
            foreach (IDisposable item in disposables) {
                if (item != null)
                    item.Dispose();
            }
        }

        public static void DisposeQuietly<TDisposable>(this IEnumerable<TDisposable> disposables) where TDisposable : IDisposable {
            if (disposables == null)
                return;
            foreach (IDisposable item in disposables)
                item.DisposeQuietly();
        }

        public static void DisposeQuietly(this IDisposable disposable) {
            if (disposable == null)
                return;
            try {
                disposable.Dispose();
            }
            catch (Exception toIgnore) {
                toIgnore.Ignore();
            }
        }
    }
}