using System;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class ExceptionExtensionsTests {
        [Test]
        public void ShouldIgnoreExceptionExplicitlyInOrderToAvoidCompilerWarnings() {
            try {
                throw new ApplicationException();
            }
            catch (Exception expected) {
                expected.Ignore();
            }
        }
    }
}