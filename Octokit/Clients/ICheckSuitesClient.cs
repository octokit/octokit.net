using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ICheckSuitesClient
    {
        Task<CheckSuite> Get(string owner, string name, long checkSuiteId);
        Task<CheckSuite> Get(long repositoryId, long checkSuiteId);
        Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference);
        Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference);
        Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request);
        Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request);
        Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options);
        Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options);
        Task<CheckSuitePreferences> UpdatePreferences(string owner, string name, AutoTriggerChecksObject preferences);
        Task<CheckSuitePreferences> UpdatePreferences(long repositoryId, AutoTriggerChecksObject preferences);
        Task<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite);
        Task<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite);
        Task<CheckSuite> Request(string owner, string name, CheckSuiteTriggerRequest request);
        Task<CheckSuite> Request(long repositoryId, CheckSuiteTriggerRequest request);
    }
}