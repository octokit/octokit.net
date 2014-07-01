using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class AuthorizationsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new AuthorizationsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void GetsAListOfAuthorizations()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.GetAll();

                client.Received().GetAll<Authorization>(Arg.Is<Uri>(u => u.ToString() == "authorizations"));
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

                client.Received().Get<Authorization>(Arg.Is<Uri>(u => u.ToString() == "authorizations/1"), null);
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

        public class TheCreateMethod
        {
            [Fact]
            public void SendsCreateToCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.Create(new NewAuthorization());

                client.Received().Post<Authorization>(Arg.Is<Uri>(u => u.ToString() == "authorizations")
                    , Args.NewAuthorization);
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

                client.Received().Put<Authorization>(Arg.Is<Uri>(u => u.ToString() == "authorizations/clients/clientId"),
                    Args.Object);
            }

            [Fact]
            public void GetsOrCreatesAuthenticationAtCorrectUrlUsingTwoFactor()
            {
                var data = new NewAuthorization();
                var client = Substitute.For<IApiConnection>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.GetOrCreateApplicationAuthentication("clientId", "secret", data, "two-factor");

                client.Received().Put<Authorization>(
                    Arg.Is<Uri>(u => u.ToString() == "authorizations/clients/clientId"),
                    Args.Object,
                    "two-factor");
            }

            [Fact]
            public async Task WrapsTwoFactorFailureWithTwoFactorException()
            {
                var data = new NewAuthorization();
                var client = Substitute.For<IApiConnection>();
                client.Put<Authorization>(Args.Uri, Args.Object, Args.String)
                    .ThrowsAsync<Authorization>(
                    new AuthorizationException(
                        new ApiResponse<object> { StatusCode = HttpStatusCode.Unauthorized }));
                var authEndpoint = new AuthorizationsClient(client);

                await AssertEx.Throws<TwoFactorChallengeFailedException>(async () =>
                    await authEndpoint.GetOrCreateApplicationAuthentication("clientId", "secret", data, "authenticationCode"));
            }

            [Fact]
            public async Task UsesCallbackToRetrieveTwoFactorCode()
            {
                var twoFactorChallengeResult = new TwoFactorChallengeResult("two-factor-code");
                var data = new NewAuthorization { Note = "note" };
                var client = Substitute.For<IAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication("clientId", "secret", Arg.Any<NewAuthorization>())
                    .Returns(_ => {throw new TwoFactorRequiredException();});
                client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(),
                    "two-factor-code")
                    .Returns(Task.Factory.StartNew(() => new Authorization {Token = "xyz"}));

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
                var challengeResults = new Queue<TwoFactorChallengeResult>(new []
                {
                    TwoFactorChallengeResult.RequestResendCode,
                    new TwoFactorChallengeResult("two-factor-code")
                });
                var data = new NewAuthorization();
                var client = Substitute.For<IAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication("clientId", "secret", Arg.Any<NewAuthorization>())
                    .Returns(_ => { throw new TwoFactorRequiredException(); });
                client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(),
                    "two-factor-code")
                    .Returns(Task.Factory.StartNew(() => new Authorization { Token = "OAUTHSECRET" }));

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
                    new TwoFactorChallengeResult("wrong-code"), 
                });
                var data = new NewAuthorization();
                var client = Substitute.For<IAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication("clientId", "secret", Arg.Any<NewAuthorization>())
                    .Returns(_ => { throw new TwoFactorRequiredException(); });
                client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(),
                    "wrong-code")
                    .Returns(_ => { throw new TwoFactorChallengeFailedException(); });
                
                var exception = await AssertEx.Throws<TwoFactorChallengeFailedException>(() =>
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
        }
    }
}
