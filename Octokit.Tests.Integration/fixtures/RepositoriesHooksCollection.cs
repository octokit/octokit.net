using Xunit;

namespace Octokit.Tests.Integration.Fixtures
{
    [CollectionDefinition(Name)]
    public class RepositoriesHooksCollection : ICollectionFixture<RepositoriesHooksFixture>
    {
        public const string Name = "Repositories Hooks Collection";
    }
}
