using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll(""));
            }
        }

        public class TheGetFollowingForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFollowersClient(githubClient);

                client.GetFollowingForCurrent();

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

                client.GetFollowing("alfhenrik");

                githubClient.Connection.Received(1).Get<List<User>>(
                    new Uri("users/alfhenrik/following", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableFollowersClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetFollowing(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetFollowing(""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.IsFollowingForCurrent(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.IsFollowingForCurrent(""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.IsFollowing(null, "alfhenrik-test"));
                await AssertEx.Throws<ArgumentException>(async () => await client.IsFollowing("", "alfhenrik-test"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.IsFollowing("alfhenrik", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.IsFollowing("alfhenrik", ""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Follow(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Follow(""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Unfollow(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Unfollow(""));
            }
        }
    }
}
