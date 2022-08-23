using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservablePackageVersionsTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservablePackageVersionsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackageVersionsClient(gitHubClient);

                client.GetAll("fake", PackageType.RubyGems, "name");

                gitHubClient.Connection.Received().Get<List<PackageVersion>>(
                    new Uri("/orgs/fake/packages/rubygems/name/versions", UriKind.Relative),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")));
            }

            [Fact]
            public void RequestsCorrectUrlWithOptionalParameter()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackageVersionsClient(gitHubClient);

                client.GetAll("fake", PackageType.RubyGems, "name", PackageVersionState.Deleted);

                gitHubClient.Connection.Received().Get<List<PackageVersion>>(
                    Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state") && d["state"] == "deleted"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackageVersionsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, PackageType.Nuget, "name"));
                Assert.Throws<ArgumentException>(() => client.GetAll("", PackageType.Nuget, "name"));

                Assert.Throws<ArgumentNullException>(() => client.GetAll("fake", PackageType.Nuget, null));
                Assert.Throws<ArgumentException>(() => client.GetAll("fake", PackageType.Nuget, ""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackageVersionsClient(gitHubClient);

                client.Get("fake", PackageType.Npm, "name", 5);

                gitHubClient.Packages.PackageVersions.Received().GetForOrg("fake", PackageType.Npm, "name", 5);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackageVersionsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, PackageType.Npm, "asd", 5));
                Assert.Throws<ArgumentException>(() => client.Get("", PackageType.Npm, "asd", 5));

                Assert.Throws<ArgumentNullException>(() => client.Get("owner", PackageType.Npm, null, 5));
                Assert.Throws<ArgumentException>(() => client.Get("owner", PackageType.Npm, "", 5));

                Assert.Throws<ArgumentException>(() => client.Get("owner", PackageType.Npm, "asd", 0));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackageVersionsClient(gitHubClient);

                client.Delete("fake", PackageType.Npm, "name", 5);

                gitHubClient.Packages.PackageVersions.Received(1).DeleteForOrg("fake", PackageType.Npm, "name", 5);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackageVersionsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, PackageType.Npm, "asd", 5));
                Assert.Throws<ArgumentException>(() => client.Delete("", PackageType.Npm, "asd", 5));

                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", PackageType.Npm, null, 5));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", PackageType.Npm, "", 5));

                Assert.Throws<ArgumentException>(() => client.Delete("owner", PackageType.Npm, "asd", 0));
            }
        }

        public class TheRestoreMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePackageVersionsClient(gitHubClient);

                client.Restore("fake", PackageType.Npm, "name", 5);

                gitHubClient.Packages.PackageVersions.Received(1).RestoreForOrg("fake", PackageType.Npm, "name", 5);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePackageVersionsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Restore(null, PackageType.Npm, "asd", 5));
                Assert.Throws<ArgumentException>(() => client.Restore("", PackageType.Npm, "asd", 5));

                Assert.Throws<ArgumentNullException>(() => client.Restore("owner", PackageType.Npm, null, 5));
                Assert.Throws<ArgumentException>(() => client.Restore("owner", PackageType.Npm, "", 5));
                
                Assert.Throws<ArgumentException>(() => client.Restore("owner", PackageType.Npm, "asd", 0));
            }
        }
    }

}
