using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using TUnit.Mocks;
using TUnit.Mocks.Arguments;
using TUnit.Mocks.Generated;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class ReadOnlyListWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenListIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IReadOnlyListWithValueEquality<int> list = [];

        // Assert
        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task CollectionExpression_WhenListHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange

        // Act
        IReadOnlyListWithValueEquality<int> list = [1, 2, 3];

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
    public async Task ToReadOnlyListWithValueEquality_WhenListIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IReadOnlyList<int> listWithoutValueEquality = [];

        // Act
        IReadOnlyListWithValueEquality<int> list = listWithoutValueEquality.ToReadOnlyListWithValueEquality();

        // Assert
        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToReadOnlyListWithValueEquality_WhenListHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        IReadOnlyList<int> listWithoutValueEquality = [1, 2, 3];

        // Act
        IReadOnlyListWithValueEquality<int> list = listWithoutValueEquality.ToReadOnlyListWithValueEquality();

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
        IReadOnlyListWithValueEquality<int> list1 = [1, 2, 3];
        IReadOnlyListWithValueEquality<int> list2 = [1, 2];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyListWithValueEquality<int> list1 = [1, 2, 3];
        IReadOnlyListWithValueEquality<int> list2 = [3, 4, 5];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyListWithValueEquality<int> list1 = [1, 2, 3];
        IReadOnlyListWithValueEquality<int> list2 = [2, 1, 3];

        // Act
        bool areEqual = list1.Equals(list2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenListsHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IReadOnlyListWithValueEquality<int> list1 = [1, 2, 3];
        IReadOnlyListWithValueEquality<int> list2 = [1, 2, 3];

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
    public async Task Count_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IReadOnlyList_T_Mock<int> mock = IReadOnlyList<int>.Mock();
        mock.Count.Returns(3);
        IReadOnlyListWithValueEquality<int> list = mock.ToReadOnlyListWithValueEquality();

        // Act
        int count = list.Count;

        // Assert
        await Assert.That(count).IsEqualTo(3);
        mock.Count.WasCalled(Times.Once);
    }

    [Test]
    public async Task Indexer_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IReadOnlyList_T_Mock<int> mock = IReadOnlyList<int>.Mock();
        mock.Item(Arg.Any<int>()).Returns(4);
        IReadOnlyListWithValueEquality<int> list = mock.ToReadOnlyListWithValueEquality();

        // Act
        int item = list[1];

        // Assert
        await Assert.That(item).IsEqualTo(4);
        mock.Item(1).WasCalled(Times.Once);
    }

    [Test]
    public async Task Enumerator_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        List<int> items = [1, 2, 3];
        IReadOnlyList_T_Mock<int> mock = IReadOnlyList<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IReadOnlyListWithValueEquality<int> list = mock.ToReadOnlyListWithValueEquality();

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in list)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEquivalentTo(items);
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    private record Record(IReadOnlyListWithValueEquality<int> List);
}