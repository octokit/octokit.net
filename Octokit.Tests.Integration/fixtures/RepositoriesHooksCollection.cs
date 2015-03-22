using Xunit;

namespace Octokit.Tests.Integration.fixtures
{
    [CollectionDefinition("Repositories Hooks Collection")]
    public class RepositoriesHooksCollection : ICollectionFixture<RepositoriesHooksFixture>
    {
    }
}
