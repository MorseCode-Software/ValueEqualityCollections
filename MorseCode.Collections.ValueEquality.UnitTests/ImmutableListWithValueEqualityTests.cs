using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class ImmutableListWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenListIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableListWithValueEquality<int> list = [];

        // Assert
        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task CollectionExpression_WhenListHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange

        // Act
        IImmutableListWithValueEquality<int> list = [1, 2, 3];

        // Assert
        await Assert.That(list.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(list[0]).IsEqualTo(1);
            await Assert.That(list[1]).IsEqualTo(2);
            await Assert.That(list[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task ToImmutableListWithValueEquality_WhenListIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        IImmutableList<int> listWithoutValueEquality = ImmutableList<int>.Empty;

        // Act
        IImmutableListWithValueEquality<int> list = listWithoutValueEquality.ToImmutableListWithValueEquality();

        // Assert
        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToImmutableListWithValueEquality_WhenListHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        IImmutableList<int> listWithoutValueEquality = ImmutableList.Create(1, 2, 3);

        // Act
        IImmutableListWithValueEquality<int> list = listWithoutValueEquality.ToImmutableListWithValueEquality();

        // Assert
        await Assert.That(list.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(list[0]).IsEqualTo(1);
            await Assert.That(list[1]).IsEqualTo(2);
            await Assert.That(list[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Equals_WhenListsHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list1 = [1, 2, 3];
        IImmutableListWithValueEquality<int> list2 = [1, 2];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list1 = [1, 2, 3];
        IImmutableListWithValueEquality<int> list2 = [3, 4, 5];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list1 = [1, 2, 3];
        IImmutableListWithValueEquality<int> list2 = [2, 1, 3];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list1 = [1, 2, 3];
        IImmutableListWithValueEquality<int> list2 = [1, 2, 3];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task RecordEquals_WhenListsHaveDifferentNumberElements_ThenReturnsFalse()
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
    public async Task RecordEquals_WhenListsHaveDifferentElements_ThenReturnsFalse()
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
    public async Task RecordEquals_WhenListsHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        Record record1 = new([1, 2, 3]);
        Record record2 = new([2, 1, 3]);

        // Act
        bool areEqual = record1 == record2;

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task RecordEquals_WhenListsHaveSameElementsInSameOrder_ThenReturnsTrue()
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
    public async Task Count_WhenListIsEmpty_ThenResultIs0()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableListWithValueEquality<int> list = [];

        // Act
        int count = list.Count;

        // Assert
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task Count_WhenListHasThreeItems_ThenResultIs3()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [1, 2, 3];

        // Act
        int count = list.Count;

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Indexer_WhenListHasSecondItemOf4_ThenIndex1Is4()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [3, 4, 5];

        // Act
        int item = list[1];

        // Assert
        await Assert.That(item).IsEqualTo(4);
    }

    [Test]
    public async Task Enumerator_WhenListIsEmpty_ThenNoElementsAreEnumerated()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableListWithValueEquality<int> list = [];

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in list)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task Enumerator_WhenListHasThreeItems_ThenSameThreeElementsAreEnumerated()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableListWithValueEquality<int> list = [1, 2, 3];

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in list)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Clear_WhenListHasItems_ThenResultIsEmptyAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.Clear();

        // Assert
        await Assert.That(result.Count).IsEqualTo(0);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Add_WhenAddingValue_ThenResultHasValueAppendedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.Add(4);

        // Assert
        await Assert.That(result.Count).IsEqualTo(4);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(2);
            await Assert.That(result[2]).IsEqualTo(3);
            await Assert.That(result[3]).IsEqualTo(4);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task AddRange_WhenAddingValues_ThenResultHasValuesAppendedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.AddRange([4, 5]);

        // Assert
        await Assert.That(result.Count).IsEqualTo(5);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(2);
            await Assert.That(result[2]).IsEqualTo(3);
            await Assert.That(result[3]).IsEqualTo(4);
            await Assert.That(result[4]).IsEqualTo(5);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Insert_WhenInsertingValueAtIndex_ThenResultHasValueInsertedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.Insert(index: 1, element: 99);

        // Assert
        await Assert.That(result.Count).IsEqualTo(4);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(99);
            await Assert.That(result[2]).IsEqualTo(2);
            await Assert.That(result[3]).IsEqualTo(3);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task InsertRange_WhenInsertingValuesAtIndex_ThenResultHasValuesInsertedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.InsertRange(index: 1, items: [97, 98]);

        // Assert
        await Assert.That(result.Count).IsEqualTo(5);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(97);
            await Assert.That(result[2]).IsEqualTo(98);
            await Assert.That(result[3]).IsEqualTo(2);
            await Assert.That(result[4]).IsEqualTo(3);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Remove_WithEqualityComparer_WhenValueExists_ThenResultHasValueRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result =
            originalList.Remove(value: 2, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(result.Count).IsEqualTo(2);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(3);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Remove_WithEqualityComparer_WhenValueDoesNotExist_ThenResultIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [10, 20, 30];

        // Act
        IImmutableListWithValueEquality<int> result =
            originalList.Remove(value: 99, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(10);
            await Assert.That(result[1]).IsEqualTo(20);
            await Assert.That(result[2]).IsEqualTo(30);
        }
    }

    [Test]
    public async Task Remove_WithoutEqualityComparer_WhenValueExists_ThenResultHasValueRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.Remove(2);

        // Assert
        await Assert.That(result.Count).IsEqualTo(2);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(3);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task
        RemoveAll_WhenPredicateMatchesSomeValues_ThenResultHasMatchingValuesRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3, 4, 5, 6];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.RemoveAll(x => x % 2 == 0);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(originalList.Count).IsEqualTo(6);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(3);
            await Assert.That(result[2]).IsEqualTo(5);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
            await Assert.That(originalList[3]).IsEqualTo(4);
            await Assert.That(originalList[4]).IsEqualTo(5);
            await Assert.That(originalList[5]).IsEqualTo(6);
        }
    }

    [Test]
    public async Task
        RemoveRange_WithEqualityComparerAndValues_WhenValuesExist_ThenResultHasValuesRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [10, 20, 30, 40, 50];

        // Act
        IImmutableListWithValueEquality<int> result =
            originalList.RemoveRange(items: [20, 40], equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(originalList.Count).IsEqualTo(5);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(10);
            await Assert.That(result[1]).IsEqualTo(30);
            await Assert.That(result[2]).IsEqualTo(50);
            await Assert.That(originalList[0]).IsEqualTo(10);
            await Assert.That(originalList[1]).IsEqualTo(20);
            await Assert.That(originalList[2]).IsEqualTo(30);
            await Assert.That(originalList[3]).IsEqualTo(40);
            await Assert.That(originalList[4]).IsEqualTo(50);
        }
    }

    [Test]
    public async Task
        RemoveRange_WithoutEqualityComparerAndValues_WhenValuesExist_ThenResultHasValuesRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [10, 20, 30, 40, 50];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.RemoveRange([20, 40]);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(originalList.Count).IsEqualTo(5);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(10);
            await Assert.That(result[1]).IsEqualTo(30);
            await Assert.That(result[2]).IsEqualTo(50);
            await Assert.That(originalList[0]).IsEqualTo(10);
            await Assert.That(originalList[1]).IsEqualTo(20);
            await Assert.That(originalList[2]).IsEqualTo(30);
            await Assert.That(originalList[3]).IsEqualTo(40);
            await Assert.That(originalList[4]).IsEqualTo(50);
        }
    }

    [Test]
    public async Task RemoveRange_WithIndexAndCount_ThenResultHasRangeRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3, 4, 5];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.RemoveRange(index: 1, count: 2);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(originalList.Count).IsEqualTo(5);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(4);
            await Assert.That(result[2]).IsEqualTo(5);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
            await Assert.That(originalList[3]).IsEqualTo(4);
            await Assert.That(originalList[4]).IsEqualTo(5);
        }
    }

    [Test]
    public async Task RemoveAt_WhenRemovingValueAtIndex_ThenResultHasValueRemovedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.RemoveAt(1);

        // Assert
        await Assert.That(result.Count).IsEqualTo(2);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(3);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task SetItem_WhenSettingValueAtIndex_ThenResultHasValueReplacedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.SetItem(index: 1, value: 99);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(99);
            await Assert.That(result[2]).IsEqualTo(3);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task
        Replace_WithEqualityComparer_WhenOldValueExists_ThenResultHasValueReplacedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result =
            originalList.Replace(oldValue: 2, newValue: 99, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(99);
            await Assert.That(result[2]).IsEqualTo(3);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Replace_WithEqualityComparer_WhenOldValueDoesNotExist_ThenThrowsArgumentException()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [1, 2, 3];

        // Act & Assert
        await Assert
            .That(() => list.Replace(oldValue: 99, newValue: 100, equalityComparer: EqualityComparer<int>.Default))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task
        Replace_WithoutEqualityComparer_WhenOldValueExists_ThenResultHasValueReplacedAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableListWithValueEquality<int> originalList = [1, 2, 3];

        // Act
        IImmutableListWithValueEquality<int> result = originalList.Replace(oldValue: 2, newValue: 99);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(originalList.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(1);
            await Assert.That(result[1]).IsEqualTo(99);
            await Assert.That(result[2]).IsEqualTo(3);
            await Assert.That(originalList[0]).IsEqualTo(1);
            await Assert.That(originalList[1]).IsEqualTo(2);
            await Assert.That(originalList[2]).IsEqualTo(3);
        }
    }

    [Test]
    public async Task Replace_WithoutEqualityComparer_WhenOldValueDoesNotExist_ThenThrowsArgumentException()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [1, 2, 3];

        // Act & Assert
        await Assert.That(() => list.Replace(oldValue: 99, newValue: 100)).Throws<ArgumentException>();
    }

    [Test]
    public async Task IndexOf_WhenValueExists_ThenReturnsFirstIndex()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.IndexOf(5);

        // Assert
        await Assert.That(index).IsEqualTo(0);
    }

    [Test]
    public async Task IndexOf_WhenValueDoesNotExist_ThenReturnsMinusOne()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.IndexOf(99);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_WithStartIndex_WhenValueExistsAtOrAfterStartIndex_ThenReturnsIndex()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.IndexOf(item: 5, startIndex: 1);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task IndexOf_WithStartIndex_WhenValueOnlyExistsBeforeStartIndex_ThenReturnsMinusOne()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 8];

        // Act
        int index = list.IndexOf(item: 5, startIndex: 1);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_WithStartIndexAndCount_WhenValueExistsWithinRange_ThenReturnsIndex()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.IndexOf(item: 5, startIndex: 2, count: 2);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task IndexOf_WithStartIndexAndCount_WhenValueExistsOutsideRange_ThenReturnsMinusOne()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.IndexOf(item: 5, startIndex: 3, count: 1);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_WithIndexCountAndEqualityComparer_WhenValueExistsWithinRange_ThenReturnsIndex()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.IndexOf(item: 5, index: 2, count: 2, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task LastIndexOf_WhenValueExistsMultipleTimes_ThenReturnsLastIndex()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.LastIndexOf(5);

        // Assert
        await Assert.That(index).IsEqualTo(4);
    }

    [Test]
    public async Task LastIndexOf_WithStartIndex_WhenValueExistsAtOrBeforeStartIndex_ThenReturnsIndex()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.LastIndexOf(item: 5, startIndex: 2);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task LastIndexOf_WithStartIndexAndCount_WhenValueExistsWithinRange_ThenReturnsIndex()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.LastIndexOf(item: 5, startIndex: 4, count: 2);

        // Assert
        await Assert.That(index).IsEqualTo(4);
    }

    [Test]
    public async Task LastIndexOf_WithIndexCountAndEqualityComparer_WhenValueExistsWithinRange_ThenReturnsIndex()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [5, 2, 5, 8, 5];

        // Act
        int index = list.LastIndexOf(item: 5, index: 4, count: 2, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(index).IsEqualTo(4);
    }

    [Test]
    public async Task BaseImmutableListInterface_Add_WhenAddingValue_ThenResultHasValueAppended()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [1, 2, 3];
        IImmutableList<int> baseList = list;

        // Act
        IImmutableList<int> result = baseList.Add(4);

        // Assert
        List<int> resultItems = [.. result];
        await Assert.That(resultItems).IsEquivalentTo([1, 2, 3, 4]);
    }

    [Test]
    public async Task BaseImmutableListInterface_Remove_WhenValueExists_ThenResultHasValueRemoved()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [1, 2, 3];
        IImmutableList<int> baseList = list;

        // Act
        IImmutableList<int> result = baseList.Remove(value: 2, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        List<int> resultItems = [.. result];
        await Assert.That(resultItems).IsEquivalentTo([1, 3]);
    }

    [Test]
    public async Task BaseImmutableListInterface_Clear_WhenListHasValues_ThenResultIsEmpty()
    {
        // Arrange
        IImmutableListWithValueEquality<int> list = [1, 2, 3];
        IImmutableList<int> baseList = list;

        // Act
        IImmutableList<int> result = baseList.Clear();

        // Assert
        List<int> resultItems = [.. result];
        await Assert.That(resultItems).IsEmpty();
    }

    private record Record(IImmutableListWithValueEquality<int> List);
}