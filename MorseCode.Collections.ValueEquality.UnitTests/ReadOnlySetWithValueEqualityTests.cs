using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using TUnit.Mocks;
using TUnit.Mocks.Arguments;
using TUnit.Mocks.Generated;
using static TUnit.Mocks.Arguments.Arg;

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.Count.Returns(3);
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.Contains(Any<int>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.IsProperSubsetOf(Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.IsProperSupersetOf(Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.IsSubsetOf(Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.IsSupersetOf(Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.Overlaps(Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.SetEquals(Any<IEnumerable<int>>()).Returns(true);
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
        Mock<IReadOnlySet<int>> mock = Mock.Of<IReadOnlySet<int>>();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IReadOnlySetWithValueEquality<int> set = mock.Object.ToReadOnlySetWithValueEquality();

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
