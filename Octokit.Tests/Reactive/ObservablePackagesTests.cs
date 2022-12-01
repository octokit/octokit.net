using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservablePackagesTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservablePackagesClient(null));
            }
        }

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.GetAllForOrg("fake", PackageType.RubyGems);

                gitHubClient.Connection.Received(1).Get<List<Package>>(
                    new Uri("orgs/fake/packages", UriKind.Relative),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type")));
            }

            [Fact]
            public void RequestsCorrectUrlWithOptionalParameter()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.GetAllForOrg("fake", PackageType.RubyGems, PackageVisibility.Public);

                gitHubClient.Connection.Received().Get<List<Package>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type") && d.ContainsKey("visibility")));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForOrg(null, PackageType.Nuget));
                Assert.Throws<ArgumentException>(() => client.GetAllForOrg("", PackageType.Nuget));
            }
        }

        public class TheGetForOrgMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.GetForOrg("fake", PackageType.Npm, "name");

                gitHubClient.Packages.Received().GetForOrg("fake", PackageType.Npm, "name");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetForOrg(null, PackageType.Npm, "asd"));
                Assert.Throws<ArgumentException>(() => client.GetForOrg("", PackageType.Npm, "asd"));

                Assert.Throws<ArgumentNullException>(() => client.GetForOrg("owner", PackageType.Npm, null));
                Assert.Throws<ArgumentException>(() => client.GetForOrg("owner", PackageType.Npm, ""));
            }
        }

        public class TheDeleteForOrgMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.DeleteForOrg("fake", PackageType.Npm, "name");

                gitHubClient.Packages.Received(1).DeleteForOrg("fake", PackageType.Npm, "name");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteForOrg(null, PackageType.Npm, "asd"));
                Assert.Throws<ArgumentException>(() => client.DeleteForOrg("", PackageType.Npm, "asd"));

                Assert.Throws<ArgumentNullException>(() => client.DeleteForOrg("owner", PackageType.Npm, null));
                Assert.Throws<ArgumentException>(() => client.DeleteForOrg("owner", PackageType.Npm, ""));
            }
        }

        public class TheRestoreForOrgMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.RestoreForOrg("fake", PackageType.Npm, "name");

                gitHubClient.Packages.Received(1).RestoreForOrg("fake", PackageType.Npm, "name");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.RestoreForOrg(null, PackageType.Npm, "asd"));
                Assert.Throws<ArgumentException>(() => client.RestoreForOrg("", PackageType.Npm, "asd"));

                Assert.Throws<ArgumentNullException>(() => client.RestoreForOrg("owner", PackageType.Npm, null));
                Assert.Throws<ArgumentException>(() => client.RestoreForOrg("owner", PackageType.Npm, ""));
            }

            public class TheGetAllForActiveUserMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.GetAllForActiveUser(PackageType.RubyGems);

                    gitHubClient.Connection.Received(1).Get<List<Package>>(
                        new Uri("user/packages", UriKind.Relative),
                        Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type")));
                }

                [Fact]
                public void RequestsCorrectUrlWithOptionalParameter()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.GetAllForActiveUser(PackageType.RubyGems, PackageVisibility.Public);

                    gitHubClient.Connection.Received().Get<List<Package>>(
                        Arg.Is<Uri>(u => u.ToString() == "user/packages"),
                        Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type") && d.ContainsKey("visibility")));
                }
            }

            public class TheGetForActiveUserMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.GetForActiveUser(PackageType.Npm, "name");

                    gitHubClient.Packages.Received().GetForActiveUser(PackageType.Npm, "name");
                }

                [Fact]
                public void EnsuresNonNullArguments()
                {
                    var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                    Assert.Throws<ArgumentNullException>(() => client.GetForActiveUser(PackageType.Npm, null));
                    Assert.Throws<ArgumentException>(() => client.GetForActiveUser(PackageType.Npm, ""));
                }
            }

            public class TheDeleteForActiveUserMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.DeleteForActiveUser(PackageType.Npm, "name");

                    gitHubClient.Packages.Received(1).DeleteForActiveUser(PackageType.Npm, "name");
                }

                [Fact]
                public void EnsuresNonNullArguments()
                {
                    var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                    Assert.Throws<ArgumentNullException>(() => client.DeleteForActiveUser(PackageType.Npm, null));
                    Assert.Throws<ArgumentException>(() => client.DeleteForActiveUser(PackageType.Npm, ""));
                }
            }

            public class TheRestoreForActiveUserMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.RestoreForActiveUser(PackageType.Npm, "name");

                    gitHubClient.Packages.Received(1).RestoreForActiveUser(PackageType.Npm, "name");
                }

                [Fact]
                public void EnsuresNonNullArguments()
                {
                    var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                    Assert.Throws<ArgumentNullException>(() => client.RestoreForActiveUser(PackageType.Npm, null));
                    Assert.Throws<ArgumentException>(() => client.RestoreForActiveUser(PackageType.Npm, ""));
                }
            }

            public class TheGetAllForUserMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.GetAllForUser("fake", PackageType.RubyGems);

                    gitHubClient.Connection.Received(1).Get<List<Package>>(
                        new Uri("users/fake/packages", UriKind.Relative),
                        Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type")));
                }

                [Fact]
                public void RequestsCorrectUrlWithOptionalParameter()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.GetAllForUser("fake", PackageType.RubyGems, PackageVisibility.Public);

                    gitHubClient.Connection.Received().Get<List<Package>>(
                        Arg.Is<Uri>(u => u.ToString() == "users/fake/packages"),
                        Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type") && d.ContainsKey("visibility")));
                }

                [Fact]
                public void EnsuresNonNullArguments()
                {
                    var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                    Assert.Throws<ArgumentNullException>(() => client.GetAllForUser(null, PackageType.Nuget));
                    Assert.Throws<ArgumentException>(() => client.GetAllForUser("", PackageType.Nuget));
                }
            }

            public class TheGetForUserMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.GetForUser("fake", PackageType.Npm, "name");

                    gitHubClient.Packages.Received().GetForUser("fake", PackageType.Npm, "name");
                }

                [Fact]
                public void EnsuresNonNullArguments()
                {
                    var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                    Assert.Throws<ArgumentNullException>(() => client.GetForUser(null, PackageType.Npm, "asd"));
                    Assert.Throws<ArgumentException>(() => client.GetForUser("", PackageType.Npm, "asd"));

                    Assert.Throws<ArgumentNullException>(() => client.GetForUser("owner", PackageType.Npm, null));
                    Assert.Throws<ArgumentException>(() => client.GetForUser("owner", PackageType.Npm, ""));
                }
            }

            public class TheDeleteForUserMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.DeleteForUser("fake", PackageType.Npm, "name");

                    gitHubClient.Packages.Received(1).DeleteForUser("fake", PackageType.Npm, "name");
                }

                [Fact]
                public void EnsuresNonNullArguments()
                {
                    var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                    Assert.Throws<ArgumentNullException>(() => client.DeleteForUser(null, PackageType.Npm, "asd"));
                    Assert.Throws<ArgumentException>(() => client.DeleteForUser("", PackageType.Npm, "asd"));

                    Assert.Throws<ArgumentNullException>(() => client.DeleteForUser("owner", PackageType.Npm, null));
                    Assert.Throws<ArgumentException>(() => client.DeleteForUser("owner", PackageType.Npm, ""));
                }
            }

            public class TheRestoreForUserMethod
            {
                [Fact]
                public void RequestsCorrectUrl()
                {
                    var gitHubClient = Substitute.For<IGitHubClient>();
                    var client = new ObservablePackagesClient(gitHubClient);

                    client.RestoreForUser("fake", PackageType.Npm, "name");

                    gitHubClient.Packages.Received(1).RestoreForUser("fake", PackageType.Npm, "name");
                }

                [Fact]
                public void EnsuresNonNullArguments()
                {
                    var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                    Assert.Throws<ArgumentNullException>(() => client.RestoreForUser(null, PackageType.Npm, "asd"));
                    Assert.Throws<ArgumentException>(() => client.RestoreForUser("", PackageType.Npm, "asd"));

                    Assert.Throws<ArgumentNullException>(() => client.RestoreForUser("owner", PackageType.Npm, null));
                    Assert.Throws<ArgumentException>(() => client.RestoreForUser("owner", PackageType.Npm, ""));
                }
            }
        }
    }
}
