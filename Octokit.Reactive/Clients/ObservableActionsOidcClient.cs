using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{

	/// <summary>
	/// A client for GitHub's Actions OIDC API.
	/// </summary>
	/// <remarks>
	/// See the <a href="https://developer.github.com/v3/actions/oidc/">Actions OIDC API documentation</a> for more information.
	/// </remarks>
	public class ObservableActionsOidcClient : IObservableActionsOidcClient
	{
		readonly IActionsOidcClient _client;


		/// <summary>
		/// Initializes a new GitHub Actions OIDC API client
		/// </summary>
		/// <param name="client">A GitHub client.</param>
		public ObservableActionsOidcClient(IGitHubClient client)
		{
			Ensure.ArgumentNotNull(client, nameof(client));

			_client = client.Actions.Oidc;
		}


		/// <inheritdoc/>
		public IObservable<OrganizationOidcSubjectClaim> GetOrganizationOidcSubjectClaim(string organization)
		{
			Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
			return _client.GetOrganizationOidcSubjectClaim(organization).ToObservable();
		}

		/// <inheritdoc/>
		public IObservable<Unit> SetOrganizationOidcSubjectClaim(string organization, OrganizationOidcSubjectClaimRequest oidcSubjectClaim)
		{
			Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
			Ensure.ArgumentNotNull(oidcSubjectClaim, nameof(oidcSubjectClaim));

			return _client.SetOrganizationOidcSubjectClaim(organization, oidcSubjectClaim).ToObservable();
		}

		/// <inheritdoc/>
		public IObservable<RepositoryOidcSubjectClaim> GetRepositoryOidcSubjectClaim(string owner, string repository)
		{
			Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
			Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));

			return _client.GetRepositoryOidcSubjectClaim(owner, repository).ToObservable();
		}

		/// <inheritdoc/>
		public IObservable<Unit> SetRepositoryOidcSubjectClaim(string owner, string repository, RepositoryOidcSubjectClaimRequest oidcSubjectClaim)
		{
			Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
			Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
			Ensure.ArgumentNotNull(oidcSubjectClaim, nameof(oidcSubjectClaim));

			return _client.SetRepositoryOidcSubjectClaim(owner, repository, oidcSubjectClaim).ToObservable();
		}
	}
}
