using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    public class CheckSuitesClient : ApiClient, ICheckSuitesClient
    {
        public CheckSuitesClient(ApiConnection apiConnection) : base(apiConnection)
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

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(owner, name, reference, new CheckSuiteRequest(), ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(repositoryId, reference, new CheckSuiteRequest(), ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForReference(owner, name, reference, request, ApiOptions.None);
        }

        public Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));
            return GetAllForReference(repositoryId, reference, request, ApiOptions.None);
        }

        public async Task<IReadOnlyList<CheckSuite>> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<CheckSuiteList>(ApiUrls.ReferenceCheckSuites(owner, name, reference), request.ToParametersDictionary(), AcceptHeaders.ChecksApiPreview, options).ConfigureAwait(false);
            return results.SelectMany(x => x.CheckSuites).ToList();
        }

        public async Task<IReadOnlyList<CheckSuite>> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            var results = await ApiConnection.GetAll<CheckSuiteList>(ApiUrls.ReferenceCheckSuites(repositoryId, reference), request.ToParametersDictionary(), AcceptHeaders.ChecksApiPreview, options).ConfigureAwait(false);
            return results.SelectMany(x => x.CheckSuites).ToList();
        }

        public Task<CheckSuite> Request(string owner, string name, CheckSuiteTriggerRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return ApiConnection.Post<CheckSuite>(ApiUrls.CheckSuites(owner, name), request, AcceptHeaders.ChecksApiPreview);
        }


        public Task<CheckSuite> Request(long repositoryId, CheckSuiteTriggerRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return ApiConnection.Post<CheckSuite>(ApiUrls.CheckSuites(repositoryId), request, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckSuitePreferences> UpdatePreferences(string owner, string name, AutoTriggerChecksObject preferences)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return ApiConnection.Patch<CheckSuitePreferences>(ApiUrls.CheckSuitePreferences(owner, name), preferences, AcceptHeaders.ChecksApiPreview);
        }

        public Task<CheckSuitePreferences> UpdatePreferences(long repositoryId, AutoTriggerChecksObject preferences)
        {
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return ApiConnection.Patch<CheckSuitePreferences>(ApiUrls.CheckSuitePreferences(repositoryId), preferences, AcceptHeaders.ChecksApiPreview);
        }
    }
}