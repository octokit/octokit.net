using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IGistCommentsClient
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", 
            Justification = "Method makes a network request")]
        Task<GistComment> Get(int gistId, int commentId);
        Task<IReadOnlyList<GistComment>> GetForGist(int gistId);
        Task<GistComment> Create(int gistId, string comment);
        Task<GistComment> Update(int gistId, int commentId, string comment);
        Task Delete(int gistId, int commentId);
    }
}
