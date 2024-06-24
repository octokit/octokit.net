using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Dependency Submission API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">Dependency Submission API documentation</a> for more details.
    /// </remarks>
    public class DependencySubmissionClient : ApiClient, IDependencySubmissionClient
    {
        /// <summary>
        /// Initializes a new GitHub Dependency Submission API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public DependencySubmissionClient(IApiConnection apiConnection) : base(apiConnection) { }

        /// <summary>
        /// Creates a new dependency snapshot.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="snapshot">The dependency snapshot to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs</exception>
        /// <returns>A <see cref="DependencySnapshotSubmission"/> instance for the created snapshot</returns>
        [ManualRoute("POST", "/repos/{owner}/{repo}/dependency-graph/snapshots")]
        public Task<DependencySnapshotSubmission> Create(string owner, string name, NewDependencySnapshot snapshot)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(snapshot, nameof(snapshot));

            var newDependencySnapshotAsObject = ConvertToJsonObject(snapshot);

            return ApiConnection.Post<DependencySnapshotSubmission>(ApiUrls.DependencySubmission(owner, name), newDependencySnapshotAsObject);
        }

        /// <summary>
        /// Creates a new dependency snapshot.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="snapshot">The dependency snapshot to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs</exception>
        /// <returns>A <see cref="DependencySnapshotSubmission"/> instance for the created snapshot</returns>
        [ManualRoute("POST", "/repositories/{id}/dependency-graph/snapshots")]
        public Task<DependencySnapshotSubmission> Create(long repositoryId, NewDependencySnapshot snapshot)
        {
            Ensure.ArgumentNotNull(snapshot, nameof(snapshot));

            var newDependencySnapshotAsObject = ConvertToJsonObject(snapshot);

            return ApiConnection.Post<DependencySnapshotSubmission>(ApiUrls.DependencySubmission(repositoryId), newDependencySnapshotAsObject);
        }

        /// <summary>
        /// Dependency snapshots dictionaries such as Manifests need to be passed as JsonObject in order to be serialized correctly
        /// </summary>
        private JsonObject ConvertToJsonObject(NewDependencySnapshot snapshot)
        {
            var newSnapshotAsObject = new JsonObject();
            newSnapshotAsObject.Add("version", snapshot.Version);
            newSnapshotAsObject.Add("sha", snapshot.Sha);
            newSnapshotAsObject.Add("ref", snapshot.Ref);
            newSnapshotAsObject.Add("scanned", snapshot.Scanned);
            newSnapshotAsObject.Add("job", snapshot.Job);
            newSnapshotAsObject.Add("detector", snapshot.Detector);

            if (snapshot.Metadata != null)
            {
                var metadataAsObject = new JsonObject();
                foreach (var kvp in snapshot.Metadata)
                {
                    metadataAsObject.Add(kvp.Key, kvp.Value);
                }

                newSnapshotAsObject.Add("metadata", metadataAsObject);
            }

            if (snapshot.Manifests != null)
            {
                var manifestsAsObject = new JsonObject();
                foreach (var manifestKvp in snapshot.Manifests)
                {
                    var manifest = manifestKvp.Value;

                    var manifestAsObject = new JsonObject();
                    manifestAsObject.Add("name", manifest.Name);

                    if (manifest.File.SourceLocation != null)
                    {
                        var manifestFileAsObject = new { SourceLocation = manifest.File.SourceLocation };
                        manifestAsObject.Add("file", manifestFileAsObject);
                    }

                    if (manifest.Metadata != null)
                    {
                        manifestAsObject.Add("metadata", manifest.Metadata);
                    }

                    if (manifest.Resolved != null)
                    {
                        var resolvedAsObject = new JsonObject();
                        foreach (var resolvedKvp in manifest.Resolved)
                        {
                            resolvedAsObject.Add(resolvedKvp.Key, resolvedKvp.Value);
                        }

                        manifestAsObject.Add("resolved", resolvedAsObject);
                    }

                    manifestsAsObject.Add(manifestKvp.Key, manifestAsObject);
                }

                newSnapshotAsObject.Add("manifests", manifestsAsObject);
            }

            return newSnapshotAsObject;
        }
    }
}
