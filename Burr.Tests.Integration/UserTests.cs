using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Burr.Tests.Integration
{
    public class UserTests
    {
        [Fact]
        public async Task CanGetUser()
        {
            var client = new GitHubClient();

            var user = await client.Users.GetAsync("tclem");

            user.Should().NotBeNull();
            user.Followers.Should().Be(69);
            user.Type.Should().Be("User");
            user.Hireable.Should().BeFalse();
            user.AvatarUrl.Should().Be("https://secure.gravatar.com/avatar/2f4861b27dc35663ed271d39f5358261?d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-140.png");
            user.Bio.Should().BeNull();
            user.HtmlUrl.Should().Be("https://github.com/tclem");
            user.CreatedAt.Should().Be(DateTimeOffset.Parse("2009-10-07T20:26:53Z"));
            user.PublicRepos.Should().Be(20);
            user.Blog.Should().Be("http://timclem.wordpress.com");
            user.Url.Should().Be("https://api.github.com/users/tclem");
            user.PublicGists.Should().Be(6);
            user.Following.Should().Be(8);
            user.Company.Should().Be("GitHub");
            user.Name.Should().Be("Tim Clem");
            user.Location.Should().Be("San Francisco, CA");
            user.Id.Should().Be(136521);
            user.Email.Should().Be("timothy.clem@gmail.com");
            user.Login.Should().Be("tclem");
        }

        [Fact]
        public async Task CanGetAuthenticatedUser()
        {
            var client = new GitHubClient { Login = "xapitestaccountx", Password = "octocat" };

            var user = await client.Users.GetAsync();

            user.Login.Should().Be("xapitestaccountx");
        }

        [Fact]
        public async Task CanUpdateAuthenticatedUser()
        {
            var client = new GitHubClient { Login = "xapitestaccountx", Password = "octocat" };

            var user = await client.Users.UpdateAsync(new User { Name = "Mr. Manager" });

            user.Login.Should().Be("xapitestaccountx");
        }
    }
}
