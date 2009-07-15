using System.Collections.Generic;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class CollectionExtensionsTests {
        [Test]
        public void ShouldSortNaturally() {
            new List<int> {3, 4, 2}.Sorted().Should(Be.EqualTo(new[] {2, 3, 4}));
        }

        [Test]
        public void ShouldJoinWithDelimiter() {
            new List<int> {3, 4, 2}.Join(", ").Should(Be.EqualTo("3, 4, 2"));
        }

        [Test]
        public void ShouldCreateListFromSingleObject() {
            var instance = new FakeDisposable();
            var list = instance.AsList();
            list.Should(Be.EqualTo(new[] {instance}));
            list.Should(Be.InstanceOf(typeof(List<FakeDisposable>)));
        }

        [Test]
        public void ShouldIdentifyEmptyCollection() {
            new int[] {}.IsEmpty().Should(Be.True);
            new[] {1}.IsEmpty().Should(Be.False);
        }

        [Test]
        public void ShouldIdentifyNonEmptyCollection() {
            new int[] {}.IsNotEmpty().Should(Be.False);
            new[] {1}.IsNotEmpty().Should(Be.True);
        }

        [Test]
        public void ShiftShouldRemoveAndReturnTheFirstElement() {
            var list = new List<string> {"43","111","26"};
            list.Shift().Should(Be.EqualTo("43"));
            list.Should(Have.Count.EqualTo(2));

            list.Shift().Should(Be.EqualTo("111"));
            list.Should(Have.Count.EqualTo(1));

            list.Shift().Should(Be.EqualTo("26"));
            list.Should(Have.Count.EqualTo(0));
        }

        [Test]
        public void ShiftShouldReturnNullWhenCollectionIsEmpty() {
            new List<string>().Shift().Should(Be.Null);
        }

        [Test]
        public void ForEachShouldEnumerateItemsAndPerformAction() {
            var total = 0;
            new[] {1, 2, 3}.ForEach(i => total += i);
            total.Should(Be.EqualTo(6));
        }
    }
}