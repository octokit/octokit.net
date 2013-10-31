
using System.Threading.Tasks;

namespace Octokit
{
    public class IssueCommentsClient : ApiClient, IIssueCommentsClient
    {
        public IssueCommentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<IssueComment> Get(string owner, string name, int number)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<IssueComment>> GetForRepository(string owner, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<IssueComment>> GetForIssue(string owner, string name, int number)
        {
            throw new System.NotImplementedException();
        }

        public Task<IssueComment> Create(string owner, string name, int number, string newComment)
        {
            throw new System.NotImplementedException();
        }

        public Task<IssueComment> Update(string owner, string name, int number, string commentUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
