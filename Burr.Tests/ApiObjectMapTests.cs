using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burr.SimpleJSON;
using Xunit;
using FluentAssertions;

namespace Burr.Tests
{
    public class ApiObjectMapTests
    {
        public class TheForMethod
        {
            [Fact]
            public void ThrowsIfMapDoesntExists()
            {
                Assert.Throws<KeyNotFoundException>(() => new ApiObjectMap().For<string>(JObject.CreateString("hi")));
            }

            [Fact]
            public void ThrowsIfParamsAreNull()
            {
                Assert.Throws<ArgumentNullException>(() => new ApiObjectMap().For<User>((JObject)null));
                Assert.Throws<ArgumentNullException>(() => new ApiObjectMap().For((User)null));
            }

            [Fact]
            public void ProperlyMapsToAUser()
            {
                var map = new ApiObjectMap();

                var user = map.For<User>(JSONDecoder.Decode(Fixtures.UserJson.GetResourceAsString()));

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
            public void ProperlyMapsFromAUser()
            {
                var map = new ApiObjectMap();

                var jObj = map.For(
                    new User
                    {
                        Name = "Tim Clem",
                        Email = "timothy.clem@gmail.com",
                        Blog = "http://timclem.wordpress.com",
                        Company = "GitHub",
                        Location = "San Francisco, CA",
                        Hireable = false,
                        Bio = "once upon a time..."
                    });

                ((string)jObj["Name"]).Should().Be("Tim Clem");
                ((string)jObj["Email"]).Should().Be("timothy.clem@gmail.com");
                ((string)jObj["Blog"]).Should().Be("http://timclem.wordpress.com");
                ((string)jObj["Company"]).Should().Be("GitHub");
                ((string)jObj["Location"]).Should().Be("San Francisco, CA");
                ((bool)jObj["Hireable"]).Should().Be(false);
                ((string)jObj["Bio"]).Should().Be("once upon a time...");
            }
        }
    }
}
