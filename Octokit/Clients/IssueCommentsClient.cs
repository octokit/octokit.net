
using System.Threading.Tasks;

namespace Octokit
{
    public class IssueCommentsClient : ApiClient, IIssueCommentsClient
    {
        public IssueCommentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<Issue> Get(string owner, string name, int number)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Issue>> GetForRepository(string owner, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Issue>> GetForIssue(string owner, string name, int number)
        {
            throw new System.NotImplementedException();
        }

        public Task<Issue> Create(string owner, string name, int number, string newComment)
        {
            throw new System.NotImplementedException();
        }

        public Task<Issue> Update(string owner, string name, int number, string commentUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
