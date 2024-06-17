using System.Threading.Tasks;
using Xunit;

#if SODIUM_CORE_AVAILABLE
using Sodium;
#endif

namespace Octokit.Tests.Integration.Clients
{
    public class OrganizationCustomPropertiesClientTests
    {
        public class GetAllMethod
        {
            [OrganizationTest]
            public async Task GetCustomProperties()
            {
                var github = Helper.GetAuthenticatedClient();

                var customProperties = await github.Organization.CustomProperty.GetAll(Helper.Organization);

                Assert.NotEmpty(customProperties);
            }
        }

        /// <summary>
        /// Please create a custom property in your organization called TEST
        /// </summary>
        public class GetMethod
        {
            [OrganizationTest]
            public async Task GetCustomProperty()
            {
                var github = Helper.GetAuthenticatedClient();

                var customProperty = await github.Organization.CustomProperty.Get(Helper.Organization, "TEST");

                Assert.NotNull(customProperty);
                Assert.Equal("TEST", customProperty.PropertyName);
            }
        }

        public class CreateOrUpdateMethod
        {
#if SODIUM_CORE_AVAILABLE
            [OrganizationTest]
            public async Task UpsertCustomProperty()
            {
                var github = Helper.GetAuthenticatedClient();
                var upsertValue = GetCustomPropertyUpdateForCreate("value");

                var customProperty = await github.Organization.CustomProperty.CreateOrUpdate(Helper.Organization, "UPSERT_TEST", upsertValue);

                Assert.NotNull(customProperty);
                Assert.Equal("UPSERT_TEST", customProperty.PropertyName);
            }
#endif
        }

        public class DeleteMethod
        {
#if SODIUM_CORE_AVAILABLE
            [OrganizationTest]
            public async Task DeleteCustomProperty()
            {
                var github = Helper.GetAuthenticatedClient();

                var propertyName = "DELETE_TEST";

                var upsertValue = GetCustomPropertyUpdateForCreate("value");

                await github.Organization.CustomProperty.CreateOrUpdate(Helper.Organization, propertyName, upsertValue);
                await github.Organization.CustomProperty.Delete(Helper.Organization, propertyName);
            }
#endif
        }


#if SODIUM_CORE_AVAILABLE
        private static UpsertOrganizationCustomProperties GetCustomPropertiesForCreate(string propertyName, string value)
        {
            var properties = new UpsertOrganizationCustomProperties
            {
                Properties = new List<OrganizationCustomPropertyUpdate> { GetCustomPropertyUpdateForCreate(propertyName, value) };
            };

            return upsertValue;
        }

        private static OrganizationCustomPropertyUpdate GetCustomPropertyUpdateForCreate(string propertyName, string value)
        {
            return new OrganizationCustomPropertyUpdate(propertyName, CustomPropertyValueType.String, value);
        }

        private static UpsertOrganizationCustomProperty GetCustomPropertyUpdateForUpdate(string value)
        {
            return new UpsertOrganizationCustomProperty(CustomPropertyValueType.String, value);
        }
#endif

        private static async Task<Repository> CreateRepoIfNotExists(IGitHubClient github, string name)
        {
            try
            {
                var existingRepo = await github.Repository.Get(Helper.Organization, name);
                return existingRepo;
            }
            catch
            {
                var newRepo = await github.Repository.Create(Helper.Organization, new NewRepository(name));
                return newRepo;
            }
        }
    }
}
