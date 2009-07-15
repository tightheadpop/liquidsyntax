using System;
using System.Collections.Generic;
using System.Linq;

namespace LiquidSyntax {
    public static class DisposeExtensions {
        /// <summary>
        /// Disposes all items in the list, throwing any exceptions encountered, and ignoring null items.
        /// </summary>
        public static void Dispose<TDisposable>(this IEnumerable<TDisposable> disposables) where TDisposable : IDisposable {
            if (disposables == null)
                return;
            foreach (IDisposable disposable in disposables) {
                if (disposable != null)
                    disposable.Dispose();
            }
        }

        public static void DisposeQuietly<TDisposable>(this IEnumerable<TDisposable> disposables) where TDisposable : IDisposable {
            if (disposables == null)
                return;
            disposables.ToList().ForEach(d => d.DisposeQuietly());
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