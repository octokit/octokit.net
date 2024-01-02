using NSubstitute;
using Octokit.Clients;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Octokit.Tests.Clients
{
	public class ActionsOidcClientTests
	{
		public class TheCtor
		{
			[Fact]
			public void EnsuresNonNullArguments()
			{
				Assert.Throws<ArgumentNullException>(() => new ActionsOidcClient(null));
			}
		}

		public class GetOrganizationOidcSubjectClaim_Method
		{
			[Fact]
			public async Task RequestsCorrectUrl()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);

				await client.GetOrganizationOidcSubjectClaim("fake");

				connection.Received().Get<OrganizationOidcSubjectClaim>(
					Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/oidc/customization/sub"));
			}

			[Fact]
			public async Task EnsuresNonNullArguments()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetOrganizationOidcSubjectClaim(null));
			}

			[Fact]
			public async Task EnsuresNonEmptyArguments()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);

				await Assert.ThrowsAsync<ArgumentException>(() => client.GetOrganizationOidcSubjectClaim(""));
			}
		}

		public class SetOrganizationOidcSubjectClaim_Method
		{
			[Fact]
			public async Task RequestsCorrectUrl()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);
				var newClaims = new OrganizationOidcSubjectClaimRequest(new System.Collections.Generic.List<string> { "fake" });

				await client.SetOrganizationOidcSubjectClaim("fake", newClaims);

				connection.Received().Put(
					Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/oidc/customization/sub"), newClaims);
			}

			[Fact]
			public async Task EnsuresNonNullArguments()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);
				var newClaims = new OrganizationOidcSubjectClaimRequest(new System.Collections.Generic.List<string> { "fake" });

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetOrganizationOidcSubjectClaim(null, newClaims));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetOrganizationOidcSubjectClaim("fake", null));
			}

			[Fact]
			public async Task EnsuresNonEmptyArguments()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);
				var newClaims = new OrganizationOidcSubjectClaimRequest(new System.Collections.Generic.List<string> { "fake" });

				await Assert.ThrowsAsync<ArgumentException>(() => client.SetOrganizationOidcSubjectClaim("", newClaims));
			}
		}

		public class GetRepositoryOidcSubjectClaim_Method
		{
			[Fact]
			public async Task RequestsCorrectUrl()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);

				await client.GetRepositoryOidcSubjectClaim("fake", "abc");

				connection.Received().Get<RepositoryOidcSubjectClaim>(
					Arg.Is<Uri>(u => u.ToString() == "repos/fake/abc/actions/oidc/customization/sub"));
			}

			[Fact]
			public async Task EnsuresNonNullArguments()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRepositoryOidcSubjectClaim(null, "repo"));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRepositoryOidcSubjectClaim("owner", null));
			}

			[Fact]
			public async Task EnsuresNonEmptyArguments()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);

				await Assert.ThrowsAsync<ArgumentException>(() => client.GetRepositoryOidcSubjectClaim("", "repo"));
				await Assert.ThrowsAsync<ArgumentException>(() => client.GetRepositoryOidcSubjectClaim("owner", ""));
			}
		}

		public class SetRepositoryOidcSubjectClaim_Method
		{
			[Fact]
			public async Task RequestsCorrectUrl()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);
				var newClaims = new RepositoryOidcSubjectClaimRequest(false, new System.Collections.Generic.List<string> { "fake" });

				await client.SetRepositoryOidcSubjectClaim("fake", "abc", newClaims);

				connection.Received().Put(
					Arg.Is<Uri>(u => u.ToString() == "repos/fake/abc/actions/oidc/customization/sub"), newClaims);
			}

			[Fact]
			public async Task EnsuresNonNullArguments()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);
				var newClaims = new RepositoryOidcSubjectClaimRequest(false, new System.Collections.Generic.List<string> { "fake" });

				await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetRepositoryOidcSubjectClaim(null, "repo", newClaims));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetRepositoryOidcSubjectClaim("owner", null, newClaims));
				await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetRepositoryOidcSubjectClaim("owner", "repo", null));
			}

			[Fact]
			public async Task EnsuresNonEmptyArguments()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new ActionsOidcClient(connection);
				var newClaims = new RepositoryOidcSubjectClaimRequest(false, new System.Collections.Generic.List<string> { "fake" });

				await Assert.ThrowsAsync<ArgumentException>(() => client.SetRepositoryOidcSubjectClaim("", "repo", newClaims));
				await Assert.ThrowsAsync<ArgumentException>(() => client.SetRepositoryOidcSubjectClaim("owner", "", newClaims));
			}
		}
	}
}
