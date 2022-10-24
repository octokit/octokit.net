using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Client for accessing contents of files within a repository as base64 encoded content.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/contents/">Repository Contents API documentation</a> for more information.
    /// </remarks>
    public class RepositoryContentsClient : ApiClient, IRepositoryContentsClient
    {
        /// <summary>
        /// Create an instance of the RepositoryContentsClient
        /// </summary>
        /// <param name="apiConnection">The underlying connection to use</param>
        public RepositoryContentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/contents/{path}")]
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            var url = ApiUrls.RepositoryContent(owner, name, path);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the raw content of the file at the given <paramref name="path"/> or <c>null</c> if the path is a directory.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        [ManualRoute("GET", "repos/{owner}/{repo}/contents/{path}")]
        public Task<byte[]> GetRawContent(string owner, string name, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            var url = ApiUrls.RepositoryContent(owner, name, path);

            return ApiConnection.GetRaw(url, null);
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The content path</param>
        [ManualRoute("GET", "/repoitories/{id}/contents/{path}")]
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(long repositoryId, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            var url = ApiUrls.RepositoryContent(repositoryId, path);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/contents/{path}")]
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var url = ApiUrls.RepositoryContent(owner, name, string.Empty);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/contents/{path}")]
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(long repositoryId)
        {
            var url = ApiUrls.RepositoryContent(repositoryId, string.Empty);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository�s default branch (usually main)</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/contents/{path}?ref={ref}")]
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            var url = ApiUrls.RepositoryContent(owner, name, path, reference);
            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the raw content of the file at the given <paramref name="path"/> or <c>null</c> if the path is a directory.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        /// <param name="reference">The name of the commit/branch/tag.</param>
        [ManualRoute("GET", "repos/{owner}/{repo}/contents/{path}?ref={ref}")]
        public Task<byte[]> GetRawContentByRef(string owner, string name, string path, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            var url = ApiUrls.RepositoryContent(owner, name, path, reference);
            return ApiConnection.GetRaw(url, null);
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The content path</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually main)</param>
        [ManualRoute("GET", "/repositories/{id}/contents/{path}?ref={ref}")]
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(long repositoryId, string path, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            var url = ApiUrls.RepositoryContent(repositoryId, path, reference);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository�s default branch (usually main)</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/contents/{path}?ref={ref}")]
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            var url = ApiUrls.RepositoryContent(owner, name, string.Empty, reference);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually main)</param>
        [ManualRoute("GET", "/repositories/{id}/contents/{path}?ref={ref}")]
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            var url = ApiUrls.RepositoryContent(repositoryId, string.Empty, reference);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Gets the preferred README for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/readme")]
        public async Task<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.RepositoryReadme(owner, name);
            var readmeInfo = await ApiConnection.Get<ReadmeResponse>(endpoint, null).ConfigureAwait(false);

            return new Readme(readmeInfo, ApiConnection);
        }

        /// <summary>
        /// Gets the preferred README for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repositories/{id}/readme")]
        public async Task<Readme> GetReadme(long repositoryId)
        {
            var endpoint = ApiUrls.RepositoryReadme(repositoryId);
            var readmeInfo = await ApiConnection.Get<ReadmeResponse>(endpoint, null).ConfigureAwait(false);

            return new Readme(readmeInfo, ApiConnection);
        }

        /// <summary>
        /// Gets the preferred README's HTML for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [DotNetSpecificRouteAttribute]
        public Task<string> GetReadmeHtml(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.GetHtml(ApiUrls.RepositoryReadme(owner, name), null);
        }

        /// <summary>
        /// Gets the preferred README's HTML for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [DotNetSpecificRouteAttribute]
        public Task<string> GetReadmeHtml(long repositoryId)
        {
            return ApiConnection.GetHtml(ApiUrls.RepositoryReadme(repositoryId), null);
        }

        /// <summary>
        /// Get an archive of a given repository's contents
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/{archive_format}/{ref}")]
        public Task<byte[]> GetArchive(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetArchive(owner, name, ArchiveFormat.Tarball, string.Empty);
        }

        /// <summary>
        /// Get an archive of a given repository's contents
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/{archive_format}/{ref}")]
        public Task<byte[]> GetArchive(long repositoryId)
        {
            return GetArchive(repositoryId, ArchiveFormat.Tarball, string.Empty);
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/{archive_format}/{ref}")]
        public Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetArchive(owner, name, archiveFormat, string.Empty);
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        [ManualRoute("GET", "/repositories/{id}/{archive_format}/{ref}")]
        public Task<byte[]> GetArchive(long repositoryId, ArchiveFormat archiveFormat)
        {
            return GetArchive(repositoryId, archiveFormat, string.Empty);
        }

        /// <summary>
        /// Get an archive of a given repository's contents, using a specific format and reference
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/{archive_format}/{ref}")]
        public Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reference, nameof(reference));

            return GetArchive(owner, name, archiveFormat, reference, TimeSpan.FromMinutes(60));
        }

        /// <summary>
        /// Get an archive of a given repository's contents, using a specific format and reference
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        [ManualRoute("GET", "/repositories/{id}/{archive_format}/{ref}")]
        public Task<byte[]> GetArchive(long repositoryId, ArchiveFormat archiveFormat, string reference)
        {
            Ensure.ArgumentNotNull(reference, nameof(reference));

            return GetArchive(repositoryId, archiveFormat, reference, TimeSpan.FromMinutes(60));
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <param name="timeout"> Time span until timeout </param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/{archive_format}/{ref}")]
        public async Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat, string reference, TimeSpan timeout)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reference, nameof(reference));
            Ensure.GreaterThanZero(timeout, nameof(timeout));

            var endpoint = ApiUrls.RepositoryArchiveLink(owner, name, archiveFormat, reference);

            var response = await Connection.Get<byte[]>(endpoint, timeout).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <param name="timeout"> Time span until timeout </param>
        [ManualRoute("GET", "/repositories/{id}/{archive_format}/{ref}")]
        public async Task<byte[]> GetArchive(long repositoryId, ArchiveFormat archiveFormat, string reference, TimeSpan timeout)
        {
            Ensure.ArgumentNotNull(reference, nameof(reference));
            Ensure.GreaterThanZero(timeout, nameof(timeout));

            var endpoint = ApiUrls.RepositoryArchiveLink(repositoryId, archiveFormat, reference);

            var response = await Connection.Get<byte[]>(endpoint, timeout).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/contents/{path}")]
        public Task<RepositoryContentChangeSet> CreateFile(string owner, string name, string path, CreateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNull(request, nameof(request));

            var createUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(createUrl, request);
        }

        /// <summary>
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        [ManualRoute("PUT", "/repositories/{id}/contents/{path}")]
        public Task<RepositoryContentChangeSet> CreateFile(long repositoryId, string path, CreateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNull(request, nameof(request));

            var createUrl = ApiUrls.RepositoryContent(repositoryId, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(createUrl, request);
        }

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/contents/{path}")]
        public Task<RepositoryContentChangeSet> UpdateFile(string owner, string name, string path, UpdateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNull(request, nameof(request));

            var updateUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(updateUrl, request);
        }

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        [ManualRoute("PUT", "/repositories/{id}/contents/{path}")]
        public Task<RepositoryContentChangeSet> UpdateFile(long repositoryId, string path, UpdateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNull(request, nameof(request));

            var updateUrl = ApiUrls.RepositoryContent(repositoryId, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(updateUrl, request);
        }

        /// <summary>
        /// Creates a commit that deletes a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to delete</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/contents/{path}")]
        public Task DeleteFile(string owner, string name, string path, DeleteFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNull(request, nameof(request));

            var deleteUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Delete(deleteUrl, request);
        }

        /// <summary>
        /// Creates a commit that deletes a file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to delete</param>
        [ManualRoute("DELETE", "/repositorioes/{id}/contents/{path}")]
        public Task DeleteFile(long repositoryId, string path, DeleteFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));
            Ensure.ArgumentNotNull(request, nameof(request));

            var deleteUrl = ApiUrls.RepositoryContent(repositoryId, path);
            return ApiConnection.Delete(deleteUrl, request);
        }
    }
}
