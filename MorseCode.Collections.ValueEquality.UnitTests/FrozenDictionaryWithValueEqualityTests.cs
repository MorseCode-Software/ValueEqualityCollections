using System.Collections.Frozen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class FrozenDictionaryWithValueEqualityTests
{
    [Test]
    public async Task ToFrozenDictionaryWithValueEquality_WhenDictionaryIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        FrozenDictionary<string, int> frozenDictionary =
            new Dictionary<string, int>().ToFrozenDictionary();

        // Act
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            frozenDictionary.ToFrozenDictionaryWithValueEquality();

        // Assert
        await Assert.That(dictionary.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToFrozenDictionaryWithValueEquality_WhenDictionaryHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        FrozenDictionary<string, int> frozenDictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToFrozenDictionary();

        // Act
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            frozenDictionary.ToFrozenDictionaryWithValueEquality();

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
        IFrozenDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        IFrozenDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveDifferentKeys_ThenReturnsFalse()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        IFrozenDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["x"] = 1, ["y"] = 2, ["z"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSameKeysButDifferentValues_ThenReturnsFalse()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        IFrozenDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 99 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSamePairsInDifferentOrder_ThenReturnsTrue()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        IFrozenDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["c"] = 3, ["a"] = 1, ["b"] = 2 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        bool areEqual = dictionary1.Equals(dictionary2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Equals_WhenDictionariesHaveSamePairsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary1 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        IFrozenDictionaryWithValueEquality<string, int> dictionary2 =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["x"] = 1, ["y"] = 2, ["z"] = 3 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 99 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["c"] = 3, ["a"] = 1, ["b"] = 2 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

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
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

        Record record2 =
            new(
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }.ToFrozenDictionary()
                    .ToFrozenDictionaryWithValueEquality());

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Count_WhenDictionaryIsEmpty_ThenResultIs0()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int>()
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        int count = dictionary.Count;

        // Assert
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task Count_WhenDictionaryHasThreeItems_ThenResultIs3()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        int count = dictionary.Count;

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task ContainsKey_WhenKeyIsPresent_ThenReturnsTrue()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        bool containsKey = dictionary.ContainsKey("b");

        // Assert
        await Assert.That(containsKey).IsTrue();
    }

    [Test]
    public async Task ContainsKey_WhenKeyIsAbsent_ThenReturnsFalse()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        bool containsKey = dictionary.ContainsKey("z");

        // Assert
        await Assert.That(containsKey).IsFalse();
    }

    [Test]
    public async Task TryGetValue_WhenKeyIsPresent_ThenReturnsTrueAndOutputsValue()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        bool found = dictionary.TryGetValue(key: "b", value: out int value);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(value).IsEqualTo(2);
        }
    }

    [Test]
    public async Task TryGetValue_WhenKeyIsAbsent_ThenReturnsFalse()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        bool found = dictionary.TryGetValue(key: "z", value: out int value);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsFalse();
            await Assert.That(value).IsEqualTo(0);
        }
    }

    [Test]
    public async Task Indexer_WhenKeyIsPresent_ThenReturnsValue()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        int value = dictionary["c"];

        // Assert
        await Assert.That(value).IsEqualTo(3);
    }

    [Test]
    public async Task Keys_WhenDictionaryHasThreeItems_ThenResultContainsAllKeys()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        IEnumerable<string> keys = dictionary.Keys;

        // Assert
        await Assert.That(keys).IsEquivalentTo(["a", "b", "c"]);
    }

    [Test]
    public async Task Values_WhenDictionaryHasThreeItems_ThenResultContainsAllValues()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        IEnumerable<int> values = dictionary.Values;

        // Assert
        await Assert.That(values).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Enumerator_WhenDictionaryIsEmpty_ThenNoElementsAreEnumerated()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int>()
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        List<KeyValuePair<string, int>> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (KeyValuePair<string, int> pair in dictionary)
        {
            result.Add(pair);
        }

        // Assert
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task Enumerator_WhenDictionaryHasThreeItems_ThenSameThreeElementsAreEnumerated()
    {
        // Arrange
        IFrozenDictionaryWithValueEquality<string, int> dictionary =
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ToFrozenDictionary()
                .ToFrozenDictionaryWithValueEquality();

        // Act
        List<KeyValuePair<string, int>> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (KeyValuePair<string, int> pair in dictionary)
        {
            result.Add(pair);
        }

        // Assert
        await Assert.That(result)
            .IsEquivalentTo(
            [
                new KeyValuePair<string, int>(key: "a", value: 1),
                new KeyValuePair<string, int>(key: "b", value: 2),
                new KeyValuePair<string, int>(key: "c", value: 3)
            ]);
    }

    private record Record(IFrozenDictionaryWithValueEquality<string, int> Dictionary);
}