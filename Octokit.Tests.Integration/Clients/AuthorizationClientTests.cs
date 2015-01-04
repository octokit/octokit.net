using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class AuthorizationClientTests
    {
        public class TheCreateMethod
        {
            [ApplicationTest]
            public async Task CanCreateAnAuthorization()
            {
                var client = Helper.GetAuthenticatedClient();
                var newAuthorization = new NewAuthorization();
                // TODO: probably some more setup work here
                var authorization = await client.Authorization.Create(newAuthorization);

                Assert.NotNull(authorization);
            }
        }

    }
}
