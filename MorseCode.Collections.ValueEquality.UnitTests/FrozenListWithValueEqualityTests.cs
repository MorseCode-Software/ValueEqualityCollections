using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class FrozenListWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenListIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenListWithValueEquality<int> list = [];

        // Assert
        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task CollectionExpression_WhenListHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange

        // Act
        IFrozenListWithValueEquality<int> list = [1, 2, 3];

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
    public async Task ToFrozenListWithValueEquality_WhenListIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IEnumerable<int> listWithoutValueEquality = [];

        // Act
        IFrozenListWithValueEquality<int> list = listWithoutValueEquality.ToFrozenListWithValueEquality();

        // Assert
        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToFrozenListWithValueEquality_WhenListHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        IEnumerable<int> listWithoutValueEquality = [1, 2, 3];

        // Act
        IFrozenListWithValueEquality<int> list = listWithoutValueEquality.ToFrozenListWithValueEquality();

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
        IFrozenListWithValueEquality<int> list1 = [1, 2, 3];
        IFrozenListWithValueEquality<int> list2 = [1, 2];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IFrozenListWithValueEquality<int> list1 = [1, 2, 3];
        IFrozenListWithValueEquality<int> list2 = [3, 4, 5];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        IFrozenListWithValueEquality<int> list1 = [1, 2, 3];
        IFrozenListWithValueEquality<int> list2 = [2, 1, 3];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IFrozenListWithValueEquality<int> list1 = [1, 2, 3];
        IFrozenListWithValueEquality<int> list2 = [1, 2, 3];

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
        IFrozenListWithValueEquality<int> list = [];

        // Act
        int count = list.Count;

        // Assert
        await Assert.That(count).IsEqualTo(0);
    }

    [Test]
    public async Task Count_WhenListHasThreeItems_ThenResultIs3()
    {
        // Arrange
        IFrozenListWithValueEquality<int> list = [1, 2, 3];

        // Act
        int count = list.Count;

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Indexer_WhenListHasSecondItemOf4_ThenIndex1Is4()
    {
        // Arrange
        IFrozenListWithValueEquality<int> list = [3, 4, 5];

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
        IFrozenListWithValueEquality<int> list = [];

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
        IFrozenListWithValueEquality<int> list = [1, 2, 3];

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

    private record Record(IFrozenListWithValueEquality<int> List);
}