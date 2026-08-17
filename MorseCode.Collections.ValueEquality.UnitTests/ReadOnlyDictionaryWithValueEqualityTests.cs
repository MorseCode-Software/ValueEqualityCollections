using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using TUnit.Mocks;
using TUnit.Mocks.Arguments;
using TUnit.Mocks.Generated;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class ReadOnlyDictionaryWithValueEqualityTests
{
    [Test]
    public async Task ToReadOnlyDictionaryWithValueEquality_WhenDictionaryIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IReadOnlyDictionary<string, int> dictionaryWithoutValueEquality = new Dictionary<string, int>();

        // Act
        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            dictionaryWithoutValueEquality.ToReadOnlyDictionaryWithValueEquality();

        // Assert
        await Assert.That(dictionary.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToReadOnlyDictionaryWithValueEquality_WhenDictionaryHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        IReadOnlyDictionary<string, int> dictionaryWithoutValueEquality =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 };

        // Act
        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            dictionaryWithoutValueEquality.ToReadOnlyDictionaryWithValueEquality();

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
        IReadOnlyDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToReadOnlyDictionaryWithValueEquality();

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }.ToReadOnlyDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveDifferentKeys_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToReadOnlyDictionaryWithValueEquality();

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["x"] = 1, ["y"] = 2, ["z"] = 3 }.ToReadOnlyDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSameKeysButDifferentValues_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToReadOnlyDictionaryWithValueEquality();

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 99 }.ToReadOnlyDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSamePairsInDifferentOrder_ThenReturnsTrue()
    {
        // Arrange
        IReadOnlyDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToReadOnlyDictionaryWithValueEquality();

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["c"] = 3, ["a"] = 1, ["b"] = 2 }.ToReadOnlyDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSamePairsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IReadOnlyDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToReadOnlyDictionaryWithValueEquality();

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToReadOnlyDictionaryWithValueEquality();

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                    .ToReadOnlyDictionaryWithValueEquality());

        Record record2 =
            new(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }.ToReadOnlyDictionaryWithValueEquality());

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                    .ToReadOnlyDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["x"] = 1, ["y"] = 2, ["z"] = 3 }
                    .ToReadOnlyDictionaryWithValueEquality());

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                    .ToReadOnlyDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 99 }
                    .ToReadOnlyDictionaryWithValueEquality());

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                    .ToReadOnlyDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["c"] = 3, ["a"] = 1, ["b"] = 2 }
                    .ToReadOnlyDictionaryWithValueEquality());

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                    .ToReadOnlyDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                    .ToReadOnlyDictionaryWithValueEquality());

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Count_WhenCalled_ThenCallIsPassedThroughToUnderlyingDictionary()
    {
        // Arrange
        IReadOnlyDictionary_TKey_TValue_Mock<string, int> mock = IReadOnlyDictionary<string, int>.Mock();
        mock.Count.Returns(3);

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            mock.ToReadOnlyDictionaryWithValueEquality();

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
        IReadOnlyDictionary_TKey_TValue_Mock<string, int> mock = IReadOnlyDictionary<string, int>.Mock();
        mock.ContainsKey(Arg.Any<string>()).Returns(true);

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            mock.ToReadOnlyDictionaryWithValueEquality();

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
        IReadOnlyDictionary_TKey_TValue_Mock<string, int> mock = IReadOnlyDictionary<string, int>.Mock();
        mock.TryGetValue(Arg.Any<string>()).Returns(true).SetsOutValue(2);

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            mock.ToReadOnlyDictionaryWithValueEquality();

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
        IReadOnlyDictionary_TKey_TValue_Mock<string, int> mock = IReadOnlyDictionary<string, int>.Mock();
        mock.Item(Arg.Any<string>()).Returns(3);

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            mock.ToReadOnlyDictionaryWithValueEquality();

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
        IReadOnlyDictionary_TKey_TValue_Mock<string, int> mock = IReadOnlyDictionary<string, int>.Mock();
        mock.Keys.Returns(keys);

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            mock.ToReadOnlyDictionaryWithValueEquality();

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
        IReadOnlyDictionary_TKey_TValue_Mock<string, int> mock = IReadOnlyDictionary<string, int>.Mock();
        mock.Values.Returns(values);

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            mock.ToReadOnlyDictionaryWithValueEquality();

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

        IReadOnlyDictionary_TKey_TValue_Mock<string, int> mock = IReadOnlyDictionary<string, int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());

        IReadOnlyDictionaryWithValueEquality<string, int> dictionary =
            mock.ToReadOnlyDictionaryWithValueEquality();

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

    private record Record(IReadOnlyDictionaryWithValueEquality<string, int> Dictionary);
}