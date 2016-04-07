using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableAuthorizationsClientTests
    {
        readonly ObservableAuthorizationsClient _authorizationsClient;

        public ObservableAuthorizationsClientTests()
        {
            var github = Helper.GetBasicAuthClient();

            _authorizationsClient = new ObservableAuthorizationsClient(github);
        }

        [IntegrationTest]
        public async Task CanGetAuthorization()
        {
            var authorization = await _authorizationsClient.GetAll();
            Assert.NotNull(authorization);
        }

        [IntegrationTest]
        public async Task CanGetAuthorizationWithApiOptions()
        {
            var authorization = await _authorizationsClient.GetAll(ApiOptions.None);
            Assert.NotNull(authorization);
        }

        [IntegrationTest]
        public async Task ReturnsNotEmptyAuthorizationsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var authorizations = await _authorizationsClient.GetAll(options).ToList();
            Assert.NotEmpty(authorizations);
        }
    }
}
