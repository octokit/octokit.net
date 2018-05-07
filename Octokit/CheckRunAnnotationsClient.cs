using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    internal class CheckRunAnnotationsClient : ApiClient, ICheckRunAnnotationsClient
    {
        public CheckRunAnnotationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<CheckRunAnnotation>> List(long repositoryId, long checkRunId)
        {
            return List(repositoryId, checkRunId, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckRunAnnotation>> List(string owner, string name, long checkRunId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return List(owner, name, checkRunId, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckRunAnnotation>> List(long repositoryId, long checkRunId, ApiOptions options)
        {
            return ApiConnection.GetAll<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(repositoryId, checkRunId), options);
        }

        public Task<IReadOnlyList<CheckRunAnnotation>> List(string owner, string name, long checkRunId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.GetAll<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(owner, name, checkRunId), options);
        }
    }
}