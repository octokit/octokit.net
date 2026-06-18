using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading;

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
		public IObservable<OrganizationOidcSubjectClaim> GetOrganizationOidcSubjectClaim(string organization, CancellationToken cancellationToken = default)
		{
			Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
			return _client.GetOrganizationOidcSubjectClaim(organization, cancellationToken).ToObservable();
		}

		/// <inheritdoc/>
		public IObservable<Unit> SetOrganizationOidcSubjectClaim(string organization, OrganizationOidcSubjectClaimRequest oidcSubjectClaim, CancellationToken cancellationToken = default)
		{
			Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
			Ensure.ArgumentNotNull(oidcSubjectClaim, nameof(oidcSubjectClaim));

			return _client.SetOrganizationOidcSubjectClaim(organization, oidcSubjectClaim, cancellationToken).ToObservable();
		}

		/// <inheritdoc/>
		public IObservable<RepositoryOidcSubjectClaim> GetRepositoryOidcSubjectClaim(string owner, string repository, CancellationToken cancellationToken = default)
		{
			Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
			Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));

			return _client.GetRepositoryOidcSubjectClaim(owner, repository, cancellationToken).ToObservable();
		}

		/// <inheritdoc/>
		public IObservable<Unit> SetRepositoryOidcSubjectClaim(string owner, string repository, RepositoryOidcSubjectClaimRequest oidcSubjectClaim, CancellationToken cancellationToken = default)
		{
			Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
			Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
			Ensure.ArgumentNotNull(oidcSubjectClaim, nameof(oidcSubjectClaim));

			return _client.SetRepositoryOidcSubjectClaim(owner, repository, oidcSubjectClaim, cancellationToken).ToObservable();
		}
	}
}
