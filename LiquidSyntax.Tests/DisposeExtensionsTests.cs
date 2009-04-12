using System;
using System.Collections.Generic;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class DisposeExtensionsTests {
        [Test]
        public void DisposeOfListDisposesAllItemsInList() {
            var disposables = new List<FakeDisposable> {new FakeDisposable(), new FakeDisposable()};

            disposables.Dispose();

            disposables.ForEach(d => d.Disposed.Should(Be.True));
        }

        [Test]
        public void DisposeQuietlyOfIDisposableSuppressesExceptions() {
            var exceptionThrowingDisposable = new ExceptionThrowingDisposable();

            exceptionThrowingDisposable.DisposeQuietly();

            exceptionThrowingDisposable.Disposed.Should(Be.True);
        }

        [Test]
        public void DisposeQuietlyOfNullThrowsNoException() {
            ((IDisposable) null).DisposeQuietly();
        }

        [Test]
        public void DisposingListDoesNotStifleExceptions() {
            try {
                new List<IDisposable> {new ExceptionThrowingDisposable()}.Dispose();
                Assert.Fail();
            }
            catch (ApplicationException) {}
        }

        [Test]
        public void DisposingListIgnoresNulls() {
            new List<IDisposable> {(IDisposable) null}.Dispose();
        }

        [Test]
        public void DisposingListsIgnoresNullList() {
            ((IEnumerable<IDisposable>) null).Dispose();
        }

        [Test]
        public void DisposingListsQuietlyIgnoresNullList() {
            ((IEnumerable<IDisposable>) null).DisposeQuietly();
        }

        [Test]
        public void DisposingListsQuietlyIgnoresNulls() {
            new List<IDisposable> {(IDisposable) null}.DisposeQuietly();
        }

        [Test]
        public void QuietlyDisposingListStiflesExceptions() {
            var item = new ExceptionThrowingDisposable();
            new List<IDisposable> {item}.DisposeQuietly();
            item.Disposed.Should(Be.True);
        }

        private class ExceptionThrowingDisposable : IDisposable {
            public bool Disposed { get; private set; }

            public void Dispose() {
                Disposed = true;
                throw new ApplicationException("Simulate a fatal disposal.");
            }
        }
    }
}