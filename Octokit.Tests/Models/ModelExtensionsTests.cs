using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Models
{
    public class ModelExtensionsTests
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
                var key = new SshKey(raw);

                SshKeyInfo keyInfo = key.GetKeyDataAndName();
                Assert.Equal(data, keyInfo.Data);
                Assert.Equal(name, keyInfo.Name);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("apsdfoihat")]
            public void ParsingBadDataReturnsNull(string key)
            {
                Assert.Null(new SshKey(key).GetKeyDataAndName());
            }
        }

        public class TheHasSameDataAsMethod
        {
            [Fact]
            public void ReturnsTrueWhenTwoKeysHaveTheSameData()
            {
                var key = new SshKey("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA", "somekey");
                var anotherKey = new SshKey("ssh-rsa AAAAB3NzaC1yc2EAAAABIwAA", "whatever");

                Assert.True(key.HasSameDataAs(anotherKey));
            }

            [Fact]
            public void ReturnsFalseWhenCompareKeyIsNull()
            {
                var key = new SshKey("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA", "somekey");

                Assert.False(key.HasSameDataAs(null));
            }

            [Theory]
            [InlineData(null, "ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA")]
            [InlineData("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA", null)]
            [InlineData("ssh-dsa AAAAB3NzaC1yc2EAAAABIwAA", "ssh-dsa AAAAB3NzaC1yc2EAAAABIwAB")]
            public void ReturnsFalseWhenTwoKeysHaveDifferentData(string firstKey, string secondKey)
            {
                var key = new SshKey(firstKey, "somekey");
                var anotherKey = new SshKey(secondKey, "whatever");

                Assert.False(key.HasSameDataAs(anotherKey));
            }
        }
    }
}
