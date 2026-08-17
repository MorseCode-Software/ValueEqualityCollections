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

public class ImmutableStackWithValueEqualityTests
{
    [Test]
    public async Task CollectionExpression_WhenStackIsEmpty_ThenResultIsEmpty()
    {
        // Arrange

        // Act
        // ReSharper disable once CollectionNeverUpdated.Local
        IImmutableStackWithValueEquality<int> stack = [];

        // Assert
        await Assert.That(stack.IsEmpty).IsTrue();
    }

    [Test]
    public async Task CollectionExpression_WhenStackHasThreeItems_ThenResultHasSameThreeItemsInReverseOrder()
    {
        // Arrange

        // Act
        IImmutableStackWithValueEquality<int> stack = [1, 2, 3];

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
    public async Task ToImmutableStackWithValueEquality_WhenStackIsEmpty_ThenResultIsEmpty()
    {
        // Arrange
        IImmutableStack<int> underlyingStack = ImmutableStack.Create<int>();

        // Act
        IImmutableStackWithValueEquality<int> stack = underlyingStack.ToImmutableStackWithValueEquality();

        // Assert
        await Assert.That(stack.IsEmpty).IsTrue();
    }

    [Test]
    public async Task
        ToImmutableStackWithValueEquality_WhenStackHasThreeItems_ThenResultHasSameThreeItemsInReverseOrder()
    {
        // Arrange
        IImmutableStack<int> underlyingStack = ImmutableStack.Create(1, 2, 3);

        // Act
        IImmutableStackWithValueEquality<int> stack = underlyingStack.ToImmutableStackWithValueEquality();

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
        IImmutableStackWithValueEquality<int> stack1 = [1, 2, 3];
        IImmutableStackWithValueEquality<int> stack2 = [1, 2];

        // Act
        bool areEqual = stack1.Equals(stack2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenStacksHaveDifferentElements_ThenReturnsFalse()
    {
        // Arrange
        IImmutableStackWithValueEquality<int> stack1 = [1, 2, 3];
        IImmutableStackWithValueEquality<int> stack2 = [3, 4, 5];

        // Act
        bool areEqual = stack1.Equals(stack2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenStacksHaveSameElementsInDifferentOrder_ThenReturnsFalse()
    {
        // Arrange
        IImmutableStackWithValueEquality<int> stack1 = [1, 2, 3];
        IImmutableStackWithValueEquality<int> stack2 = [2, 1, 3];

        // Act
        bool areEqual = stack1.Equals(stack2);

        // Assert
        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task Equals_WhenStacksHaveSameElementsInSameOrder_ThenReturnsTrue()
    {
        // Arrange
        IImmutableStackWithValueEquality<int> stack1 = [1, 2, 3];
        IImmutableStackWithValueEquality<int> stack2 = [1, 2, 3];

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
    public async Task IsEmpty_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        var mock = IImmutableStack<int>.Mock();
        mock.IsEmpty.Returns(true);
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        bool isEmpty = stack.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
        mock.IsEmpty.WasCalled(Times.Once);
    }

    [Test]
    public async Task Peek_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        var mock = IImmutableStack<int>.Mock();
        mock.Peek().Returns(3);
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        int item = stack.Peek();

        // Assert
        await Assert.That(item).IsEqualTo(3);
        mock.Peek().WasCalled(Times.Once);
    }

    [Test]
    public async Task TryPeek_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        var mock = IImmutableStack<int>.Mock();
        mock.IsEmpty.Returns(false);
        mock.Peek().Returns(3);
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        bool found = stack.TryPeek(out int result);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(result).IsEqualTo(3);
        }

        mock.IsEmpty.WasCalled(Times.Once);
        mock.Peek().WasCalled(Times.Once);
    }

    [Test]
    public async Task Contains_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        List<int> items = [3, 2, 1];
        var mock = IImmutableStack<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        bool contains = stack.Contains(2);

        // Assert
        await Assert.That(contains).IsTrue();
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    [Test]
    public async Task ToArray_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        List<int> items = [3, 2, 1];
        var mock = IImmutableStack<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        int[] array = stack.ToArray();

        // Assert
        await Assert.That(array).IsEquivalentTo(items);
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    [Test]
    public async Task Enumerator_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        List<int> items = [3, 2, 1];
        var mock = IImmutableStack<int>.Mock();
        mock.GetEnumerator().Returns(items.GetEnumerator());
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        List<int> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (int item in stack)
        {
            result.Add(item);
        }

        // Assert
        await Assert.That(result).IsEquivalentTo(items);
        mock.GetEnumerator().WasCalled(Times.Once);
    }

    [Test]
    public async Task Clear_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        IImmutableStack<int> returnedStack = ImmutableStack.Create<int>();
        var mock = IImmutableStack<int>.Mock();
        mock.Clear().Returns(returnedStack);
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        IImmutableStackWithValueEquality<int> result = stack.Clear();

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
        mock.Clear().WasCalled(Times.Once);
    }

    [Test]
    public async Task Push_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        IImmutableStack<int> returnedStack = ImmutableStack.Create(4, 3, 2, 1);
        var mock = IImmutableStack<int>.Mock();
        mock.Push(Any<int>()).Returns(returnedStack);
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        IImmutableStackWithValueEquality<int> result = stack.Push(4);

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo(returnedStack.ToArray());
        mock.Push(4).WasCalled(Times.Once);
    }

    [Test]
    public async Task Pop_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        IImmutableStack<int> returnedStack = ImmutableStack.Create(2, 1);
        var mock = IImmutableStack<int>.Mock();
        mock.Pop().Returns(returnedStack);
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        IImmutableStackWithValueEquality<int> result = stack.Pop();

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo(returnedStack.ToArray());
        mock.Pop().WasCalled(Times.Once);
    }

    [Test]
    public async Task Pop_WithOutValue_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        IImmutableStack<int> returnedStack = ImmutableStack.Create(2, 1);
        var mock = IImmutableStack<int>.Mock();
        mock.Peek().Returns(3);
        mock.Pop().Returns(returnedStack);
        IImmutableStackWithValueEquality<int> stack = mock.ToImmutableStackWithValueEquality();

        // Act
        IImmutableStackWithValueEquality<int> result = stack.Pop(out int value);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(value).IsEqualTo(3);
            await Assert.That(result.ToArray()).IsEquivalentTo(returnedStack.ToArray());
        }

        mock.Peek().WasCalled(Times.Once);
        mock.Pop().WasCalled(Times.Once);
    }

    [Test]
    public async Task CastToIImmutableStack_Push_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        IImmutableStack<int> returnedStack = ImmutableStack.Create(4, 3, 2, 1);
        var mock = IImmutableStack<int>.Mock();
        mock.Push(Any<int>()).Returns(returnedStack);
        IImmutableStackWithValueEquality<int> stackWithValueEquality = mock.ToImmutableStackWithValueEquality();
        IImmutableStack<int> stack = stackWithValueEquality;

        // Act
        IImmutableStack<int> result = stack.Push(4);

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo(returnedStack.ToArray());
        mock.Push(4).WasCalled(Times.Once);
    }

    [Test]
    public async Task CastToIImmutableStack_Pop_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        IImmutableStack<int> returnedStack = ImmutableStack.Create(2, 1);
        var mock = IImmutableStack<int>.Mock();
        mock.Pop().Returns(returnedStack);
        IImmutableStackWithValueEquality<int> stackWithValueEquality = mock.ToImmutableStackWithValueEquality();
        IImmutableStack<int> stack = stackWithValueEquality;

        // Act
        IImmutableStack<int> result = stack.Pop();

        // Assert
        await Assert.That(result.ToArray()).IsEquivalentTo(returnedStack.ToArray());
        mock.Pop().WasCalled(Times.Once);
    }

    [Test]
    public async Task CastToIImmutableStack_Clear_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        IImmutableStack<int> returnedStack = ImmutableStack.Create<int>();
        var mock = IImmutableStack<int>.Mock();
        mock.Clear().Returns(returnedStack);
        IImmutableStackWithValueEquality<int> stackWithValueEquality = mock.ToImmutableStackWithValueEquality();
        IImmutableStack<int> stack = stackWithValueEquality;

        // Act
        IImmutableStack<int> result = stack.Clear();

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
        mock.Clear().WasCalled(Times.Once);
    }

    private record Record(IImmutableStackWithValueEquality<int> Stack);
}
