[![](https://img.shields.io/nuget/v/soenneker.extensions.collection.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.collection/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.collection/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.collection/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.collection.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.collection/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.collection/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.collection/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Collection

Mutating helpers for removing matching items from `ICollection<T>` and conditionally adding non-null values.

## Installation

```bash
dotnet add package Soenneker.Extensions.Collection
```

## Remove multiple values

```csharp
using Soenneker.Extensions.Collection;

var names = new List<string> { "Alpha", "Beta", "ALPHA", "Gamma" };

names.RemoveEnumerableFromCollection(
    ["alpha", "gamma"],
    StringComparer.OrdinalIgnoreCase);

// names contains only "Beta"
```

`RemoveEnumerableFromCollection()` mutates the target and removes every occurrence that matches any supplied removal value. Missing values are ignored. The removal sequence is materialized before the target changes, so it is safe to pass the same collection as both arguments.

Equality selection is explicit:

- A supplied `IEqualityComparer<T>` is honored for `List<T>`, `HashSet<T>`, and other `ICollection<T>` implementations.
- Without one, a `HashSet<T>` target uses its own comparer.
- Other target types use `EqualityComparer<T>.Default`.

The method creates a `HashSet<T>` of removal values. Lists and hash sets use optimized in-place removal. Other collection types are scanned into a temporary list before removal, avoiding mutation during enumeration and ensuring duplicate matching elements are all removed.

Null or empty target/removal collections are no-ops. A read-only collection still throws from its own `Remove` implementation when a match is found. The method provides no locking; callers must synchronize concurrent mutation.

For a short inline list, `RemoveFromCollection()` is a `params` convenience wrapper:

```csharp
names.RemoveFromCollection("Beta", "Gamma");
```

## Add non-null values

```csharp
var recipients = new List<string>();
string? optionalAddress = GetAddress();

recipients.AddIfNotNull(optionalAddress);
```

`AddIfNotNull()` skips a null reference or nullable value and otherwise calls the target collection's `Add`. It does not prevent duplicates, validate the value, or suppress exceptions from a read-only/fixed-size collection. Non-nullable value types are always added, including their default value.
