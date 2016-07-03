using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableReferencesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableReferencesClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReferencesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", "heads/develop"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, "heads/develop"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Get(1, null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", "heads/develop"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", "heads/develop"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.Get(1, ""));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.Get("owner", "repo", "heads/develop");

                gitHubClient.Received().Git.Reference.Get("owner", "repo", "heads/develop");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.Get(1, "heads/develop");

                gitHubClient.Received().Git.Reference.Get(1, "heads/develop");
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReferencesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", ""));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.GetAll("owner", "repo");

                gitHubClient.Received().Git.Reference.GetAll("owner", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.GetAll(1);

                gitHubClient.Received().Git.Reference.GetAll(1);
            }
        }

        public class TheGetAllForSubNamespaceMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReferencesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForSubNamespace(null, "name", "heads"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForSubNamespace("owner", null, "heads"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForSubNamespace("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForSubNamespace(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForSubNamespace("", "name", "heads"));
                Assert.Throws<ArgumentException>(() => client.GetAllForSubNamespace("owner", "", "heads"));
                Assert.Throws<ArgumentException>(() => client.GetAllForSubNamespace("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllForSubNamespace(1, ""));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.GetAllForSubNamespace("owner", "repo", "heads");

                gitHubClient.Received().Git.Reference.GetAllForSubNamespace("owner", "repo", "heads");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.GetAllForSubNamespace(1, "heads");

                gitHubClient.Received().Git.Reference.GetAllForSubNamespace(1, "heads");
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReferencesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewReference("heads/develop", "sha")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewReference("heads/develop", "sha")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewReference("heads/develop", "sha")));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewReference("heads/develop", "sha")));
            }

            [Fact]
            public void PostsToCorrectUrl()
            {
                var newReference = new NewReference("heads/develop", "sha");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.Create("owner", "repo", newReference);

                gitHubClient.Received().Git.Reference.Create("owner", "repo", newReference);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var newReference = new NewReference("heads/develop", "sha");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.Create(1, newReference);

                gitHubClient.Received().Git.Reference.Create(1, newReference);
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReferencesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", "heads/develop", new ReferenceUpdate("sha")));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, "heads/develop", new ReferenceUpdate("sha")));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", null, new ReferenceUpdate("sha")));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", "heads/develop", null));

                Assert.Throws<ArgumentNullException>(() => client.Update(1, null, new ReferenceUpdate("sha")));
                Assert.Throws<ArgumentNullException>(() => client.Update(1, "heads/develop", null));

                Assert.Throws<ArgumentException>(() => client.Update("", "name", "heads/develop", new ReferenceUpdate("sha")));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "", "heads/develop", new ReferenceUpdate("sha")));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "name", "", new ReferenceUpdate("sha")));

                Assert.Throws<ArgumentException>(() => client.Update(1, "", new ReferenceUpdate("sha")));
            }

            [Fact]
            public void PostsToCorrectUrl()
            {
                var referenceUpdate = new ReferenceUpdate("sha");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.Update("owner", "repo", "heads/develop", referenceUpdate);

                gitHubClient.Received().Git.Reference.Update("owner", "repo", "heads/develop", referenceUpdate);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var referenceUpdate = new ReferenceUpdate("sha");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.Update(1, "heads/develop", referenceUpdate);

                gitHubClient.Received().Git.Reference.Update(1, "heads/develop", referenceUpdate);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReferencesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", "heads/develop"));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, "heads/develop"));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Delete(1, null));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", "heads/develop"));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", "heads/develop"));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.Delete(1, ""));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.Delete("owner", "repo", "heads/develop");

                gitHubClient.Received().Git.Reference.Delete("owner", "repo", "heads/develop");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReferencesClient(gitHubClient);

                client.Delete(1, "heads/develop");

                gitHubClient.Received().Git.Reference.Delete(1, "heads/develop");
            }
        }
    }
}