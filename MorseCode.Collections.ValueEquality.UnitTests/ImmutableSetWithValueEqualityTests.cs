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

public class ImmutableSetWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenSetIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableSetWithValueEquality<int> set = [];

        // Assert
        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task CollectionExpression_WhenSetHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange

        // Act
        IImmutableSetWithValueEquality<int> set = [1, 2, 3];

        // Assert
        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task ToImmutableSetWithValueEquality_WhenSetIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        IImmutableSet<int> setWithoutValueEquality = ImmutableHashSet.Create<int>();

        // Act
        IImmutableSetWithValueEquality<int> set = setWithoutValueEquality.ToImmutableSetWithValueEquality();

        // Assert
        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToImmutableSetWithValueEquality_WhenSetHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        IImmutableSet<int> setWithoutValueEquality = ImmutableHashSet.Create(1, 2, 3);

        // Act
        IImmutableSetWithValueEquality<int> set = setWithoutValueEquality.ToImmutableSetWithValueEquality();

        // Assert
        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Equals_WhenSetsHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        IImmutableSetWithValueEquality<int> set1 = [1, 2, 3];
        IImmutableSetWithValueEquality<int> set2 = [1, 2];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenSetsHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IImmutableSetWithValueEquality<int> set1 = [1, 2, 3];
        IImmutableSetWithValueEquality<int> set2 = [3, 4, 5];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenSetsHaveSameElementsInDifferentOrder_ThenReturnsTrue()
    {
        // Arrange
        IImmutableSetWithValueEquality<int> set1 = [1, 2, 3];
        IImmutableSetWithValueEquality<int> set2 = [2, 1, 3];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Equals_WhenSetsHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IImmutableSetWithValueEquality<int> set1 = [1, 2, 3];
        IImmutableSetWithValueEquality<int> set2 = [1, 2, 3];

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
    public async Task Count_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Count.Returns(3);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        int count = set.Count;

        // Assert
        await Assert.That(count).IsEqualTo(3);
        mock.Count.WasCalled(Times.Once);
    }

    [Test]
    public async Task Contains_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Contains(Arg.Any<int>()).Returns(true);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        bool contains = set.Contains(2);

        // Assert
        await Assert.That(contains).IsTrue();
        mock.Contains(2).WasCalled(Times.Once);
    }

    [Test]
    public async Task IsProperSubsetOf_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [1, 2, 3];
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.IsProperSubsetOf(Arg.Any<IEnumerable<int>>()).Returns(true);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        bool isProperSubset = set.IsProperSubsetOf(other);

        // Assert
        await Assert.That(isProperSubset).IsTrue();
        mock.IsProperSubsetOf(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task IsProperSupersetOf_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [1, 2];
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.IsProperSupersetOf(Arg.Any<IEnumerable<int>>()).Returns(true);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        bool isProperSuperset = set.IsProperSupersetOf(other);

        // Assert
        await Assert.That(isProperSuperset).IsTrue();
        mock.IsProperSupersetOf(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task IsSubsetOf_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [1, 2, 3];
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.IsSubsetOf(Arg.Any<IEnumerable<int>>()).Returns(true);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        bool isSubset = set.IsSubsetOf(other);

        // Assert
        await Assert.That(isSubset).IsTrue();
        mock.IsSubsetOf(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task IsSupersetOf_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [1, 2];
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.IsSupersetOf(Arg.Any<IEnumerable<int>>()).Returns(true);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        bool isSuperset = set.IsSupersetOf(other);

        // Assert
        await Assert.That(isSuperset).IsTrue();
        mock.IsSupersetOf(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task Overlaps_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [3, 4, 5];
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Overlaps(Arg.Any<IEnumerable<int>>()).Returns(true);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        bool overlaps = set.Overlaps(other);

        // Assert
        await Assert.That(overlaps).IsTrue();
        mock.Overlaps(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task SetEquals_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [3, 2, 1];
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.SetEquals(Arg.Any<IEnumerable<int>>()).Returns(true);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        bool setEquals = set.SetEquals(other);

        // Assert
        await Assert.That(setEquals).IsTrue();
        mock.SetEquals(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task Enumerator_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        List<int> items = [1, 2, 3];
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in set)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEquivalentTo(items);
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    [Test]
    public async Task Clear_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        IImmutableSet<int> returnedSet = ImmutableHashSet.Create<int>();
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Clear().Returns(returnedSet);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        IImmutableSetWithValueEquality<int> result = set.Clear();

        // Assert
        await Assert.That(result).IsEmpty();
        mock.Clear().WasCalled(Times.Once);
    }

    [Test]
    public async Task Add_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        IImmutableSet<int> returnedSet = ImmutableHashSet.Create(1, 2, 3, 4);
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Add(Arg.Any<int>()).Returns(returnedSet);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        IImmutableSetWithValueEquality<int> result = set.Add(4);

        // Assert
        await Assert.That(result).IsEquivalentTo(returnedSet);
        mock.Add(4).WasCalled(Times.Once);
    }

    [Test]
    public async Task Remove_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        IImmutableSet<int> returnedSet = ImmutableHashSet.Create(1, 3);
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Remove(Arg.Any<int>()).Returns(returnedSet);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        IImmutableSetWithValueEquality<int> result = set.Remove(2);

        // Assert
        await Assert.That(result).IsEquivalentTo(returnedSet);
        mock.Remove(2).WasCalled(Times.Once);
    }

    [Test]
    public async Task Intersect_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [3, 4, 5, 6];
        IImmutableSet<int> returnedSet = ImmutableHashSet.Create(3, 4);
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Intersect(Arg.Any<IEnumerable<int>>()).Returns(returnedSet);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        IImmutableSetWithValueEquality<int> result = set.Intersect(other);

        // Assert
        await Assert.That(result).IsEquivalentTo(returnedSet);
        mock.Intersect(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task Except_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [3, 4];
        IImmutableSet<int> returnedSet = ImmutableHashSet.Create(1, 2);
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Except(Arg.Any<IEnumerable<int>>()).Returns(returnedSet);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        IImmutableSetWithValueEquality<int> result = set.Except(other);

        // Assert
        await Assert.That(result).IsEquivalentTo(returnedSet);
        mock.Except(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task SymmetricExcept_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [2, 3, 4];
        IImmutableSet<int> returnedSet = ImmutableHashSet.Create(1, 4);
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.SymmetricExcept(Arg.Any<IEnumerable<int>>()).Returns(returnedSet);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        IImmutableSetWithValueEquality<int> result = set.SymmetricExcept(other);

        // Assert
        await Assert.That(result).IsEquivalentTo(returnedSet);
        mock.SymmetricExcept(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task Union_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        int[] other = [2, 3];
        IImmutableSet<int> returnedSet = ImmutableHashSet.Create(1, 2, 3);
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Union(Arg.Any<IEnumerable<int>>()).Returns(returnedSet);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        IImmutableSetWithValueEquality<int> result = set.Union(other);

        // Assert
        await Assert.That(result).IsEquivalentTo(returnedSet);
        mock.Union(other).WasCalled(Times.Once);
    }

    [Test]
    public async Task TryGetValue_WhenCalled_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.TryGetValue(Arg.Any<int>()).Returns(true);
        IImmutableSetWithValueEquality<int> set = mock.ToImmutableSetWithValueEquality();

        // Act
        bool found = set.TryGetValue(equalValue: 2, actualValue: out int _);

        // Assert
        await Assert.That(found).IsTrue();
        mock.TryGetValue(2).WasCalled(Times.Once);
    }

    [Test]
    public async Task Add_WhenCalledThroughBaseImmutableSetInterface_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        IImmutableSet<int> returnedSet = ImmutableHashSet.Create(1, 2, 3, 4);
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Add(Arg.Any<int>()).Returns(returnedSet);
        IImmutableSetWithValueEquality<int> setWithValueEquality = mock.ToImmutableSetWithValueEquality();
        IImmutableSet<int> baseSet = setWithValueEquality;

        // Act
        IImmutableSet<int> result = baseSet.Add(4);

        // Assert
        await Assert.That(result).IsEquivalentTo(returnedSet);
        mock.Add(4).WasCalled(Times.Once);
    }

    [Test]
    public async Task Contains_WhenCalledThroughBaseImmutableSetInterface_ThenCallIsPassedThroughToUnderlyingSet()
    {
        // Arrange
        IImmutableSet_T_Mock<int> mock = IImmutableSet<int>.Mock();
        mock.Contains(Arg.Any<int>()).Returns(true);
        IImmutableSetWithValueEquality<int> setWithValueEquality = mock.ToImmutableSetWithValueEquality();
        IImmutableSet<int> baseSet = setWithValueEquality;

        // Act
        bool contains = baseSet.Contains(2);

        // Assert
        await Assert.That(contains).IsTrue();
        mock.Contains(2).WasCalled(Times.Once);
    }

    private record Record(IImmutableSetWithValueEquality<int> Set);
}