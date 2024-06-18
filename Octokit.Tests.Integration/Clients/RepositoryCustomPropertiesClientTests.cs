using System.Threading.Tasks;
using Xunit;

#if SODIUM_CORE_AVAILABLE
using Sodium;
#endif

namespace Octokit.Tests.Integration.Clients
{
    /// <summary>
    /// Access to view and update custom property values is required for the following tests
    /// </summary>
    public class RepositoryCustomPropertiesClientTests
    {
        /// <summary>
        /// Fill these in for tests to work
        /// </summary>
        internal const string OWNER = "octokit";
        internal const string REPO = "octokit.net";

        public class GetAllMethod
        {
            [IntegrationTest]
            public async Task GetPropertyValues()
            {
                var github = Helper.GetAuthenticatedClient();

                var propertyValues = await github.Repository.CustomProperty.GetAll(OWNER, REPO);

                Assert.NotEmpty(propertyValues);
            }
        }

        public class CreateOrUpdateMethod
        {
#if SODIUM_CORE_AVAILABLE
            [IntegrationTest]
            public async Task UpsertPropertyValues()
            {
                var github = Helper.GetAuthenticatedClient();
                var now = DateTime.Now;

                var upsertValue = new UpsertRepositoryCustomPropertyValues
                {
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate("TEST", "UPSERT_TEST")
                    }
                };

                var updatedValues = await github.Repository.CustomProperty.CreateOrUpdate(OWNER, REPO, upsertValue);

                Assert.NotNull(updatedValues);
                Assert.True(updatedValues.Count > 0);
            }
#endif
        }
    }
}
