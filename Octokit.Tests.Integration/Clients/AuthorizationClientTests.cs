using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class AuthorizationClientTests
    {
        [BasicAuthenticationTest]
        public async Task CanCreatePersonalToken()
        {
            var github = Helper.GetBasicAuthClient();
            var note = Helper.MakeNameWithTimestamp("Testing authentication");
            var newAuthorization = new NewAuthorization(
                note,
                new string[] { "user" });

            var created = await github.Authorization.Create(newAuthorization);

            Assert.False(string.IsNullOrWhiteSpace(created.Token));
            Assert.False(string.IsNullOrWhiteSpace(created.TokenLastEight));
            Assert.False(string.IsNullOrWhiteSpace(created.HashedToken));

            var get = await github.Authorization.Get(created.Id);

            Assert.Equal(created.Id, get.Id);
            Assert.Equal(created.Note, get.Note);
        }

        [IntegrationTest]
        public async Task CannotCreatePersonalTokenWhenUsingOauthTokenCredentials()
        {
            var github = Helper.GetAuthenticatedClient();
            var note = Helper.MakeNameWithTimestamp("Testing authentication");
            var newAuthorization = new NewAuthorization(
                note,
                new string[] { "user" });

            var error = Assert.ThrowsAsync<ForbiddenException>(() => github.Authorization.Create(newAuthorization));
            Assert.True(error.Result.Message.Contains("username and password Basic Auth"));
        }

        [BasicAuthenticationTest(Skip = "See https://github.com/octokit/octokit.net/issues/1000 for issue to investigate this further")]
        public async Task CanCreateAndGetAuthorizationWithoutFingerPrint()
        {
            var github = Helper.GetBasicAuthClient();
            var note = Helper.MakeNameWithTimestamp("Testing authentication");
            var newAuthorization = new NewAuthorization(
                note,
                new[] { "user" });

            // the first call will create the authorization
            var created = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                newAuthorization);

            Assert.False(string.IsNullOrWhiteSpace(created.Token));
            Assert.False(string.IsNullOrWhiteSpace(created.TokenLastEight));
            Assert.False(string.IsNullOrWhiteSpace(created.HashedToken));

            // we can then query it through the regular API
            var get = await github.Authorization.Get(created.Id);

            Assert.Equal(created.Id, get.Id);
            Assert.Equal(created.Note, get.Note);

            // but the second time we call this API we get
            // a different set of data
            var getExisting = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                newAuthorization);

            Assert.Equal(created.Id, getExisting.Id);

            // the token is no longer returned for subsequent calls
            Assert.True(string.IsNullOrWhiteSpace(getExisting.Token));
            // however the hashed and last 8 characters are available
            Assert.False(string.IsNullOrWhiteSpace(getExisting.TokenLastEight));
            Assert.False(string.IsNullOrWhiteSpace(getExisting.HashedToken));

            await github.Authorization.Delete(created.Id);
        }

        [BasicAuthenticationTest]
        public async Task CanCreateAndGetAuthorizationByFingerprint()
        {
            var github = Helper.GetBasicAuthClient();
            var fingerprint = Helper.MakeNameWithTimestamp("authorization-testing");
            var note = Helper.MakeNameWithTimestamp("Testing authentication");
            var newAuthorization = new NewAuthorization(
                note,
                new[] { "user" },
                fingerprint);

            var created = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                newAuthorization);

            Assert.NotNull(created);
            Assert.False(string.IsNullOrWhiteSpace(created.Token));

            // we can then query it through the regular API
            var get = await github.Authorization.Get(created.Id);

            Assert.Equal(created.Id, get.Id);
            Assert.Equal(created.Note, get.Note);

            // but the second time we call this API we get
            // a different set of data
            var getExisting = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                newAuthorization);

            Assert.Equal(created.Id, getExisting.Id);

            // NOTE: the new API will no longer return the full
            //       token as soon as you specify a Fingerprint
            Assert.True(string.IsNullOrWhiteSpace(getExisting.Token));

            // NOTE: however you will get these two new properties
            //       to help identify the authorization at hand
            Assert.False(string.IsNullOrWhiteSpace(getExisting.TokenLastEight));
            Assert.False(string.IsNullOrWhiteSpace(getExisting.HashedToken));

            await github.Authorization.Delete(created.Id);
        }

        [BasicAuthenticationTest]
        public async Task CanCheckApplicationAuthentication()
        {
            var github = Helper.GetBasicAuthClient();
            var fingerprint = Helper.MakeNameWithTimestamp("authorization-testing");
            var note = Helper.MakeNameWithTimestamp("Testing authentication");
            var newAuthorization = new NewAuthorization(
                note,
                new[] { "user" },
                fingerprint);

            var created = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                newAuthorization);

            var applicationClient = Helper.GetAuthenticatedApplicationClient();
            var applicationAuthorization = await applicationClient.Authorization.CheckApplicationAuthentication(Helper.ClientId, created.Token);

            Assert.NotNull(applicationAuthorization);
            Assert.Equal(created.Token, applicationAuthorization.Token);

            await github.Authorization.Delete(created.Id);
            Assert.ThrowsAsync<NotFoundException>(() => github.Authorization.Get(created.Id));
        }

        [BasicAuthenticationTest]
        public async Task CanResetApplicationAuthentication()
        {
            var github = Helper.GetBasicAuthClient();
            var fingerprint = Helper.MakeNameWithTimestamp("authorization-testing");
            var note = Helper.MakeNameWithTimestamp("Testing authentication");
            var newAuthorization = new NewAuthorization(
                note,
                new[] { "user" },
                fingerprint);

            var created = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                newAuthorization);

            var applicationClient = Helper.GetAuthenticatedApplicationClient();
            var applicationAuthorization = await applicationClient.Authorization.ResetApplicationAuthentication(Helper.ClientId, created.Token);

            Assert.NotNull(applicationAuthorization);
            Assert.NotEqual(created.Token, applicationAuthorization.Token);

            await github.Authorization.Delete(created.Id);
            Assert.ThrowsAsync<NotFoundException>(() => github.Authorization.Get(created.Id));
        }

        [BasicAuthenticationTest]
        public async Task CanRevokeApplicationAuthentication()
        {
            var github = Helper.GetBasicAuthClient();
            var fingerprint = Helper.MakeNameWithTimestamp("authorization-testing");
            var note = Helper.MakeNameWithTimestamp("Testing authentication");
            var newAuthorization = new NewAuthorization(
                note,
                new[] { "user" },
                fingerprint);

            var created = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                newAuthorization);

            var applicationClient = Helper.GetAuthenticatedApplicationClient();
            await applicationClient.Authorization.RevokeApplicationAuthentication(Helper.ClientId, created.Token);

            Assert.ThrowsAsync<NotFoundException>(() => applicationClient.Authorization.CheckApplicationAuthentication(Helper.ClientId, created.Token));
            Assert.ThrowsAsync<NotFoundException>(() => github.Authorization.Get(created.Id));
        }

        [BasicAuthenticationTest(Skip = "See https://github.com/octokit/octokit.net/issues/1078 for explanation of why this is now obsolete")]
        public async Task CanRevokeAllApplicationAuthentications()
        {
            var github = Helper.GetBasicAuthClient();

            var fingerprint = Helper.MakeNameWithTimestamp("authorization-testing");
            var note = Helper.MakeNameWithTimestamp("Testing authentication");
            var token1 = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                new NewAuthorization(
                    note,
                    new[] { "user" },
                    fingerprint));

            fingerprint = Helper.MakeNameWithTimestamp("authorization-testing-2");
            note = Helper.MakeNameWithTimestamp("Testing authentication 2");
            var token2 = await github.Authorization.GetOrCreateApplicationAuthentication(
                Helper.ClientId,
                Helper.ClientSecret,
                new NewAuthorization(
                    note,
                    new[] { "user" },
                    fingerprint));

            var applicationClient = Helper.GetAuthenticatedApplicationClient();
            await applicationClient.Authorization.RevokeAllApplicationAuthentications(Helper.ClientId);

            Assert.ThrowsAsync<NotFoundException>(async () =>
                await applicationClient.Authorization.CheckApplicationAuthentication(Helper.ClientId, token1.Token));
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await applicationClient.Authorization.CheckApplicationAuthentication(Helper.ClientId, token2.Token));

            Assert.ThrowsAsync<NotFoundException>(() => github.Authorization.Get(token1.Id));
            Assert.ThrowsAsync<NotFoundException>(() => github.Authorization.Get(token2.Id));
        }
    }
}
