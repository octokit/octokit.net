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

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.GetAll("fake", PackageType.RubyGems);

                gitHubClient.Connection.Received(1).Get<List<Package>>(
                    new Uri("/orgs/fake/packages", UriKind.Relative),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type")));
            }

            [Fact]
            public void RequestsCorrectUrlWithOptionalParameter()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.GetAll("fake", PackageType.RubyGems, PackageVisibility.Public);

                gitHubClient.Connection.Received().Get<List<Package>>(
                    Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type") && d.ContainsKey("visibility")));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, PackageType.Nuget));
                Assert.Throws<ArgumentException>(() => client.GetAll("", PackageType.Nuget));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.Get("fake", PackageType.Npm, "name");

                gitHubClient.Packages.Received().GetForOrg("fake", PackageType.Npm, "name");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, PackageType.Npm, "asd"));
                Assert.Throws<ArgumentException>(() => client.Get("", PackageType.Npm, "asd"));

                Assert.Throws<ArgumentNullException>(() => client.Get("owner", PackageType.Npm, null));
                Assert.Throws<ArgumentException>(() => client.Get("owner", PackageType.Npm, ""));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.Delete("fake", PackageType.Npm, "name");

                gitHubClient.Packages.Received(1).DeleteForOrg("fake", PackageType.Npm, "name");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, PackageType.Npm, "asd"));
                Assert.Throws<ArgumentException>(() => client.Delete("", PackageType.Npm, "asd"));

                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", PackageType.Npm, null));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", PackageType.Npm, ""));
            }
        }

        public class TheRestoreMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackagesClient(gitHubClient);

                client.Restore("fake", PackageType.Npm, "name");

                gitHubClient.Packages.Received(1).RestoreForOrg("fake", PackageType.Npm, "name");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackagesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Restore(null, PackageType.Npm, "asd"));
                Assert.Throws<ArgumentException>(() => client.Restore("", PackageType.Npm, "asd"));

                Assert.Throws<ArgumentNullException>(() => client.Restore("owner", PackageType.Npm, null));
                Assert.Throws<ArgumentException>(() => client.Restore("owner", PackageType.Npm, ""));
            }
        }
    }
}
