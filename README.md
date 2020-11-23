# FromCenter

Iterate over an `IEnumerable<T>` from the center alternating between the right and left sides.

## Examples

```csharp
new int[] { 1, 2, 3, 4, 5, 6, 7 }

// Will be iterated in this order:
4, 3, 5, 2, 6, 1, 7

// With startWithRightSide set to true:
4, 5, 3, 6, 2, 7, 1
```

## Usage

```csharp
using FromCenter;

var array = new int[] { 1, 2, 3 };

// The extension method FromCenter can be called with any IEnumerable<T>
foreach (var item in array.FromCenter()) {
    Console.WriteLine(item);
}
```
