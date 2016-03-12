using Xunit;

namespace Octokit.Tests.Integration.fixtures
{
    [CollectionDefinition(Name)]
    public class OrganizationsHooksCollection : ICollectionFixture<OrganizationsHooksFixture>
    {
        public const string Name = "Organization Hooks Collection";
    }
}
