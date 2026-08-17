using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using TUnit.Mocks;
using TUnit.Mocks.Arguments;
using TUnit.Mocks.Generated;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class ImmutableDictionaryWithValueEqualityTests
{
    [Test]
    public async Task ToImmutableDictionaryWithValueEquality_WhenDictionaryIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        IImmutableDictionary<string, int> immutableDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int>());

        // Act
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            immutableDictionary.ToImmutableDictionaryWithValueEquality();

        // Assert
        await Assert.That(dictionary.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToImmutableDictionaryWithValueEquality_WhenDictionaryHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        IImmutableDictionary<string, int> immutableDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });

        // Act
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            immutableDictionary.ToImmutableDictionaryWithValueEquality();

        // Assert
        await Assert.That(dictionary.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(dictionary["a"]).IsEqualTo(1);
            await Assert.That(dictionary["b"]).IsEqualTo(2);
            await Assert.That(dictionary["c"]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveDifferentCount_ThenReturnsFalse()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary1 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        IImmutableDictionaryWithValueEquality<string, int> dictionary2 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveDifferentKeys_ThenReturnsFalse()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary1 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        IImmutableDictionaryWithValueEquality<string, int> dictionary2 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["x"] = 1, ["y"] = 2, ["z"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSameKeysButDifferentValues_ThenReturnsFalse()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary1 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        IImmutableDictionaryWithValueEquality<string, int> dictionary2 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 99 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSamePairsInDifferentOrder_ThenReturnsTrue()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary1 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        IImmutableDictionaryWithValueEquality<string, int> dictionary2 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["c"] = 3, ["a"] = 1, ["b"] = 2 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSamePairsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary1 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        IImmutableDictionaryWithValueEquality<string, int> dictionary2 =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task RecordEquals_WhenDictionariesHaveDifferentCount_ThenReturnsFalse()
    {
        // Arrange
        Record record1 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                    .ToImmutableDictionaryWithValueEquality());

        Record record2 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 })
                    .ToImmutableDictionaryWithValueEquality());

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task RecordEquals_WhenDictionariesHaveDifferentKeys_ThenReturnsFalse()
    {
        // Arrange
        Record record1 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                    .ToImmutableDictionaryWithValueEquality());

        Record record2 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["x"] = 1, ["y"] = 2, ["z"] = 3 })
                    .ToImmutableDictionaryWithValueEquality());

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task RecordEquals_WhenDictionariesHaveSameKeysButDifferentValues_ThenReturnsFalse()
    {
        // Arrange
        Record record1 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                    .ToImmutableDictionaryWithValueEquality());

        Record record2 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 99 })
                    .ToImmutableDictionaryWithValueEquality());

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task RecordEquals_WhenDictionariesHaveSamePairsInDifferentOrder_ThenReturnsTrue()
    {
        // Arrange
        Record record1 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                    .ToImmutableDictionaryWithValueEquality());

        Record record2 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["c"] = 3, ["a"] = 1, ["b"] = 2 })
                    .ToImmutableDictionaryWithValueEquality());

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task RecordEquals_WhenDictionariesHaveSamePairsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        Record record1 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                    .ToImmutableDictionaryWithValueEquality());

        Record record2 =
            new(
                ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                    .ToImmutableDictionaryWithValueEquality());

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Count_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Count.Returns(3);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        int count = dictionary.Count;

        // Assert
        await Assert.That(count).IsEqualTo(3);
        mock.Count.WasCalled(Times.Once);
    }

    [Test]
    public async Task ContainsKey_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.ContainsKey(Arg.Any<string>()).Returns(true);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        bool containsKey = dictionary.ContainsKey("b");

        // Assert
        await Assert.That(containsKey).IsTrue();
        mock.ContainsKey("b").WasCalled(Times.Once);
    }

    [Test]
    public async Task TryGetValue_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.TryGetValue(Arg.Any<string>()).Returns(true).SetsOutValue(2);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        bool found = dictionary.TryGetValue(key: "b", value: out int value);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(value).IsEqualTo(2);
        }

        mock.TryGetValue("b").WasCalled(Times.Once);
    }

    [Test]
    public async Task Indexer_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Item(Arg.Any<string>()).Returns(3);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        int value = dictionary["c"];

        // Assert
        await Assert.That(value).IsEqualTo(3);
        mock.Item("c").WasCalled(Times.Once);
    }

    [Test]
    public async Task Keys_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        string[] keys = ["a", "b", "c"];
        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Keys.Returns(keys);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IEnumerable<string> result = dictionary.Keys;

        // Assert
        await Assert.That(result).IsEquivalentTo(keys);
        mock.Keys.WasCalled(Times.Once);
    }

    [Test]
    public async Task Values_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        int[] values = [1, 2, 3];
        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Values.Returns(values);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IEnumerable<int> result = dictionary.Values;

        // Assert
        await Assert.That(result).IsEquivalentTo(values);
        mock.Values.WasCalled(Times.Once);
    }

    [Test]
    public async Task Enumerator_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        List<KeyValuePair<string, int>> items =
        [
            new(key: "a", value: 1), new(key: "b", value: 2), new(key: "c", value: 3)
        ];

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        List<KeyValuePair<string, int>> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (KeyValuePair<string, int> pair in dictionary)
        {
            result.Add(pair);
        }

        // Assert
        await Assert.That(result).IsEquivalentTo(items);
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    [Test]
    public async Task Clear_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int>());

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Clear().Returns(returnedDictionary);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.Clear();

        // Assert
        await Assert.That(result.Count).IsEqualTo(0);
        mock.Clear().WasCalled(Times.Once);
    }

    [Test]
    public async Task Add_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["d"] = 4 });

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Add(key: Arg.Any<string>(), value: Arg.Any<int>()).Returns(returnedDictionary);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.Add(key: "d", value: 4);

        // Assert
        await Assert.That(result["d"]).IsEqualTo(4);
        mock.Add(key: "d", value: 4).WasCalled(Times.Once);
    }

    [Test]
    public async Task AddRange_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        KeyValuePair<string, int>[] pairs = [new(key: "d", value: 4), new(key: "e", value: 5)];

        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["d"] = 4, ["e"] = 5 });

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.AddRange(Arg.Any<IEnumerable<KeyValuePair<string, int>>>()).Returns(returnedDictionary);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.AddRange(pairs);

        // Assert
        await Assert.That(result.Count).IsEqualTo(2);
        mock.AddRange(pairs).WasCalled(Times.Once);
    }

    [Test]
    public async Task SetItem_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 100 });

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.SetItem(key: Arg.Any<string>(), value: Arg.Any<int>()).Returns(returnedDictionary);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.SetItem(key: "a", value: 100);

        // Assert
        await Assert.That(result["a"]).IsEqualTo(100);
        mock.SetItem(key: "a", value: 100).WasCalled(Times.Once);
    }

    [Test]
    public async Task SetItems_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        KeyValuePair<string, int>[] items = [new(key: "a", value: 111), new(key: "d", value: 4)];

        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 111, ["d"] = 4 });

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.SetItems(Arg.Any<IEnumerable<KeyValuePair<string, int>>>()).Returns(returnedDictionary);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.SetItems(items);

        // Assert
        await Assert.That(result["a"]).IsEqualTo(111);
        mock.SetItems(items).WasCalled(Times.Once);
    }

    [Test]
    public async Task RemoveRange_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        string[] keys = ["a", "b"];

        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["c"] = 3 });

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.RemoveRange(Arg.Any<IEnumerable<string>>()).Returns(returnedDictionary);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.RemoveRange(keys);

        // Assert
        await Assert.That(result.Count).IsEqualTo(1);
        mock.RemoveRange(keys).WasCalled(Times.Once);
    }

    [Test]
    public async Task Remove_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["c"] = 3 });

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Remove(Arg.Any<string>()).Returns(returnedDictionary);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.Remove("b");

        // Assert
        await Assert.That(result.ContainsKey("b")).IsFalse();
        mock.Remove("b").WasCalled(Times.Once);
    }

    [Test]
    public async Task Contains_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        KeyValuePair<string, int> pair = new(key: "a", value: 1);
        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Contains(Arg.Any<KeyValuePair<string, int>>()).Returns(true);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        bool contains = dictionary.Contains(pair);

        // Assert
        await Assert.That(contains).IsTrue();
        mock.Contains(pair).WasCalled(Times.Once);
    }

    [Test]
    public async Task TryGetKey_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.TryGetKey(Arg.Any<string>()).Returns(true);

        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            mock.ToImmutableDictionaryWithValueEquality();

        // Act
        bool found = dictionary.TryGetKey(equalKey: "a", actualKey: out string _);

        // Assert
        await Assert.That(found).IsTrue();
        mock.TryGetKey("a").WasCalled(Times.Once);
    }

    [Test]
    public async Task
        ImmutableDictionaryAdd_WhenCalledThroughBaseInterface_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["d"] = 4 });

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.Add(key: Arg.Any<string>(), value: Arg.Any<int>()).Returns(returnedDictionary);
        IImmutableDictionary<string, int> dictionary = mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionary<string, int> result = dictionary.Add(key: "d", value: 4);

        // Assert
        await Assert.That(result["d"]).IsEqualTo(4);
        mock.Add(key: "d", value: 4).WasCalled(Times.Once);
    }

    [Test]
    public async Task
        ImmutableDictionarySetItem_WhenCalledThroughBaseInterface_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IImmutableDictionary<string, int> returnedDictionary =
            ImmutableDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 100 });

        IImmutableDictionary_TKey_TValue_Mock<string, int> mock = IImmutableDictionary<string, int>.Mock();
        mock.SetItem(key: Arg.Any<string>(), value: Arg.Any<int>()).Returns(returnedDictionary);
        IImmutableDictionary<string, int> dictionary = mock.ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionary<string, int> result = dictionary.SetItem(key: "a", value: 100);

        // Assert
        await Assert.That(result["a"]).IsEqualTo(100);
        mock.SetItem(key: "a", value: 100).WasCalled(Times.Once);
    }

    private record Record(IImmutableDictionaryWithValueEquality<string, int> Dictionary);
}