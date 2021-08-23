using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableEnterpriseLDAPClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableEnterpriseLdapClient(null));
            }
        }

        public class TheUpdateUserMappingMethod
        {
            readonly string _distinguishedName = "uid=test-user,ou=users,dc=company,dc=com";

            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseLdapClient(github);

                client.UpdateUserMapping("test-user", new NewLdapMapping(_distinguishedName));
                github.Enterprise.Ldap.Received(1).UpdateUserMapping(
                    Arg.Is<string>(a => a == "test-user"),
                    Arg.Is<NewLdapMapping>(a =>
                        a.LdapDistinguishedName == _distinguishedName));
            }
        }

        public class TheQueueSyncUserMappingMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseLdapClient(github);

                client.QueueSyncUserMapping("test-user");
                github.Enterprise.Ldap.Received(1).QueueSyncUserMapping(
                    Arg.Is<string>(a => a == "test-user"));
            }
        }

        public class TheUpdateTeamMappingMethod
        {
            readonly string _distinguishedName = "cn=test-team,ou=groups,dc=company,dc=com";

            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseLdapClient(github);

                client.UpdateTeamMapping(1, new NewLdapMapping(_distinguishedName));
                github.Enterprise.Ldap.Received(1).UpdateTeamMapping(
                    Arg.Is<long>(a => a == 1),
                    Arg.Is<NewLdapMapping>(a =>
                        a.LdapDistinguishedName == _distinguishedName));
            }
        }

        public class TheQueueSyncTeamMappingMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseLdapClient(github);

                client.QueueSyncTeamMapping(1);
                github.Enterprise.Ldap.Received(1).QueueSyncTeamMapping(
                    Arg.Is<long>(a => a == 1));
            }
        }
    }
}
