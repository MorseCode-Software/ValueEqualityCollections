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
    public async Task IsEmpty_WhenCalled_ThenCallIsPassedThroughToUnderlyingStack()
    {
        // Arrange
        var mock = IImmutableStack<int>.Mock();
        mock.IsEmpty.Returns(true);
        IFrozenStackWithValueEquality<int> stack = CreateFrozenStackWithValueEquality(mock);

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
        IFrozenStackWithValueEquality<int> stack = CreateFrozenStackWithValueEquality(mock);

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
        IFrozenStackWithValueEquality<int> stack = CreateFrozenStackWithValueEquality(mock);

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
        IFrozenStackWithValueEquality<int> stack = CreateFrozenStackWithValueEquality(mock);

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
        IFrozenStackWithValueEquality<int> stack = CreateFrozenStackWithValueEquality(mock);

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
        IFrozenStackWithValueEquality<int> stack = CreateFrozenStackWithValueEquality(mock);

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

    /// <summary>
    ///     The public factory method for a frozen stack always copies its source into a real
    ///     <see cref="ImmutableStack{T}" />, so there is no public way to inject a mock as the underlying
    ///     collection.  This constructs the internal wrapper directly via reflection so a mock
    ///     <see cref="IImmutableStack{T}" /> can be substituted for pass-through testing.
    /// </summary>
    private static IFrozenStackWithValueEquality<T> CreateFrozenStackWithValueEquality<T>(
        IImmutableStack<T> immutableStack,
        IEqualityComparer<T>? equalityComparer = null)
    {
        Type openType = typeof(ValueEqualityCollectionFactory).GetNestedType(
            name: "FrozenStackWithValueEquality`1",
            bindingAttr: BindingFlags.NonPublic)!;
        Type closedType = openType.MakeGenericType(typeof(T));
        ConstructorInfo constructor = closedType
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Single();
        return (IFrozenStackWithValueEquality<T>)constructor.Invoke([immutableStack, equalityComparer]);
    }

    private record Record(IFrozenStackWithValueEquality<int> Stack);
}
