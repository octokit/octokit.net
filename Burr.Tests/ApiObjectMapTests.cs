using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
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
                Assert.Throws<ArgumentNullException>(() => new ApiObjectMap().For<User>(null));
            }

            [Fact]
            public void ProperlyMapsAUser()
            {
                var map = new ApiObjectMap();

                var user = map.For<User>(JSONDecoder.Decode(Fixtures.UserJson.GetResourceAsString()));

                user.Should().NotBeNull();
                user.AvatarUrl.Should().Be("https://secure.gravatar.com/avatar/2f4861b27dc35663ed271d39f5358261?d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-140.png");
            }

        }
    }
}
