using System;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class EventExtensionsTests {
        private EventHost eventHost;

        [SetUp]
        public void SetUp() {
            eventHost = new EventHost();
        }

        [Test]
        public void ShouldRaiseEventWithEmptyEventArgs() {
            var raised = false;
            EventArgs args = null;
            eventHost.Event += (sender, e) => {
                                   raised = true;
                                   args = e;
                               };
            eventHost.OnEvent();
            raised.Should(Be.True);
            args.Should(Be.SameAs(EventArgs.Empty));
        }

        [Test]
        public void ShouldRaiseGenericEventWithArgs() {
            var raised = false;
            GenericEventArgs args = null;
            eventHost.GenericEvent += (sender, e) => {
                                          raised = true;
                                          args = e;
                                      };
            eventHost.OnGenericEvent();
            raised.Should(Be.True);
            args.ShouldNot(Be.Null);
        }

        private class EventHost {
            public void OnEvent() {
                Event.Raise(this);
            }

            public void OnGenericEvent() {
                GenericEvent.Raise(this, new GenericEventArgs());
            }

            public event EventHandler Event;
            public event EventHandler<GenericEventArgs> GenericEvent;
        }

        private class GenericEventArgs : EventArgs {}
    }
}