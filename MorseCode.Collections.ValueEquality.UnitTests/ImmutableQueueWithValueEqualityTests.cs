using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using TUnit.Mocks;
using TUnit.Mocks.Arguments;
using TUnit.Mocks.Generated;
using static TUnit.Mocks.Arguments.Arg;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class ImmutableQueueWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenQueueIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableQueueWithValueEquality<int> queue = [];

        // Assert
        await Assert.That(queue.IsEmpty).IsTrue();
    }

    [Test]
    public async Task CollectionExpression_WhenQueueHasThreeItems_ThenResultHasSameThreeItemsInOrder()
    {
        // Arrange

        // Act
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Assert
        await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task ToImmutableQueueWithValueEquality_WhenQueueIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        IImmutableQueue<int> queueWithoutValueEquality = ImmutableQueue<int>.Empty;

        // Act
        IImmutableQueueWithValueEquality<int> queue = queueWithoutValueEquality.ToImmutableQueueWithValueEquality();

        // Assert
        await Assert.That(queue.IsEmpty).IsTrue();
    }

    [Test]
    public async Task ToImmutableQueueWithValueEquality_WhenQueueHasThreeItems_ThenResultHasSameThreeItemsInOrder()
    {
        // Arrange
        IImmutableQueue<int> queueWithoutValueEquality = ImmutableQueue.Create(1, 2, 3);

        // Act
        IImmutableQueueWithValueEquality<int> queue = queueWithoutValueEquality.ToImmutableQueueWithValueEquality();

        // Assert
        await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Equals_WhenQueuesHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IImmutableQueueWithValueEquality<int> queue2 = [1, 2];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IImmutableQueueWithValueEquality<int> queue2 = [3, 4, 5];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IImmutableQueueWithValueEquality<int> queue2 = [2, 1, 3];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IImmutableQueueWithValueEquality<int> queue2 = [1, 2, 3];

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
        var mock = IImmutableQueue<int>.Mock();
        mock.IsEmpty.Returns(true);
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

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
        var mock = IImmutableQueue<int>.Mock();
        mock.Peek().Returns(1);
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

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
        var mock = IImmutableQueue<int>.Mock();
        mock.IsEmpty.Returns(false);
        mock.Peek().Returns(1);
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

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
        var mock = IImmutableQueue<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

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
        var mock = IImmutableQueue<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

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
        var mock = IImmutableQueue<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

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

    [Test]
    public async Task Clear_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        IImmutableQueue<int> returnedQueue = ImmutableQueue<int>.Empty;
        var mock = IImmutableQueue<int>.Mock();
        mock.Clear().Returns(returnedQueue);
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

        // Act
        IImmutableQueueWithValueEquality<int> result = queue.Clear();

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
        mock.Clear().WasCalled(Times.Once);
    }

    [Test]
    public async Task Enqueue_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        IImmutableQueue<int> returnedQueue = ImmutableQueue.Create(1, 2, 3, 4);
        var mock = IImmutableQueue<int>.Mock();
        mock.Enqueue(Any<int>()).Returns(returnedQueue);
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

        // Act
        IImmutableQueueWithValueEquality<int> result = queue.Enqueue(4);

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo(returnedQueue.ToArray());
        mock.Enqueue(4).WasCalled(Times.Once);
    }

    [Test]
    public async Task Dequeue_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        IImmutableQueue<int> returnedQueue = ImmutableQueue.Create(2, 3);
        var mock = IImmutableQueue<int>.Mock();
        mock.Dequeue().Returns(returnedQueue);
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

        // Act
        IImmutableQueueWithValueEquality<int> result = queue.Dequeue();

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo(returnedQueue.ToArray());
        mock.Dequeue().WasCalled(Times.Once);
    }

    [Test]
    public async Task DequeueOutValue_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        IImmutableQueue<int> returnedQueue = ImmutableQueue.Create(2, 3);
        var mock = IImmutableQueue<int>.Mock();
        mock.Peek().Returns(1);
        mock.Dequeue().Returns(returnedQueue);
        IImmutableQueueWithValueEquality<int> queue = mock.ToImmutableQueueWithValueEquality();

        // Act
        IImmutableQueueWithValueEquality<int> result = queue.Dequeue(out int value);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(value).IsEqualTo(1);
            await Assert.That(result.ToArray()).IsEquivalentTo(returnedQueue.ToArray());
        }

        mock.Peek().WasCalled(Times.Once);
        mock.Dequeue().WasCalled(Times.Once);
    }

    [Test]
    public async Task IImmutableQueueIsEmpty_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        var mock = IImmutableQueue<int>.Mock();
        mock.IsEmpty.Returns(false);
        IImmutableQueueWithValueEquality<int> source = mock.ToImmutableQueueWithValueEquality();
        IImmutableQueue<int> queue = source;

        // Act
        bool isEmpty = queue.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsFalse();
        mock.IsEmpty.WasCalled(Times.Once);
    }

    [Test]
    public async Task IImmutableQueueEnqueue_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        IImmutableQueue<int> returnedQueue = ImmutableQueue.Create(1, 2, 3, 4);
        var mock = IImmutableQueue<int>.Mock();
        mock.Enqueue(Any<int>()).Returns(returnedQueue);
        IImmutableQueueWithValueEquality<int> source = mock.ToImmutableQueueWithValueEquality();
        IImmutableQueue<int> queue = source;

        // Act
        IImmutableQueue<int> result = queue.Enqueue(4);

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo(returnedQueue.ToArray());
        mock.Enqueue(4).WasCalled(Times.Once);
    }

    [Test]
    public async Task IImmutableQueueDequeue_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        IImmutableQueue<int> returnedQueue = ImmutableQueue.Create(2, 3);
        var mock = IImmutableQueue<int>.Mock();
        mock.Dequeue().Returns(returnedQueue);
        IImmutableQueueWithValueEquality<int> source = mock.ToImmutableQueueWithValueEquality();
        IImmutableQueue<int> queue = source;

        // Act
        IImmutableQueue<int> result = queue.Dequeue();

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo(returnedQueue.ToArray());
        mock.Dequeue().WasCalled(Times.Once);
    }

    [Test]
    public async Task IImmutableQueueClear_WhenCalled_ThenCallIsPassedThroughToUnderlyingQueue()
    {
        // Arrange
        IImmutableQueue<int> returnedQueue = ImmutableQueue<int>.Empty;
        var mock = IImmutableQueue<int>.Mock();
        mock.Clear().Returns(returnedQueue);
        IImmutableQueueWithValueEquality<int> source = mock.ToImmutableQueueWithValueEquality();
        IImmutableQueue<int> queue = source;

        // Act
        IImmutableQueue<int> result = queue.Clear();

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
        mock.Clear().WasCalled(Times.Once);
    }

    private record Record(IImmutableQueueWithValueEquality<int> Queue);
}
