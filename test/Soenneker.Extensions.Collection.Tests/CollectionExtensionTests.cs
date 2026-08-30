using Soenneker.Tests.Unit;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Soenneker.Extensions.Collection.Tests;

public class CollectionExtensionTests : UnitTest
{
    [Test]
    public async System.Threading.Tasks.Task RemoveEnumerable_HonorsComparerAcrossCollectionTypes()
    {
        var set = new HashSet<string>(System.StringComparer.Ordinal) { "Alpha", "Beta" };
        set.RemoveEnumerableFromCollection(["alpha"], System.StringComparer.OrdinalIgnoreCase);

        var collection = new Collection<string> { "Alpha", "alpha", "Beta" };
        collection.RemoveEnumerableFromCollection(["ALPHA"], System.StringComparer.OrdinalIgnoreCase);

        await Assert.That(set.SetEquals(["Beta"])).IsTrue();
        await Assert.That(collection.Count).IsEqualTo(1);
        await Assert.That(collection[0]).IsEqualTo("Beta");
    }
}
