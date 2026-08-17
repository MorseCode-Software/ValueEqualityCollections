using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

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
    public async Task IsEmpty_WhenQueueIsEmpty_ThenReturnsTrue()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableQueueWithValueEquality<int> queue = [];

        // Act
        bool isEmpty = queue.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmpty_WhenQueueHasThreeItems_ThenReturnsFalse()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        bool isEmpty = queue.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsFalse();
    }

    [Test]
    public async Task Peek_WhenQueueHasThreeItems_ThenReturnsFirstEnqueuedItem()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        int item = queue.Peek();

        // Assert
        await Assert.That(item).IsEqualTo(1);
    }

    [Test]
    public async Task Peek_WhenQueueIsEmpty_ThenThrowsInvalidOperationException()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableQueueWithValueEquality<int> queue = [];

        // Act & Assert
        await Assert.That(() => queue.Peek()).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task TryPeek_WhenQueueHasThreeItems_ThenReturnsTrueAndFirstEnqueuedItem()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        bool found = queue.TryPeek(out int item);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(item).IsEqualTo(1);
        }
    }

    [Test]
    public async Task TryPeek_WhenQueueIsEmpty_ThenReturnsFalse()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableQueueWithValueEquality<int> queue = [];

        // Act
        bool found = queue.TryPeek(out int item);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsFalse();
            await Assert.That(item).IsEqualTo(0);
        }
    }

    [Test]
    public async Task Contains_WhenItemIsInQueue_ThenReturnsTrue()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        bool contains = queue.Contains(2);

        // Assert
        await Assert.That(contains).IsTrue();
    }

    [Test]
    public async Task Contains_WhenItemIsNotInQueue_ThenReturnsFalse()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        bool contains = queue.Contains(4);

        // Assert
        await Assert.That(contains).IsFalse();
    }

    [Test]
    public async Task ToArray_WhenQueueIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableQueueWithValueEquality<int> queue = [];

        // Act
        int[] result = queue.ToArray();

        // Assert
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task ToArray_WhenQueueHasThreeItems_ThenResultHasSameThreeItemsInOrder()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        int[] result = queue.ToArray();

        // Assert
        await Assert.That(result).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Enumerator_WhenQueueIsEmpty_ThenNoElementsAreEnumerated()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableQueueWithValueEquality<int> queue = [];

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in queue)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task Enumerator_WhenQueueHasThreeItems_ThenSameThreeElementsAreEnumeratedInOrder()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in queue)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Clear_WhenQueueHasThreeItems_ThenResultIsEmptyAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        IImmutableQueueWithValueEquality<int> result = queue.Clear();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.IsEmpty).IsTrue();
            await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
        }
    }

    [Test]
    public async Task Enqueue_WhenQueueHasThreeItems_ThenResultHasFourItemsAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        IImmutableQueueWithValueEquality<int> result = queue.Enqueue(4);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.ToArray()).IsEquivalentTo([1, 2, 3, 4]);
            await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
        }
    }

    [Test]
    public async Task Dequeue_WhenQueueHasThreeItems_ThenResultHasRemainingItemsAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        IImmutableQueueWithValueEquality<int> result = queue.Dequeue();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(result.ToArray()).IsEquivalentTo([2, 3]);
            await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
        }
    }

    [Test]
    public async Task Dequeue_WhenQueueIsEmpty_ThenThrowsInvalidOperationException()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableQueueWithValueEquality<int> queue = [];

        // Act & Assert
        await Assert.That(() => queue.Dequeue()).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task
        DequeueOutValue_WhenQueueHasThreeItems_ThenReturnsFirstEnqueuedItemAndRemainingQueueAndOriginalIsUnchanged()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        IImmutableQueueWithValueEquality<int> result = queue.Dequeue(out int value);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(value).IsEqualTo(1);
            await Assert.That(result.ToArray()).IsEquivalentTo([2, 3]);
            await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
        }
    }

    [Test]
    public async Task DequeueOutValue_WhenQueueIsEmpty_ThenThrowsInvalidOperationException()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableQueueWithValueEquality<int> queue = [];

        // Act & Assert
        await Assert.That(() => queue.Dequeue(out int _)).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task IImmutableQueueIsEmpty_WhenQueueHasThreeItems_ThenReturnsFalse()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> source = [1, 2, 3];
        IImmutableQueue<int> queue = source;

        // Act
        bool isEmpty = queue.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsFalse();
    }

    [Test]
    public async Task IImmutableQueueEnqueue_WhenQueueHasThreeItems_ThenResultHasFourItems()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> source = [1, 2, 3];
        IImmutableQueue<int> queue = source;

        // Act
        IImmutableQueue<int> result = queue.Enqueue(4);

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo([1, 2, 3, 4]);
    }

    [Test]
    public async Task IImmutableQueueDequeue_WhenQueueHasThreeItems_ThenResultHasRemainingItems()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> source = [1, 2, 3];
        IImmutableQueue<int> queue = source;

        // Act
        IImmutableQueue<int> result = queue.Dequeue();

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo([2, 3]);
    }

    [Test]
    public async Task IImmutableQueueClear_WhenQueueHasThreeItems_ThenResultIsEmpty()
    {
        // Arrange
        IImmutableQueueWithValueEquality<int> source = [1, 2, 3];
        IImmutableQueue<int> queue = source;

        // Act
        IImmutableQueue<int> result = queue.Clear();

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    private record Record(IImmutableQueueWithValueEquality<int> Queue);
}