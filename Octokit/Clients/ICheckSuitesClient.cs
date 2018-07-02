using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ICheckSuitesClient
    {
        Task<CheckSuite> Get(string owner, string name, long checkSuiteId);
        Task<CheckSuite> Get(long repositoryId, long checkSuiteId);
        Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference);
        Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference);
        Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request);
        Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request);
        Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options);
        Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options);
        Task<CheckSuitePreferencesResponse> UpdatePreferences(string owner, string name, CheckSuitePreferences preferences);
        Task<CheckSuitePreferencesResponse> UpdatePreferences(long repositoryId, CheckSuitePreferences preferences);
        Task<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite);
        Task<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite);
        Task<bool> Request(string owner, string name, CheckSuiteTriggerRequest request);
        Task<bool> Request(long repositoryId, CheckSuiteTriggerRequest request);
    }
}