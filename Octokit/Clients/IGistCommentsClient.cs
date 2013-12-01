using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IGistCommentsClient
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", 
            Justification = "Method makes a network request")]
        Task<GistComment> Get(string gistId, string commentId);
        Task<IReadOnlyList<GistComment>> GetForGist(string gistId);
        Task<GistComment> Create(string gistId, string comment);
        Task<GistComment> Update(string gistId, string commentId, string comment);
        Task Delete(string gistId, string commentId);
    }
}
