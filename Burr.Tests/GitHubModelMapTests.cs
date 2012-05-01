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
        }

        public class TheForAuthorizationMethod
        {
            [Fact]
            public void ProperlyMapsToAuthorizations()
            {
                var map = new GitHubModelMap();

                var auth = map.For<IEnumerable<Authorization>>(JsonDecoder.Decode(Fixtures.AuthorizationsJson.GetResourceAsString()));

                auth.Should().NotBeNull();
                auth.Count().Should().Be(2);
                auth.First().Note.Should().BeNull();
                auth.First().Token.Should().Be("abxya");
                auth.First().NoteUrl.Should().BeNull();
                auth.First().Scopes.Should().BeEquivalentTo(new[] { "user", "public_repo" });
                auth.First().CreatedAt.Should().Be(DateTimeOffset.Parse("2011-03-10T20:24:18Z"));
                auth.First().UpdateAt.Should().Be(DateTimeOffset.Parse("2011-03-10T20:24:19Z"));
                auth.First().Id.Should().Be(22457);
                auth.First().Application.Name.Should().Be("LinkedIn");
                auth.First().Application.Url.Should().Be("http://github.linkedin.com");
                auth.Last().Note.Should().Be("blah");
                auth.Last().Token.Should().Be("1234asdbas");
                auth.Last().NoteUrl.Should().Be("http://example.com");
                auth.Last().Scopes.Should().BeNull();
                auth.Last().CreatedAt.Should().Be(DateTimeOffset.Parse("2011-03-30T20:48:51Z"));
                auth.Last().UpdateAt.Should().Be(DateTimeOffset.Parse("2011-03-30T21:04:24Z"));
                auth.Last().Id.Should().Be(30410);
                auth.Last().Application.Name.Should().Be("Careers 2.0 by Stack Overflow");
                auth.Last().Application.Url.Should().Be("http://careers.stackoverflow.com/");
            }

            [Fact]
            public void ProperlyMapsToAuthorization()
            {
                var map = new GitHubModelMap();

                var auth = map.For<Authorization>(JsonDecoder.Decode(Fixtures.AuthorizationJson.GetResourceAsString()));

                auth.Should().NotBeNull();
                auth.Note.Should().BeNull();
                auth.Token.Should().Be("abxya");
                auth.NoteUrl.Should().BeNull();
                auth.Scopes.Should().BeEquivalentTo(new[] { "user", "public_repo" });
                auth.CreatedAt.Should().Be(DateTimeOffset.Parse("2011-03-10T20:24:18Z"));
                auth.UpdateAt.Should().Be(DateTimeOffset.Parse("2011-03-10T20:24:19Z"));
                auth.Id.Should().Be(22457);
                auth.Application.Name.Should().Be("LinkedIn");
                auth.Application.Url.Should().Be("http://github.linkedin.com");
            }

            [Fact]
            public void ProperlyMapsFromAnAuthorization()
            {
                var map = new GitHubModelMap();

                var jObj = map.For(
                    new AuthorizationUpdate
                    {
                        Note = "hi",
                        NoteUrl = "http://example.com",
                        Scopes = new []{"user", "repo"}
                    });

                ((string)jObj["note"]).Should().Be("hi");
                ((string)jObj["note_url"]).Should().Be("http://example.com");
                (jObj["scopes"]).ArrayValue.Select(x => x.StringValue).Should().BeEquivalentTo(new[] { "user", "repo" });
            }

            [Fact]
            public void CanHandleNullScopes()
            {
                var map = new GitHubModelMap();

                var jObj = map.For(
                    new AuthorizationUpdate
                    {
                        Note = "hi",
                        NoteUrl = "http://example.com"
                    });

                jObj.Count.Should().Be(2);
                ((string)jObj["note"]).Should().Be("hi");
                ((string)jObj["note_url"]).Should().Be("http://example.com");
            }


            [Fact]
            public void CanClearScopes()
            {
                var map = new GitHubModelMap();

                var jObj = map.For(
                    new AuthorizationUpdate
                    {
                        Note = "hi",
                        NoteUrl = "http://example.com",
                        Scopes = new string [0]
                    });

                ((string)jObj["note"]).Should().Be("hi");
                ((string)jObj["note_url"]).Should().Be("http://example.com");
                (jObj["scopes"]).ArrayValue.Select(x => x.StringValue).Should().BeEmpty();
            }

            [Fact]
            public void LeavesOffFieldsThatArentSet()
            {
                var map = new GitHubModelMap();

                var jObj = map.For(
                    new AuthorizationUpdate
                    {
                        Note = "hi"
                    });

                ((string)jObj["note"]).Should().Be("hi");

                jObj.Count.Should().Be(1);
            }

            [Fact]
            public void ClearsFieldsThatAreEmptyString()
            {
                var map = new GitHubModelMap();

                var jObj = map.For(
                    new AuthorizationUpdate
                    {
                        Note = ""
                    });

                ((string)jObj["note"]).Should().Be("");

                jObj.Count.Should().Be(1);
            }
        }

        public class TheForUserMethod
        {
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
