﻿using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableTeamsClientTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var team = new NewTeam("avengers");
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(github);

                client.Create("shield", team);

                github.Organization.Team.Received().Create("shield", team);
            }

            [Fact]
            public async Task EnsuresNotNullAndNonEmptyArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableTeamsClient(github);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("shield", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, new NewTeam("avengers")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", new NewTeam("avengers")).ToTask());
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableTeamsClient(null));
            }
        }
    }
}