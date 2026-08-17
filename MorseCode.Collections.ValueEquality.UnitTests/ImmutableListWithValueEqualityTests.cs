using System;
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
    public async Task Count_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Count.Returns(3);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

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
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Item(Arg.Any<int>()).Returns(4);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

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
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

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

    [Test]
    public async Task Clear_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList<int>.Empty;
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Clear().Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.Clear();

        // Assert
        await Assert.That(result.Count).IsEqualTo(0);
        mock.Clear().WasCalled(Times.Once);
    }

    [Test]
    public async Task Add_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 2, 3, 4);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Add(Arg.Any<int>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.Add(4);

        // Assert
        await Assert.That(result.Count).IsEqualTo(4);
        await Assert.That(result[3]).IsEqualTo(4);
        mock.Add(4).WasCalled(Times.Once);
    }

    [Test]
    public async Task AddRange_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        int[] items = [4, 5];
        IImmutableList<int> returnedList = ImmutableList.Create(1, 2, 3, 4, 5);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.AddRange(Arg.Any<IEnumerable<int>>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.AddRange(items);

        // Assert
        await Assert.That(result.Count).IsEqualTo(5);
        mock.AddRange(items).WasCalled(Times.Once);
    }

    [Test]
    public async Task Insert_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 99, 2, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Insert(index: Arg.Any<int>(), element: Arg.Any<int>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.Insert(index: 1, element: 99);

        // Assert
        await Assert.That(result.Count).IsEqualTo(4);
        await Assert.That(result[1]).IsEqualTo(99);
        mock.Insert(index: 1, element: 99).WasCalled(Times.Once);
    }

    [Test]
    public async Task InsertRange_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        int[] items = [97, 98];
        IImmutableList<int> returnedList = ImmutableList.Create(1, 97, 98, 2, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.InsertRange(index: Arg.Any<int>(), items: Arg.Any<IEnumerable<int>>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.InsertRange(index: 1, items: items);

        // Assert
        await Assert.That(result.Count).IsEqualTo(5);
        mock.InsertRange(index: 1, items: items).WasCalled(Times.Once);
    }

    [Test]
    public async Task Remove_WithEqualityComparer_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Remove(value: Arg.Any<int>(), equalityComparer: Arg.Any<IEqualityComparer<int>?>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result =
            list.Remove(value: 2, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(result.Count).IsEqualTo(2);
        mock.Remove(value: 2, equalityComparer: EqualityComparer<int>.Default).WasCalled(Times.Once);
    }

    [Test]
    public async Task Remove_WithoutEqualityComparer_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.Remove(value: Arg.Any<int>(), equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .Returns(returnedList);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.Remove(2);

        // Assert
        await Assert.That(result.Count).IsEqualTo(2);
        mock.Remove(value: 2, equalityComparer: Arg.IsNull<IEqualityComparer<int>?>()).WasCalled(Times.Once);
    }

    [Test]
    public async Task RemoveAll_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 3, 5);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.RemoveAll(Arg.Any<Predicate<int>>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();
        Predicate<int> match = x => x % 2 == 0;

        // Act
        IImmutableListWithValueEquality<int> result = list.RemoveAll(match);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        mock.RemoveAll(match).WasCalled(Times.Once);
    }

    [Test]
    public async Task RemoveRange_WithEqualityComparerAndValues_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        int[] items = [20, 40];
        IImmutableList<int> returnedList = ImmutableList.Create(10, 30, 50);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.RemoveRange(items: Arg.Any<IEnumerable<int>>(), equalityComparer: Arg.Any<IEqualityComparer<int>?>())
            .Returns(returnedList);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result =
            list.RemoveRange(items: items, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        mock.RemoveRange(items: items, equalityComparer: EqualityComparer<int>.Default).WasCalled(Times.Once);
    }

    [Test]
    public async Task
        RemoveRange_WithoutEqualityComparerAndValues_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        int[] items = [20, 40];
        IImmutableList<int> returnedList = ImmutableList.Create(10, 30, 50);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.RemoveRange(items: Arg.Any<IEnumerable<int>>(), equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .Returns(returnedList);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.RemoveRange(items);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        mock.RemoveRange(items: items, equalityComparer: Arg.IsNull<IEqualityComparer<int>?>()).WasCalled(Times.Once);
    }

    [Test]
    public async Task RemoveRange_WithIndexAndCount_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 4, 5);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.RemoveRange(index: Arg.Any<int>(), count: Arg.Any<int>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.RemoveRange(index: 1, count: 2);

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);
        mock.RemoveRange(index: 1, count: 2).WasCalled(Times.Once);
    }

    [Test]
    public async Task RemoveAt_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.RemoveAt(Arg.Any<int>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.RemoveAt(1);

        // Assert
        await Assert.That(result.Count).IsEqualTo(2);
        mock.RemoveAt(1).WasCalled(Times.Once);
    }

    [Test]
    public async Task SetItem_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 99, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.SetItem(index: Arg.Any<int>(), value: Arg.Any<int>()).Returns(returnedList);
        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.SetItem(index: 1, value: 99);

        // Assert
        await Assert.That(result[1]).IsEqualTo(99);
        mock.SetItem(index: 1, value: 99).WasCalled(Times.Once);
    }

    [Test]
    public async Task Replace_WithEqualityComparer_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 99, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.Replace(
                oldValue: Arg.Any<int>(),
                newValue: Arg.Any<int>(),
                equalityComparer: Arg.Any<IEqualityComparer<int>?>())
            .Returns(returnedList);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result =
            list.Replace(oldValue: 2, newValue: 99, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(result[1]).IsEqualTo(99);
        mock.Replace(oldValue: 2, newValue: 99, equalityComparer: EqualityComparer<int>.Default).WasCalled(Times.Once);
    }

    [Test]
    public async Task Replace_WithoutEqualityComparer_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 99, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.Replace(
                oldValue: Arg.Any<int>(),
                newValue: Arg.Any<int>(),
                equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .Returns(returnedList);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableListWithValueEquality<int> result = list.Replace(oldValue: 2, newValue: 99);

        // Assert
        await Assert.That(result[1]).IsEqualTo(99);

        mock.Replace(oldValue: 2, newValue: 99, equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .WasCalled(Times.Once);
    }

    [Test]
    public async Task IndexOf_WithIndexCountAndEqualityComparer_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.IndexOf(
                item: Arg.Any<int>(),
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.Any<IEqualityComparer<int>?>())
            .Returns(2);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        int index = list.IndexOf(item: 5, index: 2, count: 2, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(index).IsEqualTo(2);

        mock.IndexOf(item: 5, index: 2, count: 2, equalityComparer: EqualityComparer<int>.Default)
            .WasCalled(Times.Once);
    }

    [Test]
    public async Task IndexOf_WithStartIndexAndCount_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.IndexOf(
                item: Arg.Any<int>(),
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .Returns(2);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        int index = list.IndexOf(item: 5, startIndex: 2, count: 2);

        // Assert
        await Assert.That(index).IsEqualTo(2);

        mock.IndexOf(item: 5, index: 2, count: 2, equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .WasCalled(Times.Once);
    }

    [Test]
    public async Task IndexOf_WithStartIndex_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Count.Returns(10);

        mock.IndexOf(
                item: Arg.Any<int>(),
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .Returns(4);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        int index = list.IndexOf(item: 5, startIndex: 3);

        // Assert
        await Assert.That(index).IsEqualTo(4);
        mock.Count.WasCalled(Times.AtLeastOnce);

        mock.IndexOf(item: 5, index: 3, count: 7, equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .WasCalled(Times.Once);
    }

    [Test]
    public async Task IndexOf_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Count.Returns(10);

        mock.IndexOf(
                item: Arg.Any<int>(),
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.Any<IEqualityComparer<int>?>())
            .Returns(0);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        int index = list.IndexOf(5);

        // Assert
        await Assert.That(index).IsEqualTo(0);
        mock.Count.WasCalled(Times.AtLeastOnce);

        mock.IndexOf(
                item: 5,
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.Any<IEqualityComparer<int>?>())
            .WasCalled(Times.AtLeastOnce);
    }

    [Test]
    public async Task
        LastIndexOf_WithIndexCountAndEqualityComparer_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.LastIndexOf(
                item: Arg.Any<int>(),
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.Any<IEqualityComparer<int>?>())
            .Returns(4);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        int index = list.LastIndexOf(item: 5, index: 4, count: 2, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        await Assert.That(index).IsEqualTo(4);

        mock.LastIndexOf(item: 5, index: 4, count: 2, equalityComparer: EqualityComparer<int>.Default)
            .WasCalled(Times.Once);
    }

    [Test]
    public async Task LastIndexOf_WithStartIndexAndCount_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();

        mock.LastIndexOf(
                item: Arg.Any<int>(),
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .Returns(4);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        int index = list.LastIndexOf(item: 5, startIndex: 4, count: 2);

        // Assert
        await Assert.That(index).IsEqualTo(4);

        mock.LastIndexOf(item: 5, index: 4, count: 2, equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .WasCalled(Times.Once);
    }

    [Test]
    public async Task LastIndexOf_WithStartIndex_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Count.Returns(5);

        mock.LastIndexOf(
                item: Arg.Any<int>(),
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .Returns(2);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        int index = list.LastIndexOf(item: 5, startIndex: 2);

        // Assert
        await Assert.That(index).IsEqualTo(2);
        mock.Count.WasCalled(Times.AtLeastOnce);

        mock.LastIndexOf(item: 5, index: 2, count: 3, equalityComparer: Arg.IsNull<IEqualityComparer<int>?>())
            .WasCalled(Times.Once);
    }

    [Test]
    public async Task LastIndexOf_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Count.Returns(5);

        mock.LastIndexOf(
                item: Arg.Any<int>(),
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.Any<IEqualityComparer<int>?>())
            .Returns(4);

        IImmutableListWithValueEquality<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        int index = list.LastIndexOf(5);

        // Assert
        await Assert.That(index).IsEqualTo(4);
        mock.Count.WasCalled(Times.AtLeastOnce);

        mock.LastIndexOf(
                item: 5,
                index: Arg.Any<int>(),
                count: Arg.Any<int>(),
                equalityComparer: Arg.Any<IEqualityComparer<int>?>())
            .WasCalled(Times.AtLeastOnce);
    }

    [Test]
    public async Task BaseImmutableListInterface_Add_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 2, 3, 4);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Add(Arg.Any<int>()).Returns(returnedList);
        IImmutableList<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableList<int> result = list.Add(4);

        // Assert
        List<int> resultItems = [.. result];
        await Assert.That(resultItems).IsEquivalentTo([1, 2, 3, 4]);
        mock.Add(4).WasCalled(Times.Once);
    }

    [Test]
    public async Task BaseImmutableListInterface_Remove_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList.Create(1, 3);
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Remove(value: Arg.Any<int>(), equalityComparer: Arg.Any<IEqualityComparer<int>?>()).Returns(returnedList);
        IImmutableList<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableList<int> result = list.Remove(value: 2, equalityComparer: EqualityComparer<int>.Default);

        // Assert
        List<int> resultItems = [.. result];
        await Assert.That(resultItems).IsEquivalentTo([1, 3]);
        mock.Remove(value: 2, equalityComparer: EqualityComparer<int>.Default).WasCalled(Times.Once);
    }

    [Test]
    public async Task BaseImmutableListInterface_Clear_WhenCalled_ThenCallIsPassedThroughToUnderlyingList()
    {
        // Arrange
        IImmutableList<int> returnedList = ImmutableList<int>.Empty;
        IImmutableList_T_Mock<int> mock = IImmutableList<int>.Mock();
        mock.Clear().Returns(returnedList);
        IImmutableList<int> list = mock.ToImmutableListWithValueEquality();

        // Act
        IImmutableList<int> result = list.Clear();

        // Assert
        List<int> resultItems = [.. result];
        await Assert.That(resultItems).IsEmpty();
        mock.Clear().WasCalled(Times.Once);
    }

    private record Record(IImmutableListWithValueEquality<int> List);
}