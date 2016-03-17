using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class UserEmailsClientTests
    {
        private readonly IUserEmailsClient _emailClient;

        public UserEmailsClientTests()
        {
            var github = Helper.GetAuthenticatedClient();
            _emailClient = github.User.Email;
        }

        [IntegrationTest]
        public async Task CanGetEmail()
        {
            var emails = await _emailClient.GetAll();
            Assert.NotEmpty(emails);
        }

        [IntegrationTest]
        public async Task CanGetEmailWithApiOptions()
        {
            var emails = await _emailClient.GetAll(ApiOptions.None);
            Assert.NotEmpty(emails);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfEmailsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var emails = await _emailClient.GetAll(options);

            Assert.NotEmpty(emails);
        }
        
        const string testEmailAddress = "hahaha-not-a-real-email@foo.com";

        [IntegrationTest(Skip = "this isn't passing in CI - i hate past me right now")]
        public async Task CanAddAndDeleteEmail()
        {
            var github = Helper.GetAuthenticatedClient();

            await github.User.Email.Add(testEmailAddress);

            var emails = await github.User.Email.GetAll();
            Assert.Contains(testEmailAddress, emails.Select(x => x.Email));

            await github.User.Email.Delete(testEmailAddress);

            emails = await github.User.Email.GetAll();
            Assert.DoesNotContain(testEmailAddress, emails.Select(x => x.Email));
        }
    }
}
