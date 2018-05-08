using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ICheckRunsClient
    {
        Task<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun);
        Task<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun);
        Task<CheckRun> Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate);
        Task<CheckRun> Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate);
        Task<IReadOnlyList<CheckRun>> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest);
        Task<IReadOnlyList<CheckRun>> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest);
        Task<IReadOnlyList<CheckRun>> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest);
        Task<IReadOnlyList<CheckRun>> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest);
        Task<IReadOnlyList<CheckRun>> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options);
        Task<IReadOnlyList<CheckRun>> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options);
        Task<IReadOnlyList<CheckRun>> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options);
        Task<IReadOnlyList<CheckRun>> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options);
        Task<CheckRun> Get(long repositoryId, long checkRunId);
        Task<CheckRun> Get(string owner, string name, long checkRunId);
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId);
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId);
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options);
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options);
    }
}
