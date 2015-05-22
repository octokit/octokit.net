using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableFollowersTest
    {
        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.GetAllForCurrent();

                githubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("user/followers", UriKind.Relative), null, null);
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.GetAll("alfhenrik");

                githubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("users/alfhenrik/followers", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableFollowersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("").ToTask());
            }
        }

        public class TheGetFollowingForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.GetAllFollowingForCurrent();

                githubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("user/following", UriKind.Relative), null, null);
            }
        }

        public class TheGetFollowingMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.GetAllFollowing("alfhenrik");

                githubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("users/alfhenrik/following", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableFollowersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllFollowing(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllFollowing("").ToTask());
            }
        }

        public class TheIsFollowingForCurrentMethod
        {
            [Fact]
            public void IsFollowingForCurrentFromClientUserFollowers()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.IsFollowingForCurrent("alfhenrik");

                githubClient.User.Followers.Received()
                    .IsFollowingForCurrent("alfhenrik");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableFollowersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsFollowingForCurrent(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsFollowingForCurrent("").ToTask());
            }
        }

        public class TheIsFollowingMethod
        {
            [Fact]
            public void IsFollowingFromClientUserFollowers()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.IsFollowing("alfhenrik", "alfhenrik-test");

                githubClient.User.Followers.Received()
                    .IsFollowing("alfhenrik", "alfhenrik-test");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableFollowersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsFollowing(null, "alfhenrik-test").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsFollowing("", "alfhenrik-test").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsFollowing("alfhenrik", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsFollowing("alfhenrik", "").ToTask());
            }
        }

        public class TheFollowMethod
        {
            [Fact]
            public void FollowFromClientUserFollowers()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.Follow("alfhenrik");

                githubClient.User.Followers.Received()
                    .Follow("alfhenrik");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableFollowersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Follow(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Follow("").ToTask());
            }
        }

        public class TheUnfollowMethod
        {
            [Fact]
            public void UnfollowFromClientUserFollowers()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.Unfollow("alfhenrik");

                githubClient.User.Followers.Received()
                    .Unfollow("alfhenrik");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableFollowersClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Unfollow(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Unfollow("").ToTask());
            }
        }
    }
}
