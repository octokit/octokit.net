#if NET_45
using System.Collections.Generic;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repositories API for Contents.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/contents/">Repositories API documentation</a> for more details.
    /// </remarks>
    public interface IRepositoryContentsClient
    {
        /// <summary>
        /// Get the Contents from the Repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path of the file or folder to retrieve from the repository</param>
        /// <returns></returns>
        Task<ContentsResponse> GetContents(string owner, string name, string path);
    }
}
