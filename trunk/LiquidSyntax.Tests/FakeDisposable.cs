using System;

namespace LiquidSyntax.Tests {
    public class FakeDisposable : IDisposable {
        public bool Disposed { get; private set; }

        public virtual void Dispose() {
            Disposed = true;
        }
    }
}