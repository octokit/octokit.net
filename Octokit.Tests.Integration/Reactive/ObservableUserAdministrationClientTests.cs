using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class ObservableUserAdministrationClientTests
    {
        readonly IObservableGitHubClient _github;

        public ObservableUserAdministrationClientTests()
        {
            _github = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
        }

        private NewUser GenerateNewUserDetails()
        {
            string username = Helper.MakeNameWithTimestamp("user");
            string email = string.Concat(username, "@company.com");
            return new NewUser(username, email);
        }

        [GitHubEnterpriseTest]
        public async Task CanCreateAndDelete()
        {
            User checkUser = null;

            // Create a new user
            var newUser = GenerateNewUserDetails();

            var observable = _github.User.Administration.Create(newUser);
            var user = await observable;

            // Check returned object (cant check email as it isn't public)
            Assert.NotNull(user);
            Assert.Equal(user.Login, newUser.Login);

            // Get user to check they exist
            checkUser = await _github.User.Get(newUser.Login);
            Assert.Equal(checkUser.Login, newUser.Login);

            // Delete the user
            await _github.User.Administration.Delete(newUser.Login);

            // Ensure user doesnt exist
            try
            {
                checkUser = await _github.User.Get(newUser.Login);
                if (checkUser != null)
                {
                    throw new Exception("User still exists!");
                }
            }
            catch (ApiException e)
            {
                if (e.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    throw;
                }
            }
        }

        [GitHubEnterpriseTest]
        public async Task CanRename()
        {
            string renamedUsername = Helper.MakeNameWithTimestamp("user-renamed");
            // Create a disposable user for the test
            using (var context = _github.CreateEnterpriseUserContext(GenerateNewUserDetails()).Result)
            {
                var observable = _github.User.Administration.Rename(
                    context.UserLogin,
                    new UserRename(renamedUsername));
                var response = await observable;

                Assert.NotNull(response);
                Assert.StartsWith("Job queued to rename user", response.Message);
                Assert.EndsWith(context.UserId.ToString(), response.Url);
            }

            // Remove user if it was already renamed
            EnterpriseHelper.DeleteUser(_github.Connection, renamedUsername);
        }

        [GitHubEnterpriseTest]
        public async Task CanAddAndDeleteImpersonationToken()
        {
            // Create a disposable user for the test
            using (var context = _github.CreateEnterpriseUserContext(GenerateNewUserDetails()).Result)
            {
                // Create Impersonation token
                var observable = _github.User.Administration.CreateImpersonationToken(
                    context.UserLogin,
                    new NewImpersonationToken(new[] { "public_repo" }));
                var token = await observable;

                Assert.NotNull(token);
                Assert.True(
                    token.Scopes.Count() == 1 &&
                    token.Scopes.All(s => s == "public_repo"));

                // Delete Impersonation token
                await _github.User.Administration.DeleteImpersonationToken(context.UserLogin);
            }
        }

        [GitHubEnterpriseTest]
        public async Task CanPromoteAndDemote()
        {
            User checkUser = null;

            // Create a disposable user for the test
            using (var context = _github.CreateEnterpriseUserContext(GenerateNewUserDetails()).Result)
            {
                // Ensure user is not site admin
                checkUser = await _github.User.Get(context.UserLogin);
                Assert.False(checkUser.SiteAdmin);

                // Promote to site admin
                await _github.User.Administration.Promote(context.UserLogin);

                // Ensure user is site admin
                checkUser = await _github.User.Get(context.UserLogin);
                Assert.True(checkUser.SiteAdmin);

                // Demote user
                await _github.User.Administration.Demote(context.UserLogin);

                // Ensure user is not site admin
                checkUser = await _github.User.Get(context.UserLogin);
                Assert.False(checkUser.SiteAdmin);
            }
        }

        [GitHubEnterpriseTest]
        public async Task CanSuspendAndUnsuspend()
        {
            User checkUser = null;

            // Create a disposable user for the test
            using (var context = _github.CreateEnterpriseUserContext(GenerateNewUserDetails()).Result)
            {
                // Ensure user is not suspended
                checkUser = await _github.User.Get(context.UserLogin);
                Assert.False(checkUser.Suspended);

                // Suspend user
                await _github.User.Administration.Suspend(context.UserLogin);

                // Ensure user is suspended
                checkUser = await _github.User.Get(context.UserLogin);
                Assert.True(checkUser.Suspended);

                // Unsuspend user
                await _github.User.Administration.Unsuspend(context.UserLogin);

                // Ensure user is not suspended
                checkUser = await _github.User.Get(context.UserLogin);
                Assert.False(checkUser.Suspended);
            }
        }

        [GitHubEnterpriseTest(Skip = "Currently no way to add keys, so cant test listing keys")]
        public async Task CanListAllPublicKeys()
        {
            // Create a disposable user for the test
            using (var context = _github.CreateEnterpriseUserContext(GenerateNewUserDetails()).Result)
            {
                // Ensure user has a key
                //var key = await _github.User.Keys.Create(new NewPublicKey("title", "key"));

                // Get public keys
                var observable = _github.User.Administration.ListAllPublicKeys();
                var keys = await observable.ToList();

                Assert.NotNull(keys);
                Assert.True(keys.Count > 0);

                // Delete key
                //await _github.User.Administration.DeletePublicKey(key.Id);
            }
        }

        [GitHubEnterpriseTest(Skip = "Currently no way to add keys, so cant test deleting keys")]
        public async Task CanDeletePublicKey()
        {
            // Create a disposable user for the test
            using (var context = _github.CreateEnterpriseUserContext(GenerateNewUserDetails()).Result)
            {
                // Ensure user has a key
                //var key = await _github.User.Keys.Create(new NewPublicKey("title", "key"));

                // Delete key
                //await _github.User.Administration.DeletePublicKey(key.Id);
            }
        }
    }
}
