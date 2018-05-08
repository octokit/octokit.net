using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ICheckRunAnnotationsClient
    {
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId);
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId);
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options);
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options);
    }
}