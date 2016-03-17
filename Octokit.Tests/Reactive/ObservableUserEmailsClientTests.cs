using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableUserEmailsClientTests
    {
        private static ObservableUserEmailsClient CreateFixtureWithNonReactiveClient()
        {
            var nonreactiveClient = new UserEmailsClient(Substitute.For<IApiConnection>());
            var github = Substitute.For<IGitHubClient>();
            github.User.Email.Returns(nonreactiveClient);
            return new ObservableUserEmailsClient(github);
        }

        public class TheGetAllMethod
        {
            private static readonly Uri _expectedUri = new Uri("user/emails", UriKind.Relative);

            [Fact]
            public void GetsCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableUserEmailsClient(github);

                client.GetAll();

                github.Connection.Received(1).Get<List<EmailAddress>>(_expectedUri,
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0), null);
            }

            [Fact]
            public void GetsCorrectUrlWithApiOption()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableUserEmailsClient(github);

                client.GetAll(ApiOptions.None);

                github.Connection.Received(1).Get<List<EmailAddress>>(_expectedUri,
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0), null);
            }
        }

        public class TheAddMethod
        {
            [Fact]
            public void CallsAddOnClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableUserEmailsClient(github);
                string email = "octo@github.com";

                client.Add(email);

                github.User.Email.Received(1).Add(Arg.Is(email));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = CreateFixtureWithNonReactiveClient();

                Assert.Throws<ArgumentNullException>(() => client.Add(null));
                Assert.Throws<ArgumentException>(() => client.Add("octo@github.com", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = CreateFixtureWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => client.Add(""));
                Assert.Throws<ArgumentException>(() => client.Add("octo@github.com", ""));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableUserEmailsClient(null));
            }
        }
    }
}
