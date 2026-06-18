using System.Threading.Tasks;
using System.Threading;

namespace Octokit.Clients
{

    /// <summary>
    /// A client for GitHub's Actions OIDC API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/oidc/">Actions OIDC API documentation</a> for more information.
    /// </remarks>
    public class ActionsOidcClient : ApiClient, IActionsOidcClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions OIDC API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsOidcClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }


        /// <inheritdoc/>
        [ManualRoute("GET", "/orgs/{organization}/actions/oidc/customization/sub")]
        public Task<OrganizationOidcSubjectClaim> GetOrganizationOidcSubjectClaim(string organization, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return ApiConnection.Get<OrganizationOidcSubjectClaim>(ApiUrls.ActionsOrganizationOidcSubjectClaim(organization));
        }

        /// <inheritdoc/>
        [ManualRoute("PUT", "/orgs/{organization}/actions/oidc/customization/sub")]
        public Task SetOrganizationOidcSubjectClaim(string organization, OrganizationOidcSubjectClaimRequest oidcSubjectClaim, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(oidcSubjectClaim, nameof(oidcSubjectClaim));

            return ApiConnection.Put(ApiUrls.ActionsOrganizationOidcSubjectClaim(organization), oidcSubjectClaim);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repository}/actions/oidc/customization/sub")]
        public Task<RepositoryOidcSubjectClaim> GetRepositoryOidcSubjectClaim(string owner, string repository, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));

            return ApiConnection.Get<RepositoryOidcSubjectClaim>(ApiUrls.ActionsRepositoryOidcSubjectClaim(owner, repository));
        }

        /// <inheritdoc/>
        [ManualRoute("PUT", "/repos/{owner}/{repository}/actions/oidc/customization/sub")]
        public Task SetRepositoryOidcSubjectClaim(string owner, string repository, RepositoryOidcSubjectClaimRequest oidcSubjectClaim, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNull(oidcSubjectClaim, nameof(oidcSubjectClaim));

            return ApiConnection.Put(ApiUrls.ActionsRepositoryOidcSubjectClaim(owner, repository), oidcSubjectClaim);
        }
    }
}
