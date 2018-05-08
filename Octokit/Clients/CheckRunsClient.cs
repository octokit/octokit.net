using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class CheckRunsClient : ApiClient, ICheckRunsClient
    {
        public CheckRunsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task Create(long repositoryId, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return ApiConnection.Post<CheckRun>(ApiUrls.CheckRuns(repositoryId), newCheckRun, AcceptHeaders.ChecksApiPreview);
        }

        public Task Create(string owner, string name, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return ApiConnection.Post<CheckRun>(ApiUrls.CheckRuns(owner, name), newCheckRun, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckRun> Get(long repositoryId, long checkRunId)
        {
            return ApiConnection.Get<CheckRun>(ApiUrls.CheckRun(repositoryId, checkRunId), null, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckRun> Get(string owner, string name, long checkRunId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<CheckRun>(ApiUrls.CheckRun(owner, name, checkRunId), null, AcceptHeaders.ChecksApiPreview);
        }

        public Task<IReadOnlyList<CheckRun>> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForReference(repositoryId, reference, checkRunRequest, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckRun>> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForReference(owner, name, reference, checkRunRequest, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckRun>> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            
            return GetAllForCheckSuite(repositoryId, checkSuiteId, checkRunRequest, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckRun>> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForCheckSuite(owner, name, checkSuiteId, checkRunRequest, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckRun>> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CheckRun>> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CheckRun>> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CheckRun>> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public Task Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate)
        {
            Ensure.ArgumentNotNull(checkRunUpdate, nameof(checkRunUpdate));

            return ApiConnection.Patch<CheckRun>(ApiUrls.CheckRun(repositoryId, checkRunId), checkRunUpdate, AcceptHeaders.ChecksApiPreview);
        }

        public Task Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunUpdate, nameof(checkRunUpdate));

            return ApiConnection.Patch<CheckRun>(ApiUrls.CheckRun(owner, name, checkRunId), checkRunUpdate, AcceptHeaders.ChecksApiPreview);
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
