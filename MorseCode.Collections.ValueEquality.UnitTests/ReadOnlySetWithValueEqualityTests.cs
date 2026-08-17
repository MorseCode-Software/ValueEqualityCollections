using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using TUnit.Mocks;
using TUnit.Mocks.Arguments;
using TUnit.Mocks.Generated;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class ReadOnlySetWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenSetIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IReadOnlySetWithValueEquality<int> set = [];

        // Assert
        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task CollectionExpression_WhenSetHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange

        // Act
        IReadOnlySetWithValueEquality<int> set = [1, 2, 3];

        // Assert
        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task ToReadOnlySetWithValueEquality_WhenSetIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        IReadOnlySet<int> setWithoutValueEquality = new HashSet<int>();

        // Act
        IReadOnlySetWithValueEquality<int> set = setWithoutValueEquality.ToReadOnlySetWithValueEquality();

        // Assert
        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ToReadOnlySetWithValueEquality_WhenSetHasThreeItems_ThenResultHasSameThreeItems()
    {
        // Arrange
        IReadOnlySet<int> setWithoutValueEquality = new HashSet<int> { 1, 2, 3 };

        // Act
        IReadOnlySetWithValueEquality<int> set = setWithoutValueEquality.ToReadOnlySetWithValueEquality();

        // Assert
        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Equals_WhenSetsHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlySetWithValueEquality<int> set1 = [1, 2, 3];
        IReadOnlySetWithValueEquality<int> set2 = [1, 2];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenSetsHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlySetWithValueEquality<int> set1 = [1, 2, 3];
        IReadOnlySetWithValueEquality<int> set2 = [3, 4, 5];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenSetsHaveSameElementsInDifferentOrder_ThenReturnsTrue()
    {
        // Arrange
        IReadOnlySetWithValueEquality<int> set1 = [1, 2, 3];
        IReadOnlySetWithValueEquality<int> set2 = [2, 1, 3];

        // Act
        bool areEqual = set1.Equals(set2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Equals_WhenSetsHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IReadOnlySetWithValueEquality<int> set1 = [1, 2, 3];
        IReadOnlySetWithValueEquality<int> set2 = [1, 2, 3];

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.Count.Returns(3);
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.Contains(Arg.Any<int>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.IsProperSubsetOf(Arg.Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.IsProperSupersetOf(Arg.Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.IsSubsetOf(Arg.Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.IsSupersetOf(Arg.Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.Overlaps(Arg.Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.SetEquals(Arg.Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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
        IReadOnlySet_T_Mock<int> mock = IReadOnlySet<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IReadOnlySetWithValueEquality<int> set = mock.ToReadOnlySetWithValueEquality();

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

    private record Record(IReadOnlySetWithValueEquality<int> Set);
}