﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class UserEmailsClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserEmailsClient(connection);

                client.GetAll();

                connection.Received(1)
                    .GetAll<EmailAddress>(Arg.Is<Uri>(u => u.ToString() == "user/emails"),
                        Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserEmailsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll(options);

                connection.Received(1)
                    .GetAll<EmailAddress>(Arg.Is<Uri>(u => u.ToString() == "user/emails"),
                        options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new UserEmailsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll(null));
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
                    .Post<IReadOnlyList<EmailAddress>>(Arg.Is<Uri>(u => u.ToString() == "user/emails"), Arg.Any<string[]>());
            }

            [Fact]
            public async Task EnsuresNonNullArgument()
            {
                var client = new UserEmailsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Add(null));
            }

            [Fact]
            public async Task EnsuresNoNullEmails()
            {
                var client = new UserEmailsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Add("octokit@github.com", null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserEmailsClient(connection);

                client.Delete("octocat@github.com");

                connection.Received(1)
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "user/emails"), Arg.Any<string[]>());
            }

            [Fact]
            public async Task EnsuresNonNullArgument()
            {
                var client = new UserEmailsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null));
            }

            [Fact]
            public async Task EnsuresNoNullEmails()
            {
                var client = new UserEmailsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("octokit@github.com", null));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new UserEmailsClient(null));
            }
        }
    }
}
