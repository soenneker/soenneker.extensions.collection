[![](https://img.shields.io/nuget/v/soenneker.extensions.collection.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.collection/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.collection/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.collection/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.collection.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.collection/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.collection/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.collection/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Collection

Helpful Collection extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.Collection
```

## Quick start

```csharp
using Soenneker.Extensions.Collection;

// Given an existing ICollection<T>? named collection:
collection.RemoveEnumerableFromCollection(toRemove);
```

## Common operations

- `RemoveEnumerableFromCollection()` - Removes all elements in the specified sequence from the target collection.
- `RemoveFromCollection()` - Removes from collection.
- `AddIfNotNull()` - Adds an item to the collection if the item is not null.
