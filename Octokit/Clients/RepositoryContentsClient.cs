using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Client for accessing contents of files within a repository as base64 encoded content.
    /// </summary>
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
        /// <returns>
        /// A collection of <see cref="RepositoryContent"/> representing the content at the specified path
        /// </returns>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var url = ApiUrls.RepositoryContent(owner, name, path);

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
        /// <returns>
        /// A collection of <see cref="RepositoryContent"/> representing the content at the specified path
        /// </returns>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var url = ApiUrls.RepositoryContent(owner, name, string.Empty);

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
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository�s default branch (usually master)</param>
        /// <returns>
        /// A collection of <see cref="RepositoryContent"/> representing the content at the specified path
        /// </returns>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            var url = ApiUrls.RepositoryContent(owner, name, path, reference);

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
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository�s default branch (usually master)</param>
        /// <returns>
        /// A collection of <see cref="RepositoryContent"/> representing the content at the specified path
        /// </returns>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(name, "reference");

            var url = ApiUrls.RepositoryContent(owner, name, string.Empty, reference);

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
        /// <returns></returns>
        public async Task<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.RepositoryReadme(owner, name);
            var readmeInfo = await ApiConnection.Get<ReadmeResponse>(endpoint, null).ConfigureAwait(false);

            return new Readme(readmeInfo, ApiConnection);
        }

        /// <summary>
        /// Gets the perferred README's HTML for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public Task<string> GetReadmeHtml(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetHtml(ApiUrls.RepositoryReadme(owner, name), null);
        }

        /// <summary>
        /// This method will return a 302 to a URL to download a tarball or zipball archive for a repository.
        /// Please make sure your HTTP framework is configured to follow redirects or you will need to use the 
        /// Location header to make a second GET request.
        /// Note: For private repositories, these links are temporary and expire quickly.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        [Obsolete("Octokit's HTTP library now follows redirects by default - this API will be removed in a future release")]
        public Task<string> GetArchiveLink(string owner, string name)
        {
            return GetArchiveLink(owner, name, ArchiveFormat.Tarball, string.Empty);
        }

        /// <summary>
        /// Get an archive of a given repository's contents
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The binary contents of the archive</returns>
        public Task<byte[]> GetArchive(string owner, string name)
        {
            return GetArchive(owner, name, ArchiveFormat.Tarball, string.Empty);
        }

        /// <summary>
        /// This method will return a 302 to a URL to download a tarball or zipball archive for a repository.
        /// Please make sure your HTTP framework is configured to follow redirects or you will need to use the 
        /// Location header to make a second GET request.
        /// Note: For private repositories, these links are temporary and expire quickly.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <returns></returns>
        [Obsolete("Octokit's HTTP library now follows redirects by default - this API will be removed in a future release")]
        public Task<string> GetArchiveLink(string owner, string name, ArchiveFormat archiveFormat)
        {
            return GetArchiveLink(owner, name, archiveFormat, string.Empty);
        }

        /// <summary>
        /// Get an archive of a given repository's contents, in a specific format
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <returns>The binary contents of the archive</returns>
        public Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat)
        {
            return GetArchive(owner, name, archiveFormat, string.Empty);
        }

        /// <summary>
        /// This method will return a 302 to a URL to download a tarball or zipball archive for a repository.
        /// Please make sure your HTTP framework is configured to follow redirects or you will need to use the 
        /// Location header to make a second GET request.
        /// Note: For private repositories, these links are temporary and expire quickly.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <returns></returns>
        [Obsolete("Octokit's HTTP library now follows redirects by default - this API will be removed in a future release")]
        public Task<string> GetArchiveLink(string owner, string name, ArchiveFormat archiveFormat, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetRedirect(ApiUrls.RepositoryArchiveLink(owner, name, archiveFormat, reference));
        }

        /// <summary>
        /// Get an archive of a given repository's contents, using a specific format and reference
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#get-archive-link</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="archiveFormat">The format of the archive. Can be either tarball or zipball</param>
        /// <param name="reference">A valid Git reference.</param>
        /// <returns>The binary contents of the archive</returns>
        public Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat, string reference)
        {
            return GetArchive(owner, name, archiveFormat, string.Empty, TimeSpan.FromMinutes(60));
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
        /// <returns>The binary contents of the archive</returns>
        public async Task<byte[]> GetArchive(string owner, string name, ArchiveFormat archiveFormat, string reference, TimeSpan timeout)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.GreaterThanZero(timeout, "timeout");

            var endpoint = ApiUrls.RepositoryArchiveLink(owner, name, archiveFormat, reference);

            var response = await Connection.Get<byte[]>(endpoint, timeout);

            return response.Body;
        }

        /// <summary>
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        /// <returns></returns>
        public Task<RepositoryContentChangeSet> CreateFile(string owner, string name, string path, CreateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            var createUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(createUrl, request);
        }

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        /// <returns>The updated content</returns>
        public Task<RepositoryContentChangeSet> UpdateFile(string owner, string name, string path, UpdateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            var updateUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(updateUrl, request);
        }

        /// <summary>
        /// Creates a commit that deletes a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to delete</param>
        public Task DeleteFile(string owner, string name, string path, DeleteFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            var deleteUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Delete(deleteUrl, request);
        }
    }
}