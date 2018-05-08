using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class CheckRunAnnotationsClient : ApiClient, ICheckRunAnnotationsClient
    {
        public CheckRunAnnotationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId)
        {
            return GetAllAnnotations(repositoryId, checkRunId, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllAnnotations(owner, name, checkRunId, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options)
        {
            return ApiConnection.GetAll<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(repositoryId, checkRunId), null, AcceptHeaders.ChecksApiPreview, options);
        }

        public Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.GetAll<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(owner, name, checkRunId), null, AcceptHeaders.ChecksApiPreview, options);
        }
    }
}