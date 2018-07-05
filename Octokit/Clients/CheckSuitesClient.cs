using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class CheckSuitesClient : ApiClient, ICheckSuitesClient
    {
        public CheckSuitesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckSuite, nameof(newCheckSuite));

            return ApiConnection.Post<CheckSuite>(ApiUrls.CheckSuites(owner, name), newCheckSuite, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite)
        {
            Ensure.ArgumentNotNull(newCheckSuite, nameof(newCheckSuite));

            return ApiConnection.Post<CheckSuite>(ApiUrls.CheckSuites(repositoryId), newCheckSuite, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckSuite> Get(string owner, string name, long checkSuiteId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<CheckSuite>(ApiUrls.CheckSuite(owner, name, checkSuiteId), null, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckSuite> Get(long repositoryId, long checkSuiteId)
        {
            return ApiConnection.Get<CheckSuite>(ApiUrls.CheckSuite(repositoryId, checkSuiteId), null, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(owner, name, reference, new CheckSuiteRequest(), ApiOptions.None);
        }

        public Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(repositoryId, reference, new CheckSuiteRequest(), ApiOptions.None);
        }

        public Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForReference(owner, name, reference, request, ApiOptions.None);
        }

        public Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForReference(repositoryId, reference, request, ApiOptions.None);
        }

        public async Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<CheckSuitesResponse>(ApiUrls.ReferenceCheckSuites(owner, name, reference), request.ToParametersDictionary(), AcceptHeaders.ChecksApiPreview, options).ConfigureAwait(false);

            return new CheckSuitesResponse(
                results.Max(x => x.TotalCount),
                results.SelectMany(x => x.CheckSuites).ToList());
        }

        public async Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            var results = await ApiConnection.GetAll<CheckSuitesResponse>(ApiUrls.ReferenceCheckSuites(repositoryId, reference), request.ToParametersDictionary(), AcceptHeaders.ChecksApiPreview, options).ConfigureAwait(false);

            return new CheckSuitesResponse(
                results.Max(x => x.TotalCount),
                results.SelectMany(x => x.CheckSuites).ToList());
        }

        public async Task<bool> Request(string owner, string name, CheckSuiteTriggerRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            var httpStatusCode = await Connection.Post(ApiUrls.CheckSuiteRequests(owner, name), request, AcceptHeaders.ChecksApiPreview).ConfigureAwait(false);

            if (httpStatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", httpStatusCode);
            }

            return httpStatusCode == HttpStatusCode.Created;
        }


        public async Task<bool> Request(long repositoryId, CheckSuiteTriggerRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var httpStatusCode = await Connection.Post(ApiUrls.CheckSuiteRequests(repositoryId), request, AcceptHeaders.ChecksApiPreview).ConfigureAwait(false);
            
            if (httpStatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", httpStatusCode);
            }

            return httpStatusCode == HttpStatusCode.Created;
        }

        public Task<CheckSuitePreferencesResponse> UpdatePreferences(string owner, string name, CheckSuitePreferences preferences)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return ApiConnection.Patch<CheckSuitePreferencesResponse>(ApiUrls.CheckSuitePreferences(owner, name), preferences, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckSuitePreferencesResponse> UpdatePreferences(long repositoryId, CheckSuitePreferences preferences)
        {
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return ApiConnection.Patch<CheckSuitePreferencesResponse>(ApiUrls.CheckSuitePreferences(repositoryId), preferences, AcceptHeaders.ChecksApiPreview);
        }
    }
}