using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    interface ICheckRunsClient
    {
        ICheckRunAnnotationsClient Annotations { get; }

        Task Create(long repositoryId, NewCheckRun newCheckRun);
        Task Create(string owner, string name, NewCheckRun newCheckRun);
        Task Update(long repositoryId, CheckRunUpdate checkRunUpdate);
        Task Update(string owner, string name, CheckRunUpdate checkRunUpdate);
        Task<IReadOnlyList<CheckRun>> List(long repositoryId, string reference, CheckRunRequest checkRunRequest);
        Task<IReadOnlyList<CheckRun>> List(string owner, string name, string reference, CheckRunRequest checkRunRequest);
        Task<IReadOnlyList<CheckRun>> List(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest);
        Task<IReadOnlyList<CheckRun>> List(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest);
        Task<CheckRun> Get(long repositoryId, long checkRunId);
        Task<CheckRun> Get(string owner, string name, long checkRunId);
    }
}
