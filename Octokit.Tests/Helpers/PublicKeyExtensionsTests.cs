using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Models
{
    public class PublicKeyExtensionsTests
    {
        public class TheGetKeyDataAndNameMethod
        {
            [Theory]
            [InlineData("ssh-rsa AAAAB3NzaC1yc2EAAAABIwAA timothy.clem@gmail.com", "AAAAB3NzaC1yc2EAAAABIwAA", "timothy.clem@gmail.com")]
            [InlineData("ssh-rsa AAAAB3NzaC1yc2EAAAABIwAA", "AAAAB3NzaC1yc2EAAAABIwAA", "")]
            [InlineData("ssh-dss AAAAB3NzaC1yc2EAAAABIwAA", "AAAAB3NzaC1yc2EAAAABIwAA", "")]
            [InlineData("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA", "AAAAB3NzaC1yc2EAAAABIwAA", "")]
            public void CanParseKeyData(string raw, string data, string name)
            {
                var key = new PublicKey(raw);

                PublicKeyInfo keyInfo = key.GetKeyDataAndName();
                Assert.Equal(data, keyInfo.Data);
                Assert.Equal(name, keyInfo.Name);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("apsdfoihat")]
            public void ParsingBadDataReturnsNull(string key)
            {
                Assert.Null(new PublicKey(key).GetKeyDataAndName());
            }
        }

        public class TheHasSameDataAsMethod
        {
            [Fact]
            public void ReturnsTrueWhenTwoKeysHaveTheSameData()
            {
                var key = new PublicKey("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA");
                var anotherKey = new PublicKey("ssh-rsa AAAAB3NzaC1yc2EAAAABIwAA");

                Assert.True(key.HasSameDataAs(anotherKey));
            }

            [Fact]
            public void ReturnsFalseWhenCompareKeyIsNull()
            {
                var key = new PublicKey("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA");

                Assert.False(key.HasSameDataAs(null));
            }

            [Theory]
            [InlineData(null, "ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA")]
            [InlineData("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA", null)]
            [InlineData("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA", "ssh-dsa AAAAB3NzaC1yc2EAAAABIwAB")]
            public void ReturnsFalseWhenTwoKeysHaveDifferentData(string firstKey, string secondKey)
            {
                var key = new PublicKey(firstKey);
                var anotherKey = new PublicKey(secondKey);

                Assert.False(key.HasSameDataAs(anotherKey));
            }
        }
    }
}
