using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class AuthorizationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new AuthorizationsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.GetAll();

                client.Received().GetAll<Authorization>(
                    Arg.Is<Uri>(u => u.ToString() == "authorizations"),
                    Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };

                authEndpoint.GetAll(options);

                client.Received().GetAll<Authorization>(
                    Arg.Is<Uri>(u => u.ToString() == "authorizations"),
                    options);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                await Assert.ThrowsAsync<ArgumentNullException>(() => authEndpoint.GetAll(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void GetsAnAuthorization()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.Get(1);

                client.Received().Get<Authorization>(
                    Arg.Is<Uri>(u => u.ToString() == "authorizations/1"),
                    null);
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void SendsUpdateToCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.Update(1, new AuthorizationUpdate());

                client.Received().Patch<Authorization>(Arg.Is<Uri>(u => u.ToString() == "authorizations/1"),
                    Args.AuthorizationUpdate);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.Delete(1);

                client.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "authorizations/1"));
            }
        }

        public class TheGetOrCreateApplicationAuthenticationMethod
        {
            [Fact]
            public void GetsOrCreatesAuthenticationAtCorrectUrl()
            {
                var data = new NewAuthorization();
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.GetOrCreateApplicationAuthentication("clientId", "secret", data);

                client.Received().Put<ApplicationAuthorization>(Arg.Is<Uri>(u => u.ToString() == "authorizations/clients/clientId"),
                    Args.Object);
            }

            [Fact]
            public void GetsOrCreatesAuthenticationAtCorrectUrlUsingTwoFactor()
            {
                var data = new NewAuthorization();
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.GetOrCreateApplicationAuthentication("clientId", "secret", data, "two-factor");

                client.Received().Put<ApplicationAuthorization>(
                    Arg.Is<Uri>(u => u.ToString() == "authorizations/clients/clientId"),
                    Args.Object,
                    "two-factor");
            }

            [Fact]
            public async Task WrapsTwoFactorFailureWithTwoFactorException()
            {
                var data = new NewAuthorization();
                var client = Substitute.For<IApiConnection>();
                client.Put<ApplicationAuthorization>(Args.Uri, Args.Object, Args.String)
                    .ThrowsAsync<ApplicationAuthorization>(
                    new AuthorizationException(
                        new Response(HttpStatusCode.Unauthorized, null, new Dictionary<string, string>(), "application/json")));
                var authEndpoint = new AuthorizationsClient(client);

                await Assert.ThrowsAsync<TwoFactorChallengeFailedException>(() =>
                    authEndpoint.GetOrCreateApplicationAuthentication("clientId", "secret", data, "authenticationCode"));
            }

            [Fact]
            public async Task UsesCallbackToRetrieveTwoFactorCode()
            {
                var twoFactorChallengeResult = new TwoFactorChallengeResult("two-factor-code");
                var data = new NewAuthorization { Note = "note" };
                var client = Substitute.For<IAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication("clientId", "secret", Arg.Any<NewAuthorization>())
                    .Returns<Task<ApplicationAuthorization>>(_ => { throw new TwoFactorRequiredException(); });
                client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(),
                    "two-factor-code")
                    .Returns(Task.Factory.StartNew(() => new ApplicationAuthorization(0, null, null, null, null, null, null, null, DateTimeOffset.Now, DateTimeOffset.Now, null, "xyz")));

                var result = await client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    data,
                    e => Task.Factory.StartNew(() => twoFactorChallengeResult));

                client.Received().GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Is<NewAuthorization>(u => u.Note == "note"));
                client.Received().GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(), "two-factor-code");
                Assert.Equal("xyz", result.Token);
            }

            [Fact]
            public async Task RetriesWhenResendRequested()
            {
                var challengeResults = new Queue<TwoFactorChallengeResult>(new[]
                {
                    TwoFactorChallengeResult.RequestResendCode,
                    new TwoFactorChallengeResult("two-factor-code")
                });
                var data = new NewAuthorization();
                var client = Substitute.For<IAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication("clientId", "secret", Arg.Any<NewAuthorization>())
                    .Returns<Task<ApplicationAuthorization>>(_ => { throw new TwoFactorRequiredException(); });
                client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(),
                    "two-factor-code")
                    .Returns(Task.Factory.StartNew(() => new ApplicationAuthorization(0, null, null, null, null, null, null, null, DateTimeOffset.Now, DateTimeOffset.Now, null, "OAUTHSECRET")));

                var result = await client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    data,
                    e => Task.Factory.StartNew(() => challengeResults.Dequeue()));

                client.Received(2).GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Args.NewAuthorization);
                client.Received().GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Args.NewAuthorization,
                    "two-factor-code");
                Assert.Equal("OAUTHSECRET", result.Token);
            }

            [Fact]
            public async Task ThrowsTwoFactorChallengeFailedExceptionWhenProvidedCodeIsIncorrect()
            {
                var challengeResults = new Queue<TwoFactorChallengeResult>(new[]
                {
                    TwoFactorChallengeResult.RequestResendCode,
                    new TwoFactorChallengeResult("wrong-code")
                });
                var data = new NewAuthorization();
                var client = Substitute.For<IAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication("clientId", "secret", Arg.Any<NewAuthorization>())
                    .Returns<Task<ApplicationAuthorization>>(_ => { throw new TwoFactorRequiredException(); });
                client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(),
                    "wrong-code")
                    .Returns<Task<ApplicationAuthorization>>(_ => { throw new TwoFactorChallengeFailedException(); });

                var exception = await Assert.ThrowsAsync<TwoFactorChallengeFailedException>(() =>
                    client.GetOrCreateApplicationAuthentication(
                        "clientId",
                        "secret",
                        data,
                        e => Task.Factory.StartNew(() => challengeResults.Dequeue())));

                Assert.NotNull(exception);
                client.Received().GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>());
                client.Received().GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(),
                    "wrong-code");
            }

            [Fact]
            public async Task GetsOrCreatesAuthenticationWithFingerprintAtCorrectUrl()
            {
                var data = new NewAuthorization { Fingerprint = "ha-ha-fingerprint" };
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                Uri calledUri = null;
                dynamic calledBody = null;

                client.Put<ApplicationAuthorization>(Arg.Do<Uri>(u => calledUri = u), Arg.Do<dynamic>(body => calledBody = body));

                authEndpoint.GetOrCreateApplicationAuthentication("clientId", "secret", data);

                Assert.NotNull(calledUri);
                Assert.Equal("authorizations/clients/clientId", calledUri.ToString());

                Assert.NotNull(calledBody);
                var fingerprintProperty = ((IEnumerable<PropertyInfo>)calledBody.GetType().DeclaredProperties).FirstOrDefault(x => x.Name == "fingerprint");
                Assert.NotNull(fingerprintProperty);
                Assert.Equal(fingerprintProperty.GetValue(calledBody), "ha-ha-fingerprint");
            }
        }

        public class TheCheckApplicationAuthenticationMethod
        {
            [Fact]
            public async Task ChecksApplicationAuthenticateAtCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.CheckApplicationAuthentication("clientId", "accessToken");

                client.Received().Get<ApplicationAuthorization>(
                    Arg.Is<Uri>(u => u.ToString() == "applications/clientId/tokens/accessToken"),
                    null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                await Assert.ThrowsAsync<ArgumentNullException>(() => authEndpoint.CheckApplicationAuthentication(null, "accessToken"));
                await Assert.ThrowsAsync<ArgumentException>(() => authEndpoint.CheckApplicationAuthentication("", "accessToken"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => authEndpoint.CheckApplicationAuthentication("clientId", null));
                await Assert.ThrowsAsync<ArgumentException>(() => authEndpoint.CheckApplicationAuthentication("clientId", ""));
            }
        }

        public class TheResetApplicationAuthenticationMethod
        {
            [Fact]
            public async Task ResetsApplicationAuthenticationAtCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.ResetApplicationAuthentication("clientId", "accessToken");

                client.Received().Post<ApplicationAuthorization>(
                    Arg.Is<Uri>(u => u.ToString() == "applications/clientId/tokens/accessToken"),
                    Args.Object);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                await Assert.ThrowsAsync<ArgumentNullException>(() => authEndpoint.ResetApplicationAuthentication(null, "accessToken"));
                await Assert.ThrowsAsync<ArgumentException>(() => authEndpoint.ResetApplicationAuthentication("", "accessToken"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => authEndpoint.ResetApplicationAuthentication("clientId", null));
                await Assert.ThrowsAsync<ArgumentException>(() => authEndpoint.ResetApplicationAuthentication("clientId", ""));
            }
        }

        public class TheRevokeApplicationAuthenticationMethod
        {
            [Fact]
            public async Task RevokesApplicationAuthenticationAtCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.RevokeApplicationAuthentication("clientId", "accessToken");

                client.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "applications/clientId/tokens/accessToken"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                await Assert.ThrowsAsync<ArgumentNullException>(() => authEndpoint.RevokeApplicationAuthentication(null, "accessToken"));
                await Assert.ThrowsAsync<ArgumentException>(() => authEndpoint.RevokeApplicationAuthentication("", "accessToken"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => authEndpoint.RevokeApplicationAuthentication("clientId", null));
                await Assert.ThrowsAsync<ArgumentException>(() => authEndpoint.RevokeApplicationAuthentication("clientId", ""));
            }
        }
    }
}
