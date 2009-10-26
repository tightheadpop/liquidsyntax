using System.Collections.Generic;
using LiquidSyntax.ForTesting;
using NUnit.Framework;

namespace LiquidSyntax.Tests {
    [TestFixture]
    public class CollectionExtensionsTests {
        [Test]
        public void ForEachShouldEnumerateItemsAndPerformAction() {
            var total = 0;
            new[] {1, 2, 3}.ForEach(i => total += i);
            total.Should(Be.EqualTo(6));
        }

        [Test]
        public void ShouldCreateListFromSingleObject() {
            var instance = new FakeDisposable();
            var list = instance.AsList();
            list.Should(Be.EqualTo(new[] {instance}));
            list.Should(Be.InstanceOf(typeof(List<FakeDisposable>)));
        }

        [Test]
        public void ShouldCreateSetFromSingleObject() {
            var instance = new FakeDisposable();
            var set = instance.AsSet();
            set.Should(Be.EqualTo(new[] {instance}));
            set.Should(Be.InstanceOf(typeof(HashSet<FakeDisposable>)));
        }

        [Test]
        public void ShouldEnableAddOfMultipleItemsToCollection() {
            var dictionary = new Dictionary<int, string> {{1, "foo"}};
            var toAdd = new Dictionary<int, string> {{2, "bar"}, {3, "baz"}};
            dictionary.AddRange(toAdd);

            IEnumerable<KeyValuePair<int, string>> expected = new Dictionary<int, string> {
                {1, "foo"}, {2, "bar"}, {3, "baz"}
            };
            dictionary.Should(Be.EquivalentTo(expected));
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
        public void ShouldJoinWithDelimiter() {
            new List<int> {3, 4, 2}.Join(", ").Should(Be.EqualTo("3, 4, 2"));
        }

        [Test]
        public void ShouldSortEnumerableByComparer() {
            var ints = new[] {3, 2, 6};
            ints.SortedBy(new IntegerComparer()).Should(Be.EqualTo(new[] {2, 3, 6}));
        }

        [Test]
        public void ShouldSortEnumerableByComparisonMethod() {
            var ints = new[] {3, 2, 6};
            ints.SortedBy((a, b) => a - b).Should(Be.EqualTo(new[] {2, 3, 6}));
        }

        [Test]
        public void ShouldSortNaturally() {
            new List<int> {3, 4, 2}.Sorted().Should(Be.EqualTo(new[] {2, 3, 4}));
        }

        [Test]
        public void ToSetShouldRemoveDuplicates() {
            var list = new List<string> {"a", "b", "a"};
            var set = list.ToSet();
            set.Should(Be.EqualTo(new[] {"a", "b"}));
            set.Should(Be.InstanceOf(typeof(HashSet<string>)));
        }

        private class IntegerComparer : Comparer<int> {
            public override int Compare(int x, int y) {
                return x - y;
            }
        }
    }
}