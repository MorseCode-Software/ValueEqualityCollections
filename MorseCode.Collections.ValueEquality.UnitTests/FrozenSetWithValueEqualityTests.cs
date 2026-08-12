using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class FrozenSetWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenSetIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenSetWithValueEquality<int> set = [];

        // Assert
        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task CollectionExpression_WhenSetHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange

        // Act
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];

        // Assert
        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task ToFrozenSetWithValueEquality_WhenSetIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        FrozenSet<int> setWithoutValueEquality = Array.Empty<int>().ToFrozenSet();

        // Act
        IFrozenSetWithValueEquality<int> set = setWithoutValueEquality.ToFrozenSetWithValueEquality();

        // Assert
        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToFrozenSetWithValueEquality_WhenSetHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        FrozenSet<int> setWithoutValueEquality = new[] { 1, 2, 3 }.ToFrozenSet();

        // Act
        IFrozenSetWithValueEquality<int> set = setWithoutValueEquality.ToFrozenSetWithValueEquality();

        // Assert
        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Equals_WhenSetsHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set1 = [1, 2, 3];
        IFrozenSetWithValueEquality<int> set2 = [1, 2];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenSetsHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set1 = [1, 2, 3];
        IFrozenSetWithValueEquality<int> set2 = [3, 4, 5];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenSetsHaveSameElementsInDifferentOrder_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set1 = [1, 2, 3];
        IFrozenSetWithValueEquality<int> set2 = [2, 1, 3];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Equals_WhenSetsHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set1 = [1, 2, 3];
        IFrozenSetWithValueEquality<int> set2 = [1, 2, 3];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task RecordEquals_WhenSetsHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        Record record1 = new([1, 2, 3]);
        Record record2 = new([1, 2]);

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task RecordEquals_WhenSetsHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        Record record1 = new([1, 2, 3]);
        Record record2 = new([3, 4, 5]);

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task RecordEquals_WhenSetsHaveSameElementsInDifferentOrder_ThenReturnsTrue()
    {
        // Arrange
        Record record1 = new([1, 2, 3]);
        Record record2 = new([2, 1, 3]);

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task RecordEquals_WhenSetsHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        Record record1 = new([1, 2, 3]);
        Record record2 = new([1, 2, 3]);

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Count_WhenSetIsEmpty_ThenResultIs0()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenSetWithValueEquality<int> set = [];

        // Act
        int count = set.Count;

        // Assert
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task Count_WhenSetHasThreeItems_ThenResultIs3()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];

        // Act
        int count = set.Count;

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Contains_WhenItemIsInSet_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];

        // Act
        bool contains = set.Contains(2);

        // Assert
        await Assert.That(contains).IsTrue();
    }

    [Test]
    public async Task Contains_WhenItemIsNotInSet_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];

        // Act
        bool contains = set.Contains(5);

        // Assert
        await Assert.That(contains).IsFalse();
    }

    [Test]
    public async Task IsProperSubsetOf_WhenSetIsProperSubsetOfOther_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2];
        int[] other = [1, 2, 3];

        // Act
        bool isProperSubset = set.IsProperSubsetOf(other);

        // Assert
        await Assert.That(isProperSubset).IsTrue();
    }

    [Test]
    public async Task IsProperSubsetOf_WhenSetIsNotProperSubsetOfOther_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];
        int[] other = [1, 2, 3];

        // Act
        bool isProperSubset = set.IsProperSubsetOf(other);

        // Assert
        await Assert.That(isProperSubset).IsFalse();
    }

    [Test]
    public async Task IsProperSupersetOf_WhenSetIsProperSupersetOfOther_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];
        int[] other = [1, 2];

        // Act
        bool isProperSuperset = set.IsProperSupersetOf(other);

        // Assert
        await Assert.That(isProperSuperset).IsTrue();
    }

    [Test]
    public async Task IsProperSupersetOf_WhenSetIsNotProperSupersetOfOther_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2];
        int[] other = [1, 2, 3];

        // Act
        bool isProperSuperset = set.IsProperSupersetOf(other);

        // Assert
        await Assert.That(isProperSuperset).IsFalse();
    }

    [Test]
    public async Task IsSubsetOf_WhenSetIsSubsetOfOther_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2];
        int[] other = [1, 2, 3];

        // Act
        bool isSubset = set.IsSubsetOf(other);

        // Assert
        await Assert.That(isSubset).IsTrue();
    }

    [Test]
    public async Task IsSubsetOf_WhenSetIsNotSubsetOfOther_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 4];
        int[] other = [1, 2, 3];

        // Act
        bool isSubset = set.IsSubsetOf(other);

        // Assert
        await Assert.That(isSubset).IsFalse();
    }

    [Test]
    public async Task IsSupersetOf_WhenSetIsSupersetOfOther_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];
        int[] other = [1, 2];

        // Act
        bool isSuperset = set.IsSupersetOf(other);

        // Assert
        await Assert.That(isSuperset).IsTrue();
    }

    [Test]
    public async Task IsSupersetOf_WhenSetIsNotSupersetOfOther_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2];
        int[] other = [1, 2, 3];

        // Act
        bool isSuperset = set.IsSupersetOf(other);

        // Assert
        await Assert.That(isSuperset).IsFalse();
    }

    [Test]
    public async Task Overlaps_WhenSetSharesElementWithOther_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];
        int[] other = [3, 4, 5];

        // Act
        bool overlaps = set.Overlaps(other);

        // Assert
        await Assert.That(overlaps).IsTrue();
    }

    [Test]
    public async Task Overlaps_WhenSetSharesNoElementWithOther_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];
        int[] other = [4, 5, 6];

        // Act
        bool overlaps = set.Overlaps(other);

        // Assert
        await Assert.That(overlaps).IsFalse();
    }

    [Test]
    public async Task SetEquals_WhenSetHasSameElementsAsOther_ThenReturnsTrue()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];
        int[] other = [3, 2, 1];

        // Act
        bool setEquals = set.SetEquals(other);

        // Assert
        await Assert.That(setEquals).IsTrue();
    }

    [Test]
    public async Task SetEquals_WhenSetDoesNotHaveSameElementsAsOther_ThenReturnsFalse()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];
        int[] other = [1, 2, 4];

        // Act
        bool setEquals = set.SetEquals(other);

        // Assert
        await Assert.That(setEquals).IsFalse();
    }

    [Test]
    public async Task Enumerator_WhenSetIsEmpty_ThenNoElementsAreEnumerated()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenSetWithValueEquality<int> set = [];

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in set)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task Enumerator_WhenSetHasThreeItems_ThenSameThreeElementsAreEnumerated()
    {
        // Arrange
        IFrozenSetWithValueEquality<int> set = [1, 2, 3];

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in set)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEquivalentTo([1, 2, 3]);
    }

    private record Record(IFrozenSetWithValueEquality<int> Set);
}