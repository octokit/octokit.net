using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableUsersClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                Uri expectedUri = new Uri("users?since=1", UriKind.Relative);
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableUsersClient(github);

                client.GetAll("1");

                github.User.Received(1).GetAll("1");
            }
        }
    }
}
