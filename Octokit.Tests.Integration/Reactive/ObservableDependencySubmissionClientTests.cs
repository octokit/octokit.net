using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

public class ObservableDependencySubmissionClientTests
{
    public class TheCreateMethod
    {
        private readonly ObservableGitHubClient _client;
        private readonly RepositoryContext _context;
        private readonly NewDependencySnapshot _newDependencySnapshot;

        public TheCreateMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _client = new ObservableGitHubClient(github);
            _context = github.CreateRepositoryContextWithAutoInit("public-repo").Result;

            var job = new NewDependencySnapshotJob("runid", "example-correlator");
            var detector = new NewDependencySnapshotDetector("example-detector", "1.0.0", "https://github.com/example/detector");

            var resolvedMetadata = new Dictionary<string, object>
            {
                { "License", "MIT" }
            };

            var manifests = new Dictionary<string, NewDependencySnapshotManifest>
            {
                {
                    "package-lock.json",
                    new NewDependencySnapshotManifest("package-lock.json")
                    {
                        File = new NewDependencySnapshotManifestFile
                        {
                            SourceLocation = "src/package-lock.json"
                        },
                        Resolved = new Dictionary<string, NewDependencySnapshotResolvedDependency>
                        {
                            {
                                "@actions/core",
                                new NewDependencySnapshotResolvedDependency
                                {
                                    PackageUrl = "pkg:/npm/%40actions/core@1.1.9",
                                    Dependencies = new Collection<string> { "@actions/http-client" },
                                    Scope = ResolvedPackageKeyScope.Runtime,
                                    Relationship =  ResolvedPackageKeyRelationship.Indirect,
                                    Metadata = resolvedMetadata
                                }
                            },
                            {
                                "@actions/http-client",
                                new NewDependencySnapshotResolvedDependency
                                {
                                    PackageUrl = "pkg:/npm/%40actions/http-client@1.0.7",
                                    Scope = ResolvedPackageKeyScope.Development,
                                    Relationship =  ResolvedPackageKeyRelationship.Direct
                                }
                            },
                            {
                                "tunnel",
                                new NewDependencySnapshotResolvedDependency
                                {
                                    PackageUrl = "pkg:/npm/tunnel@0.0.6",
                                    Dependencies = new Collection<string>(),
                                    Relationship =  ResolvedPackageKeyRelationship.Direct,
                                }
                            }
                        }
                    }
                }
            };

            var snapshotMetadata = new Dictionary<string, object>
            {
                { "Author", "John Doe" },
                { "Version", "1.0.0" },
                { "License", "MIT" }
            };

            _newDependencySnapshot = new NewDependencySnapshot(
                                      1,
                                      "ce587453ced02b1526dfb4cb910479d431683101",
                                      "refs/heads/main",
                                      "2022-06-14T20:25:00Z",
                                      job,
                                      detector)
            {
                Metadata = snapshotMetadata,
                Manifests = manifests
            };
        }

                [IntegrationTest]
        public async Task CanCreateDependencySnapshot()
        {
            var submission = await _client.DependencyGraph.DependencySubmission.Create(_context.RepositoryOwner, _context.RepositoryName, _newDependencySnapshot);

            Assert.Equal(DependencySnapshotSubmissionResult.Accepted, submission.Result);
        }

        [IntegrationTest]
        public async Task CanCreateDependencySnapshotWithRepositoryId()
        {
            var submission = await _client.DependencyGraph.DependencySubmission.Create(_context.Repository.Id, _newDependencySnapshot);

            Assert.Equal(DependencySnapshotSubmissionResult.Accepted, submission.Result);
        }
    }
}
