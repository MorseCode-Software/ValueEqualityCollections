using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class ReadOnlyQueueWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenQueueIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IReadOnlyQueueWithValueEquality<int> queue = [];

        // Assert
        await Assert.That(queue.IsEmpty).IsTrue();
    }

    [Test]
    public async Task CollectionExpression_WhenQueueHasThreeItems_ThenResultHasSameThreeItemsInOrder()
    {
        // Arrange

        // Act
        IReadOnlyQueueWithValueEquality<int> queue = [1, 2, 3];

        // Assert
        await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task ToReadOnlyQueueWithValueEquality_WhenQueueIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        Queue<int> queueWithoutValueEquality = new([]);

        // Act
        IReadOnlyQueueWithValueEquality<int> queue = queueWithoutValueEquality.ToReadOnlyQueueWithValueEquality();

        // Assert
        await Assert.That(queue.IsEmpty).IsTrue();
    }

    [Test]
    public async Task ToReadOnlyQueueWithValueEquality_WhenQueueHasThreeItems_ThenResultHasSameThreeItemsInOrder()
    {
        // Arrange
        Queue<int> queueWithoutValueEquality = new([1, 2, 3]);

        // Act
        IReadOnlyQueueWithValueEquality<int> queue = queueWithoutValueEquality.ToReadOnlyQueueWithValueEquality();

        // Assert
        await Assert.That(queue.ToArray()).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Equals_WhenQueuesHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IReadOnlyQueueWithValueEquality<int> queue2 = [1, 2];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IReadOnlyQueueWithValueEquality<int> queue2 = [3, 4, 5];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IReadOnlyQueueWithValueEquality<int> queue2 = [2, 1, 3];

        // Act
        bool areEqual = queue1.Equals(queue2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenQueuesHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue1 = [1, 2, 3];
        IReadOnlyQueueWithValueEquality<int> queue2 = [1, 2, 3];

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
        IReadOnlyQueueWithValueEquality<int> queue = [];

        // Act
        bool isEmpty = queue.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmpty_WhenQueueHasThreeItems_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        bool isEmpty = queue.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsFalse();
    }

    [Test]
    public async Task Peek_WhenQueueHasThreeItems_ThenReturnsFirstEnqueuedItem()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue = [1, 2, 3];

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
        IReadOnlyQueueWithValueEquality<int> queue = [];

        // Act & Assert
        await Assert.That(() => queue.Peek()).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task TryPeek_WhenQueueHasThreeItems_ThenReturnsTrueAndFirstEnqueuedItem()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue = [1, 2, 3];

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
        IReadOnlyQueueWithValueEquality<int> queue = [];

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
        IReadOnlyQueueWithValueEquality<int> queue = [1, 2, 3];

        // Act
        bool contains = queue.Contains(2);

        // Assert
        await Assert.That(contains).IsTrue();
    }

    [Test]
    public async Task Contains_WhenItemIsNotInQueue_ThenReturnsFalse()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue = [1, 2, 3];

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
        IReadOnlyQueueWithValueEquality<int> queue = [];

        // Act
        int[] result = queue.ToArray();

        // Assert
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task ToArray_WhenQueueHasThreeItems_ThenResultHasSameThreeItemsInOrder()
    {
        // Arrange
        IReadOnlyQueueWithValueEquality<int> queue = [1, 2, 3];

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
        IReadOnlyQueueWithValueEquality<int> queue = [];

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
        IReadOnlyQueueWithValueEquality<int> queue = [1, 2, 3];

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

    private record Record(IReadOnlyQueueWithValueEquality<int> Queue);
}