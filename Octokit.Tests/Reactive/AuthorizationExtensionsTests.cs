using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class AuthorizationExtensionsTests
    {
        public class TheGetOrCreateApplicationAuthenticationMethod
        {
            [Fact]
            public async Task UsesCallbackToRetrievTwoFactorCode()
            {
                var firstResponse = new TwoFactorRequiredException(TwoFactorType.AuthenticatorApp);
                var twoFactorChallengeResult = new TwoFactorChallengeResult("two-factor-code");
                var secondResponse = new Authorization {Token = "OAUTHSECRET"};

                var client = Substitute.For<IObservableAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication(Args.String, Args.String, Args.NewAuthorization)
                    .Returns(Observable.Throw<Authorization>(firstResponse));
                client.GetOrCreateApplicationAuthentication(
                    Args.String,
                    Args.String,
                    Args.NewAuthorization,
                    "two-factor-code")
                    .Returns(Observable.Return(secondResponse));

                var result = await client.GetOrCreateApplicationAuthentication(
                    "clientId",
                    "secret",
                    new NewAuthorization { Note = "Was it this one?"},
                    _ => Observable.Return(twoFactorChallengeResult));

                Assert.Equal("OAUTHSECRET", result.Token);
                client.Received().GetOrCreateApplicationAuthentication(
                    "clientId", "secret", Arg.Is<NewAuthorization>(a => a.Note == "Was it this one?"));
                client.Received().GetOrCreateApplicationAuthentication(
                    "clientId", "secret", Arg.Is<NewAuthorization>(a => a.Note == "Was it this one?"),
                    "two-factor-code");
            }

            [Fact]
            public async Task RetriesWhenResendRequested()
            {
                var firstResponse = new TwoFactorRequiredException(TwoFactorType.AuthenticatorApp);
                var challengeResults = new Queue<TwoFactorChallengeResult>(new[]
                {
                    TwoFactorChallengeResult.RequestResendCode,
                    new TwoFactorChallengeResult("two-factor-code")
                });
                var secondResponse = new Authorization { Token = "OAUTHSECRET" };

                var client = Substitute.For<IObservableAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication(Args.String, Args.String, Args.NewAuthorization)
                    .Returns(Observable.Throw<Authorization>(firstResponse));
                client.GetOrCreateApplicationAuthentication(
                    Args.String,
                    Args.String,
                    Args.NewAuthorization,
                    "two-factor-code")
                    .Returns(Observable.Return(secondResponse));

                var result = await client.GetOrCreateApplicationAuthentication(
                    "clientId",
                    "secret",
                    new NewAuthorization { Note = "Was it this one?" },
                    _ => Observable.Return(challengeResults.Dequeue()));

                client.Received(2).GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>());
                client.Received().GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(), "two-factor-code");
                Assert.Equal("OAUTHSECRET", result.Token);
            }

            [Fact]
            public void ThrowsTwoFactorChallengeFailedExceptionWhenProvidedCodeIsIncorrect()
            {
                var challengeResults = new Queue<TwoFactorChallengeResult>(new[]
                {
                    TwoFactorChallengeResult.RequestResendCode,
                    new TwoFactorChallengeResult("wrong-code"), 
                });
                var twoFactorFailedException = new TwoFactorChallengeFailedException();
                var data = new NewAuthorization();
                var client = Substitute.For<IObservableAuthorizationsClient>();
                client.GetOrCreateApplicationAuthentication("clientId", "secret", Arg.Any<NewAuthorization>())
                    .Returns(Observable.Throw<Authorization>(new TwoFactorRequiredException()));
                client.GetOrCreateApplicationAuthentication("clientId",
                    "secret",
                    Arg.Any<NewAuthorization>(),
                    "wrong-code")
                    .Returns(Observable.Throw<Authorization>(twoFactorFailedException));
                var observer = Substitute.For<System.IObserver<Authorization>>();

                client.GetOrCreateApplicationAuthentication(
                        "clientId",
                        "secret",
                        data,
                        _ => Observable.Return(challengeResults.Dequeue()))
                        .Subscribe(observer);

                observer.Received().OnError(twoFactorFailedException);
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
