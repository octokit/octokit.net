using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burr.SimpleJson;
using Xunit;
using FluentAssertions;
using System.Diagnostics;

namespace Burr.Tests
{
    public class GitHubModelMapTests
    {
        public class TheForMethod
        {
            [Fact]
            public void ThrowsIfMapDoesntExists()
            {
                Assert.Throws<KeyNotFoundException>(() => new GitHubModelMap().For<string>(JObject.CreateString("hi")));
            }

            [Fact]
            public void ThrowsIfParamsAreNull()
            {
                Assert.Throws<ArgumentNullException>(() => new GitHubModelMap().For<User>((JObject)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubModelMap().For((User)null));
            }

            [Fact]
            public void ProperlyMapsToAUser()
            {
                var map = new GitHubModelMap();

                var user = map.For<User>(JsonDecoder.Decode(Fixtures.UserJson.GetResourceAsString()));

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
            public void ProperlyMapsToAuthenticatedUser()
            {
                var map = new GitHubModelMap();

                var user = map.For<User>(JsonDecoder.Decode(Fixtures.UserFullJson.GetResourceAsString()));

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
                user.Plan.Should().NotBeNull();
                user.Plan.Collaborators.Should().Be(25);
                user.Plan.Space.Should().Be(6291456);
                user.Plan.Name.Should().Be("large");
                user.Plan.PrivateRepos.Should().Be(50);
            }

            [Fact]
            public void ProperlyMapsFromAUser()
            {
                var map = new GitHubModelMap();

                var jObj = map.For(
                    new UserUpdate
                    {
                        Name = "Tim Clem",
                        Email = "timothy.clem@gmail.com",
                        Blog = "http://timclem.wordpress.com",
                        Company = "GitHub",
                        Location = "San Francisco, CA",
                        Hireable = false,
                        Bio = "once upon a time..."
                    });

                ((string)jObj["name"]).Should().Be("Tim Clem");
                ((string)jObj["email"]).Should().Be("timothy.clem@gmail.com");
                ((string)jObj["blog"]).Should().Be("http://timclem.wordpress.com");
                ((string)jObj["company"]).Should().Be("GitHub");
                ((string)jObj["location"]).Should().Be("San Francisco, CA");
                ((bool)jObj["hireable"]).Should().Be(false);
                ((string)jObj["bio"]).Should().Be("once upon a time...");
            }

            [Fact]
            public void LeavesOffFieldsThatArentSet()
            {
                var map = new GitHubModelMap();

                var jObj = map.For(
                    new UserUpdate
                    {
                        Name = "Tim Clem",
                    });

                ((string)jObj["name"]).Should().Be("Tim Clem");

                jObj.Count.Should().Be(1);
            }

            [Fact]
            public void ClearsFieldsThatAreEmptyString()
            {
                var map = new GitHubModelMap();

                var jObj = map.For(
                    new UserUpdate
                    {
                        Name = "",
                    });

                ((string)jObj["name"]).Should().Be("");

                jObj.Count.Should().Be(1);
            }
        }
    }
}
