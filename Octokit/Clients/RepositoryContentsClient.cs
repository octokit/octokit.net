using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit.Models.Request;

namespace Octokit
{
    public class RepositoryContentsClient : ApiClient, IRepositoryContentsClient
    {
        public RepositoryContentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<DirectoryContent>> GetRoot(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<DirectoryContent>(ApiUrls.RepositoryContent(owner, name));
        }

        public async Task<IReadOnlyList<DirectoryContent>> GetForPath(string owner, string name, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");

            // First, find content in parent directory.
            var content = await FindContent(owner, name, path);

            if (content == null)
            {
                // We've asked for a file/folder that don't exist.
                return new List<DirectoryContent>();
            }

            var url = ApiUrls.RepositoryContent(owner, name, path);

            // Check which type the content is before fetching/deserializing it.
            switch (content.Type)
            {
                case ContentType.Dir:
                    return await ApiConnection.GetAll<DirectoryContent>(url);
                case ContentType.File:
                    return new List<DirectoryContent> { await ApiConnection.Get<FileContent>(url) };
                case ContentType.Symlink:
                    return new List<DirectoryContent> { await ApiConnection.Get<SymlinkContent>(url) };
                case ContentType.Submodule:
                    return new List<DirectoryContent> { await ApiConnection.Get<SubmoduleContent>(url) };
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        /// <returns></returns>
        public Task<CreatedContent> CreateFile(string owner, string name, string path, CreateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            var createUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Put<CreatedContent>(createUrl, request);
        }

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        /// <returns>The updated content</returns>
        public Task<CreatedContent> UpdateFile(string owner, string name, string path, UpdateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");
            Ensure.ArgumentNotNull(request, "request");

            var updateUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Put<CreatedContent>(updateUrl, request);
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

        private async Task<DirectoryContent> FindContent(string owner, string name, string path)
        {
            var pathParts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var fileOrDirectoryName = pathParts.Last();

            var parentPath = string.Join("/", pathParts.TakeWhile(x => x != fileOrDirectoryName));

            var parentContentsUri = !string.IsNullOrEmpty(parentPath) 
                ? ApiUrls.RepositoryContent(owner, name, parentPath) 
                : ApiUrls.RepositoryContent(owner, name);

            var parentContents = await ApiConnection.GetAll<DirectoryContent>(parentContentsUri);

            return parentContents.FirstOrDefault(x => x.Name == fileOrDirectoryName);
        }
    }
}