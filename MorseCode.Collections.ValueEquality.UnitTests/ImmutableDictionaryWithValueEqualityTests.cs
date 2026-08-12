using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

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
    public async Task Count_WhenDictionaryIsEmpty_ThenResultIs0()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int>())
                .ToImmutableDictionaryWithValueEquality();

        // Act
        int count = dictionary.Count;

        // Assert
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task Count_WhenDictionaryHasThreeItems_ThenResultIs3()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        int count = dictionary.Count;

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task ContainsKey_WhenKeyIsPresent_ThenReturnsTrue()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool containsKey = dictionary.ContainsKey("b");

        // Assert
        await Assert.That(containsKey).IsTrue();
    }

    [Test]
    public async Task ContainsKey_WhenKeyIsAbsent_ThenReturnsFalse()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool containsKey = dictionary.ContainsKey("z");

        // Assert
        await Assert.That(containsKey).IsFalse();
    }

    [Test]
    public async Task TryGetValue_WhenKeyIsPresent_ThenReturnsTrueAndOutputsValue()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

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
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

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
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        int value = dictionary["c"];

        // Assert
        await Assert.That(value).IsEqualTo(3);
    }

    [Test]
    public async Task Keys_WhenDictionaryHasThreeItems_ThenResultContainsAllKeys()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IEnumerable<string> keys = dictionary.Keys;

        // Assert
        await Assert.That(keys).IsEquivalentTo(["a", "b", "c"]);
    }

    [Test]
    public async Task Values_WhenDictionaryHasThreeItems_ThenResultContainsAllValues()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IEnumerable<int> values = dictionary.Values;

        // Assert
        await Assert.That(values).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Enumerator_WhenDictionaryIsEmpty_ThenNoElementsAreEnumerated()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int>())
                .ToImmutableDictionaryWithValueEquality();

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
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

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

    [Test]
    public async Task Clear_WhenDictionaryHasThreeItems_ThenResultIsEmptyAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.Clear();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(0);
            await Assert.That(dictionary.Count).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Add_WhenKeyIsNew_ThenResultHasNewKeyAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.Add(key: "d", value: 4);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(4);
            await Assert.That(result["d"]).IsEqualTo(4);
            await Assert.That(dictionary.Count).IsEqualTo(3);
            await Assert.That(dictionary.ContainsKey("d")).IsFalse();
        }
    }

    [Test]
    public async Task Add_WhenKeyExistsWithDifferentValue_ThenThrowsArgumentException()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act & Assert
        await Assert.That(() => dictionary.Add(key: "a", value: 999)).Throws<ArgumentException>();
    }

    [Test]
    public async Task AddRange_WhenPairsAreNew_ThenResultHasAllPairsAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result =
            dictionary.AddRange(
                [new KeyValuePair<string, int>(key: "d", value: 4), new KeyValuePair<string, int>(key: "e", value: 5)]);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(5);
            await Assert.That(result["d"]).IsEqualTo(4);
            await Assert.That(result["e"]).IsEqualTo(5);
            await Assert.That(dictionary.Count).IsEqualTo(3);
        }
    }

    [Test]
    public async Task SetItem_WhenKeyIsNew_ThenResultHasNewKeyAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.SetItem(key: "d", value: 4);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(4);
            await Assert.That(result["d"]).IsEqualTo(4);
            await Assert.That(dictionary.Count).IsEqualTo(3);
            await Assert.That(dictionary.ContainsKey("d")).IsFalse();
        }
    }

    [Test]
    public async Task SetItem_WhenKeyExists_ThenResultHasOverwrittenValueAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.SetItem(key: "a", value: 100);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(3);
            await Assert.That(result["a"]).IsEqualTo(100);
            await Assert.That(dictionary["a"]).IsEqualTo(1);
        }
    }

    [Test]
    public async Task SetItems_WhenItemsIncludeNewAndExistingKeys_ThenResultIsUpdatedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result =
            dictionary.SetItems(
            [
                new KeyValuePair<string, int>(key: "a", value: 111), new KeyValuePair<string, int>(key: "d", value: 4)
            ]);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(4);
            await Assert.That(result["a"]).IsEqualTo(111);
            await Assert.That(result["d"]).IsEqualTo(4);
            await Assert.That(dictionary["a"]).IsEqualTo(1);
            await Assert.That(dictionary.ContainsKey("d")).IsFalse();
        }
    }

    [Test]
    public async Task RemoveRange_WhenKeysExist_ThenResultHasKeysRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.RemoveRange(["a", "b"]);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(1);
            await Assert.That(result.ContainsKey("c")).IsTrue();
            await Assert.That(dictionary.Count).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Remove_WhenKeyExists_ThenResultHasKeyRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.Remove("b");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(2);
            await Assert.That(result.ContainsKey("b")).IsFalse();
            await Assert.That(dictionary.Count).IsEqualTo(3);
            await Assert.That(dictionary.ContainsKey("b")).IsTrue();
        }
    }

    [Test]
    public async Task Remove_WhenKeyIsAbsent_ThenResultHasSameContentsAsOriginal()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionaryWithValueEquality<string, int> result = dictionary.Remove("z");

        // Assert
        await Assert.That(result.Equals(dictionary)).IsTrue();
    }

    [Test]
    public async Task Contains_WhenPairMatches_ThenReturnsTrue()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool contains = dictionary.Contains(new KeyValuePair<string, int>(key: "a", value: 1));

        // Assert
        await Assert.That(contains).IsTrue();
    }

    [Test]
    public async Task Contains_WhenKeyIsPresentButValueDiffers_ThenReturnsFalse()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool contains = dictionary.Contains(new KeyValuePair<string, int>(key: "a", value: 999));

        // Assert
        await Assert.That(contains).IsFalse();
    }

    [Test]
    public async Task Contains_WhenKeyIsAbsent_ThenReturnsFalse()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool contains = dictionary.Contains(new KeyValuePair<string, int>(key: "z", value: 1));

        // Assert
        await Assert.That(contains).IsFalse();
    }

    [Test]
    public async Task TryGetKey_WhenKeyExists_ThenReturnsTrueAndOutputsKey()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool found = dictionary.TryGetKey(equalKey: "a", actualKey: out string actualKey);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(actualKey).IsEqualTo("a");
        }
    }

    [Test]
    public async Task TryGetKey_WhenKeyIsAbsent_ThenReturnsFalse()
    {
        // Arrange
        IImmutableDictionaryWithValueEquality<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        bool found = dictionary.TryGetKey(equalKey: "z", actualKey: out string _);

        // Assert
        await Assert.That(found).IsFalse();
    }

    [Test]
    public async Task ImmutableDictionaryAdd_WhenCalledThroughBaseInterface_ThenResultHasNewKey()
    {
        // Arrange
        IImmutableDictionary<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionary<string, int> result = dictionary.Add(key: "d", value: 4);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.Count).IsEqualTo(4);
            await Assert.That(result["d"]).IsEqualTo(4);
            await Assert.That(dictionary.Count).IsEqualTo(3);
        }
    }

    [Test]
    public async Task ImmutableDictionarySetItem_WhenCalledThroughBaseInterface_ThenResultHasOverwrittenValue()
    {
        // Arrange
        IImmutableDictionary<string, int> dictionary =
            ImmutableDictionary
                .CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 })
                .ToImmutableDictionaryWithValueEquality();

        // Act
        IImmutableDictionary<string, int> result = dictionary.SetItem(key: "a", value: 100);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result["a"]).IsEqualTo(100);
            await Assert.That(dictionary["a"]).IsEqualTo(1);
        }
    }

    private record Record(IImmutableDictionaryWithValueEquality<string, int> Dictionary);
}