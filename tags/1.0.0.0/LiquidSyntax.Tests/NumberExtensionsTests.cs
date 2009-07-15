using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class NumberExtensionsTests {
        [Test]
        public void CanCreateIncreasingRangeOfIntegers() {
            1.To(5).Should(Be.EqualTo(new[] {1, 2, 3, 4, 5}));
            1.To(1).Should(Be.EqualTo(new[] {1}));
        }

        [Test]
        public void CanCreateDecreasingRangeOfIntegers() {
            5.To(1).Should(Be.EqualTo(new[] {5, 4, 3, 2, 1}));
        }

        [Test]
        public void ShouldExecuteGivenBlockASpecifiedNumberOfTimes() {
            var i = 0;
            3.Times(()=>i++);
            i.Should(Be.EqualTo(3));
        }

        [Test]
        public void ShouldNotExecuteBlockIfCountIsZero() {
            var i = 0;
            0.Times(() => i++);
            i.Should(Be.EqualTo(0));
        }

        [Test]
        public void ShouldNotExecuteBlockIfCountIsNegative() {
            var i = 0;
            (-1).Times(() => i++);
            i.Should(Be.EqualTo(0));
        }

        [Test]
        public void ShouldExecuteGivenBlockASpecifiedNumberOfTimesPassingTheLoopCounterToTheAction() {
            var result = 0;
            3.Times(i=>result = i);
            result.Should(Be.EqualTo(3));
        }
    }
}