using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Models.Request;

namespace Octokit
{
    public interface IRepositoryContentsClient
    {
        Task<IReadOnlyList<DirectoryContent>> GetRoot(string owner, string name);

        Task<IReadOnlyList<DirectoryContent>> GetForPath(string owner, string name, string path);

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
        Task<Readme> GetReadme(string owner, string name);

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
        Task<string> GetReadmeHtml(string owner, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CreatedContent> CreateFile(string owner, string name, string path, CreateFileRequest request);

        Task<CreatedContent> UpdateFile(string owner, string name, string path, UpdateFileRequest request);

        Task DeleteFile(string owner, string name, string path, DeleteFileRequest request);
    }
}