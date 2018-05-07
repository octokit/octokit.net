using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ICheckRunAnnotationsClient
    {
        Task<IReadOnlyList<CheckAnnotation>> List(long repositoryId, long checkRunId);
        Task<IReadOnlyList<CheckAnnotation>> List(string owner, string name, long checkRunId);
    }
}