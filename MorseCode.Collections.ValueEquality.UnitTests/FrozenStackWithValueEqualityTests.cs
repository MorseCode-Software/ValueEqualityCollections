using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class FrozenStackWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenStackIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenStackWithValueEquality<int> stack = [];

        // Assert
        await Assert.That(stack.IsEmpty).IsTrue();
    }

    [Test]
    public async Task CollectionExpression_WhenStackHasThreeItems_ThenResultHasSameThreeItemsInReverseOrder()
    {
        // Arrange

        // Act
        IFrozenStackWithValueEquality<int> stack = [1, 2, 3];

        // Assert
        int[] array = stack.ToArray();

        using (Assert.Multiple())
        {
            await Assert.That(array.Length).IsEqualTo(3);
            await Assert.That(array[0]).IsEqualTo(3);
            await Assert.That(array[1]).IsEqualTo(2);
            await Assert.That(array[2]).IsEqualTo(1);
        }
    }

    [Test]
    public async Task ToFrozenStackWithValueEquality_WhenStackIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        IEnumerable<int> underlyingSequence = [];

        // Act
        IFrozenStackWithValueEquality<int> stack = underlyingSequence.ToFrozenStackWithValueEquality();

        // Assert
        await Assert.That(stack.IsEmpty).IsTrue();
    }

    [Test]
    public async Task ToFrozenStackWithValueEquality_WhenStackHasThreeItems_ThenResultHasSameThreeItemsInReverseOrder()
    {
        // Arrange
        IEnumerable<int> underlyingSequence = [1, 2, 3];

        // Act
        IFrozenStackWithValueEquality<int> stack = underlyingSequence.ToFrozenStackWithValueEquality();

        // Assert
        int[] array = stack.ToArray();

        using (Assert.Multiple())
        {
            await Assert.That(array.Length).IsEqualTo(3);
            await Assert.That(array[0]).IsEqualTo(3);
            await Assert.That(array[1]).IsEqualTo(2);
            await Assert.That(array[2]).IsEqualTo(1);
        }
    }

    [Test]
    public async Task Equals_WhenStacksHaveDifferentNumberElements_ThenReturnsFalse()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack1 = [1, 2, 3];
        IFrozenStackWithValueEquality<int> stack2 = [1, 2];

        // Act
        bool areEqual = stack1.Equals(stack2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenStacksHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack1 = [1, 2, 3];
        IFrozenStackWithValueEquality<int> stack2 = [3, 4, 5];

        // Act
        bool areEqual = stack1.Equals(stack2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenStacksHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack1 = [1, 2, 3];
        IFrozenStackWithValueEquality<int> stack2 = [2, 1, 3];

        // Act
        bool areEqual = stack1.Equals(stack2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenStacksHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack1 = [1, 2, 3];
        IFrozenStackWithValueEquality<int> stack2 = [1, 2, 3];

        // Act
        bool areEqual = stack1.Equals(stack2);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task RecordEquals_WhenStacksHaveDifferentNumberElements_ThenReturnsFalse()
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
    public async Task RecordEquals_WhenStacksHaveDifferentElements_ThenReturnsFalse()
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
    public async Task RecordEquals_WhenStacksHaveSameElementsInDifferentOrder_ThenReturnsFalse()
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
    public async Task RecordEquals_WhenStacksHaveSameElementsInSameOrder_ThenReturnsTrue()
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
    public async Task IsEmpty_WhenStackIsEmpty_ThenReturnsTrue()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenStackWithValueEquality<int> stack = [];

        // Act
        bool isEmpty = stack.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmpty_WhenStackHasItems_ThenReturnsFalse()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack = [1, 2, 3];

        // Act
        bool isEmpty = stack.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsFalse();
    }

    [Test]
    public async Task Peek_WhenStackHasItems_ThenReturnsTopItem()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack = [1, 2, 3];

        // Act
        int item = stack.Peek();

        // Assert
        await Assert.That(item).IsEqualTo(3);
    }

    [Test]
    public async Task Peek_WhenStackIsEmpty_ThenThrowsInvalidOperationException()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenStackWithValueEquality<int> stack = [];

        // Act & Assert
        await Assert.That(() => stack.Peek()).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task TryPeek_WhenStackHasItems_ThenReturnsTrueAndTopItem()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack = [1, 2, 3];

        // Act
        bool found = stack.TryPeek(out int result);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(result).IsEqualTo(3);
        }
    }

    [Test]
    public async Task TryPeek_WhenStackIsEmpty_ThenReturnsFalse()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenStackWithValueEquality<int> stack = [];

        // Act
        bool found = stack.TryPeek(out int result);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsFalse();
            await Assert.That(result).IsEqualTo(0);
        }
    }

    [Test]
    public async Task Contains_WhenItemIsInStack_ThenReturnsTrue()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack = [1, 2, 3];

        // Act
        bool contains = stack.Contains(2);

        // Assert
        await Assert.That(contains).IsTrue();
    }

    [Test]
    public async Task Contains_WhenItemIsNotInStack_ThenReturnsFalse()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack = [1, 2, 3];

        // Act
        bool contains = stack.Contains(5);

        // Assert
        await Assert.That(contains).IsFalse();
    }

    [Test]
    public async Task ToArray_WhenStackIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenStackWithValueEquality<int> stack = [];

        // Act
        int[] array = stack.ToArray();

        // Assert
        await Assert.That(array).IsEmpty();
    }

    [Test]
    public async Task ToArray_WhenStackHasThreeItems_ThenResultIsInTopToBottomOrder()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack = [1, 2, 3];

        // Act
        int[] array = stack.ToArray();

        // Assert
        await Assert.That(array.Length).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(array[0]).IsEqualTo(3);
            await Assert.That(array[1]).IsEqualTo(2);
            await Assert.That(array[2]).IsEqualTo(1);
        }
    }

    [Test]
    public async Task Enumerator_WhenStackIsEmpty_ThenNoElementsAreEnumerated()
    {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        IFrozenStackWithValueEquality<int> stack = [];

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in stack)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task Enumerator_WhenStackHasThreeItems_ThenElementsAreEnumeratedInTopToBottomOrder()
    {
        // Arrange
        IFrozenStackWithValueEquality<int> stack = [1, 2, 3];

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in stack)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result.Count).IsEqualTo(3);

        using (Assert.Multiple())
        {
            await Assert.That(result[0]).IsEqualTo(3);
            await Assert.That(result[1]).IsEqualTo(2);
            await Assert.That(result[2]).IsEqualTo(1);
        }
    }

    private record Record(IFrozenStackWithValueEquality<int> Stack);
}