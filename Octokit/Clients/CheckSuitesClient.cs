using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class CheckSuitesClient : ApiClient, ICheckSuitesClient
    {
        public CheckSuitesClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task Create(long repositoryId, NewCheckSuite newCheckSuite)
        {
            throw new System.NotImplementedException();
        }

        public Task Create(string owner, string name, NewCheckSuite newCheckSuite)
        {
            throw new System.NotImplementedException();
        }

        public Task<CheckSuite> Get(long repositoryId, long checkSuiteId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CheckSuite> Get(string owner, string name, long checkSuiteId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference, ApiOptions options)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference, ApiOptions options)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            throw new System.NotImplementedException();
        }

        public Task Request(long repositoryId, CheckSuiteTriggerRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task Request(string owner, string name, CheckSuiteTriggerRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePreferences(long repositoryId, AutoTriggerChecksObject preferences)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePreferences(string owner, string name, AutoTriggerChecksObject preferences)
        {
            throw new System.NotImplementedException();
        }
    }
}