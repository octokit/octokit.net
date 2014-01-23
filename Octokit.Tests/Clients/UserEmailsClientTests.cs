using NSubstitute;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class UserEmailsClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserEmailsClient(connection);

                client.GetAll();

                connection.Received(1)
                    .GetAll<EmailAddress>(Arg.Is<Uri>(u => u.ToString() == "user/emails"));
            }
        }

        public class TheAddMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserEmailsClient(connection);

                client.Add("octocat@github.com");

                connection.Received(1)
                    .Post<IReadOnlyList<string>>(Arg.Is<Uri>(u => u.ToString() == "user/emails"), Arg.Any<string[]>());
            }

            [Fact]
            public async Task EnsuresNonNullArgument()
            {
                var client = new UserEmailsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Add(null));
            }

            [Fact]
            public async Task EnsuresNoNullEmails()
            {
                var client = new UserEmailsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentException>(async () => await client.Add("octokit@github.com", null));
            }
        }
    }
}
