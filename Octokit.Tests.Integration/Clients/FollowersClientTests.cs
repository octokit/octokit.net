using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class FollowersClientTests : IDisposable
{
    readonly GitHubClient _github;
    readonly User _currentUser;

    public FollowersClientTests()
    {
        _github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _currentUser = _github.User.Current().Result;
    }

    [IntegrationTest]
    public async Task ReturnsUsersTheCurrentUserIsFollowing()
    {
        await _github.User.Followers.Follow("alfhenrik");

        var following = await _github.User.Followers.GetFollowingForCurrent();

        Assert.NotNull(following);
        Assert.True(following.Any(f => f.Login == "alfhenrik"));
    }

    [IntegrationTest]
    public async Task ReturnsUsersTheUserIsFollowing()
    {
        var following = await _github.User.Followers.GetFollowing("alfhenrik");

        Assert.NotNull(following);
        Assert.NotEmpty(following);
    }
    
    [IntegrationTest]
    public async Task ReturnsUsersFollowingTheUser()
    {
        await _github.User.Followers.Follow("alfhenrik");

        var followers = await _github.User.Followers.GetAll("alfhenrik");

        Assert.NotEmpty(followers);
        Assert.True(followers.Any(f => f.Login == _currentUser.Login));
    }

    [IntegrationTest]
    public async Task ChecksIfIsFollowingUserWhenFollowingUser()
    {
        await _github.User.Followers.Follow("alfhenrik");

        var isFollowing = await _github.User.Followers.IsFollowingForCurrent("alfhenrik");

        Assert.True(isFollowing);
    }

    [IntegrationTest]
    public async Task ChecksIfIsFollowingUserWhenNotFollowingUser()
    {
        var isFollowing = await _github.User.Followers.IsFollowingForCurrent("alfhenrik");

        Assert.False(isFollowing);
    }

    [IntegrationTest]
    public async Task FollowUserNotBeingFollowedByTheUser()
    {
        var result = await _github.User.Followers.Follow("alfhenrik");
        var following = await _github.User.Followers.GetFollowingForCurrent();

        Assert.True(result);
        Assert.NotEmpty(following);
        Assert.True(following.Any(f => f.Login == "alfhenrik"));
    }

    [IntegrationTest]
    public async Task UnfollowUserBeingFollowedByTheUser()
    {
        await _github.User.Followers.Follow("alfhenrik");
        var followers = await _github.User.Followers.GetAll("alfhenrik");
        Assert.True(followers.Any(f => f.Login == _currentUser.Login));

        await _github.User.Followers.Unfollow("alfhenrik");
        followers = await _github.User.Followers.GetAll("alfhenrik");
        Assert.False(followers.Any(f => f.Login == _currentUser.Login));
    }

    [IntegrationTest]
    public async Task UnfollowUserNotBeingFollowedTheUser()
    {
        var followers = await _github.User.Followers.GetAll("alfhenrik");
        Assert.False(followers.Any(f => f.Login == _currentUser.Login));

        await _github.User.Followers.Unfollow("alfhenrik");
    }

    public void Dispose()
    {
        _github.User.Followers.Unfollow("alfhenrik");
    }
}
