using Xunit;

namespace CaWorkshop.Application.UnitTests
{
    [CollectionDefinition(nameof(QueryCollection))]
    public class QueryCollection
        : ICollectionFixture<TestFixture>
    { }
}