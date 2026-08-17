using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using TUnit.Mocks;
using TUnit.Mocks.Arguments;
using TUnit.Mocks.Generated;
using static TUnit.Mocks.Arguments.Arg;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class FrozenQueueWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenQueueIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenQueueWithValueEquality<int> queue = [];

        // Assert
        await Assert.That(queue.IsEmpty).IsTrue();
    }

    [Test]
    public async Task CollectionExpression_WhenQueueHasThreeItems_ThenResultHasSameThreeItemsInOrder()
    {
        // Arrange

        // Act
        IFrozenQueueWithValueEquality<int> queue = [1, 2, 3];

        // Assert
        await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task ToFrozenQueueWithValueEquality_WhenQueueIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        IEnumerable<int> queueWithoutValueEquality = [];

        // Act
        IFrozenQueueWithValueEquality<int> queue = queueWithoutValueEquality.ToFrozenQueueWithValueEquality();

        // Assert
        await Assert.That(queue.IsEmpty).IsTrue();
    }

    [Test]
    public async Task ToFrozenQueueWithValueEquality_WhenQueueHasThreeItems_ThenResultHasSameThreeItemsInOrder()
    {
        // Arrange
        IEnumerable<int> queueWithoutValueEquality = new[] { 1, 2, 3 };

        // Act
        IFrozenQueueWithValueEquality<int> queue = queueWithoutValueEquality.ToFrozenQueueWithValueEquality();

        // Assert
        await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Equals_WhenQueuesHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        IFrozenQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IFrozenQueueWithValueEquality<int> queue2 = [1, 2];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IFrozenQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IFrozenQueueWithValueEquality<int> queue2 = [3, 4, 5];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        IFrozenQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IFrozenQueueWithValueEquality<int> queue2 = [2, 1, 3];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IFrozenQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IFrozenQueueWithValueEquality<int> queue2 = [1, 2, 3];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task RecordEquals_WhenQueuesHaveDifferentNumberElements_ThenReturnsFalse()
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
    public async Task RecordEquals_WhenQueuesHaveDifferentElements_ThenReturnsFalse()
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
    public async Task RecordEquals_WhenQueuesHaveSameElementsInDifferentOrder_ThenReturnsFalse()
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
    public async Task RecordEquals_WhenQueuesHaveSameElementsInSameOrder_ThenReturnsTrue()
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
    public async Task IsEmpty_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        Mock<IImmutableQueue<int>> mock = Mock.Of<IImmutableQueue<int>>();
        mock.IsEmpty.Returns(true);
        IFrozenQueueWithValueEquality<int> queue = CreateFrozenQueueWithValueEquality(mock.Object);

        // Act
        bool isEmpty = queue.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
        mock.IsEmpty.WasCalled(Times.Once);
    }

    [Test]
    public async Task Peek_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        Mock<IImmutableQueue<int>> mock = Mock.Of<IImmutableQueue<int>>();
        mock.Peek().Returns(1);
        IFrozenQueueWithValueEquality<int> queue = CreateFrozenQueueWithValueEquality(mock.Object);

        // Act
        int item = queue.Peek();

        // Assert
        await Assert.That(item).IsEqualTo(1);
        mock.Peek().WasCalled(Times.Once);
    }

    [Test]
    public async Task TryPeek_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        Mock<IImmutableQueue<int>> mock = Mock.Of<IImmutableQueue<int>>();
        mock.IsEmpty.Returns(false);
        mock.Peek().Returns(1);
        IFrozenQueueWithValueEquality<int> queue = CreateFrozenQueueWithValueEquality(mock.Object);

        // Act
        bool found = queue.TryPeek(out int item);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(item).IsEqualTo(1);
        }

        mock.IsEmpty.WasCalled(Times.Once);
        mock.Peek().WasCalled(Times.Once);
    }

    [Test]
    public async Task Contains_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        List<int> items = [1, 2, 3];
        Mock<IImmutableQueue<int>> mock = Mock.Of<IImmutableQueue<int>>();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IFrozenQueueWithValueEquality<int> queue = CreateFrozenQueueWithValueEquality(mock.Object);

        // Act
        bool contains = queue.Contains(2);

        // Assert
        await Assert.That(contains).IsTrue();
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    [Test]
    public async Task ToArray_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        List<int> items = [1, 2, 3];
        Mock<IImmutableQueue<int>> mock = Mock.Of<IImmutableQueue<int>>();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IFrozenQueueWithValueEquality<int> queue = CreateFrozenQueueWithValueEquality(mock.Object);

        // Act
        int[] result = queue.ToArray();

        // Assert
        await Assert.That(result).IsEquivalentTo(items);
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    [Test]
    public async Task Enumerator_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        List<int> items = [1, 2, 3];
        Mock<IImmutableQueue<int>> mock = Mock.Of<IImmutableQueue<int>>();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IFrozenQueueWithValueEquality<int> queue = CreateFrozenQueueWithValueEquality(mock.Object);

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in queue)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEquivalentTo(items);
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    /// <summary>
    ///     The public factory method for a frozen queue always copies its source into a real
    ///     <see cref="ImmutableQueue{T}" />, so there is no public way to inject a mock as the underlying
    ///     collection.  This constructs the internal wrapper directly via reflection so a mock
    ///     <see cref="IImmutableQueue{T}" /> can be substituted for pass-through testing.
    /// </summary>
    private static IFrozenQueueWithValueEquality<T> CreateFrozenQueueWithValueEquality<T>(
        IImmutableQueue<T> immutableQueue,
        IEqualityComparer<T>? equalityComparer = null)
    {
        Type openType = typeof(ValueEqualityCollectionFactory).GetNestedType(
            name: "FrozenQueueWithValueEquality`1",
            bindingAttr: BindingFlags.NonPublic)!;
        Type closedType = openType.MakeGenericType(typeof(T));
        ConstructorInfo constructor = closedType
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Single();
        return (IFrozenQueueWithValueEquality<T>)constructor.Invoke([immutableQueue, equalityComparer]);
    }

    private record Record(IFrozenQueueWithValueEquality<int> Queue);
}
