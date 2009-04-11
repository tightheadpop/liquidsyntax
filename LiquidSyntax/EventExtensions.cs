using System;

namespace LiquidSyntax {
    public static class EventExtensions {
        public static void Raise(this EventHandler handler, object sender) {
            if (handler != null) {
                handler(sender, EventArgs.Empty);
            }
        }

        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs args)
            where TEventArgs : EventArgs {
            if (handler != null) {
                handler(sender, args);
            }
        }
    }
}