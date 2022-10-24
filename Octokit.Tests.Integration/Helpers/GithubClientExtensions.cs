﻿using System;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal static class GithubClientExtensions
    {
        /// <summary>
        /// Creates a public repository with the timestamped name public-repo and autoinit true
        /// </summary>
        internal static async Task<RepositoryContext> CreateRepositoryContextWithAutoInit(this IGitHubClient client)
        {
            return await client.CreateUserRepositoryContext(x => x.AutoInit = true);
        }

        /// <summary>
        /// Creates a public repository with the repository name passed in and autoinit true
        /// </summary>
        internal static async Task<RepositoryContext> CreateRepositoryContextWithAutoInit(this IGitHubClient client, string repositoryName)
        {
            return await client.CreateUserRepositoryContext(Helper.MakeNameWithTimestamp(repositoryName), x => x.AutoInit = true);
        }

        internal static async Task<RepositoryContext> CreateRepositoryContext(this IGitHubClient client, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        /// <summary>
        /// Creates a public repository with the timestamped name "public-repo" and an optional function of parameters passed in
        /// </summary>
        /// <param name="client"></param>
        /// <param name="repositoryName">The name of the repository without the timestamp</param>
        /// <param name="action">Function setting parameters on the NewRepository</param>
        /// <returns></returns>
        internal static async Task<RepositoryContext> CreateUserRepositoryContext(this IGitHubClient client, Action<NewRepository> action = null)
        {
            var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo"));
            action?.Invoke(newRepository);

            var repo = await client.Repository.Create(newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        /// <summary>
        /// Creates a public repository with the repository name and an optional function of parameters passed in
        /// </summary>
        /// <param name="client"></param>
        /// <param name="repositoryName">The name of the repository without the timestamp</param>
        /// <param name="action"></param>
        /// <returns></returns>
        internal static async Task<RepositoryContext> CreateUserRepositoryContext(this IGitHubClient client, string repositoryName, Action<NewRepository> action = null)
        {
            var newRepository = new NewRepository(Helper.IsNameTimestamped(repositoryName) ? repositoryName : Helper.MakeNameWithTimestamp(repositoryName));
            action?.Invoke(newRepository);

            var repo = await client.Repository.Create(newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        /// <summary>
        /// Creates an organisation repository with the timestamped name organisation-repo and an optional function of parameters
        /// </summary>
        internal static async Task<RepositoryContext> CreateOrganizationRepositoryContext(this IGitHubClient client, Action<NewRepository> action = null)
        {
            var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("organization-repo"));
            action?.Invoke(newRepository);

            var repo = await client.Repository.Create(Helper.Organization, newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        /// <summary>
        /// Creates an organisation repository with the passed in repository name repo and an optional function of parameters
        /// </summary>
        internal static async Task<RepositoryContext> CreateOrganizationRepositoryContext(this IGitHubClient client, string repositoryName, Action<NewRepository> action = null)
        {
            var newRepository = new NewRepository(Helper.IsNameTimestamped(repositoryName) ? repositoryName : Helper.MakeNameWithTimestamp(repositoryName));
            action?.Invoke(newRepository);

            var repo = await client.Repository.Create(Helper.Organization, newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        /// <summary>
        /// Creates an organisation repository with the timestamped name organisation-repo
        /// </summary>
        internal static async Task<RepositoryContext> CreateOrganizationRepositoryContextWithAutoInit(this IGitHubClient client, Action<NewRepository> action = null)
        {
            var repoName = Helper.MakeNameWithTimestamp("organization-repo");
            var repo = await client.Repository.Create(Helper.Organization, new NewRepository(repoName) { AutoInit = true });

            return new RepositoryContext(client.Connection, repo);
        }

        /// <summary>
        /// Creates an organization repository
        /// </summary>
        internal static async Task<RepositoryContext> CreateOrganizationRepositoryContext(this IGitHubClient client, string organizationLogin, NewRepository newRepository)
        {
            var repo = await client.Repository.Create(organizationLogin, newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        /// <summary>
        /// Creates a private repository with the timestamped name private-repo
        /// </summary>
        internal static async Task<RepositoryContext> CreatePrivateRepositoryContext(this IGitHubClient client)
        {
            var repoName = Helper.MakeNameWithTimestamp("private-repo");
            var repo = await client.Repository.Create(new NewRepository(repoName) { Private = true });

            return new RepositoryContext(client.Connection, repo);
        }

        internal static async Task<RepositoryContext> Generate(this IGitHubClient client, string owner, string repoName, NewRepositoryFromTemplate newRepository)
        {
            var repo = await client.Repository.Generate(owner, repoName, newRepository);

            return new RepositoryContext(client.Connection, repo);
        }

        internal static async Task<TeamContext> CreateTeamContext(this IGitHubClient client, string organization, NewTeam newTeam)
        {
            newTeam.Privacy = TeamPrivacy.Closed;
            var team = await client.Organization.Team.Create(organization, newTeam);

            return new TeamContext(client.Connection, team);
        }

        internal static async Task<EnterpriseUserContext> CreateEnterpriseUserContext(this IGitHubClient client, NewUser newUser)
        {
            var user = await client.User.Administration.Create(newUser);

            return new EnterpriseUserContext(client.Connection, user);
        }

        internal static async Task<PublicKeyContext> CreatePublicKeyContext(this IGitHubClient client)
        {
            // Create a key
            string keyTitle = "title";
            string keyData = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAQEAq42HufbSy1BUbZTdKyEy8nX44gdchbh1A/cYuVFkRXETrFr6XYLETi4tauXGS3Wp3E4s3oG272O4JW+fIBX0kuOJXnRgYz52H3BDk6aY9B0ny+PYFJrYrpG43px5EVfojj9o7oxugNq4zLCGqWTqZU1maTf5T4Mopjt0ggA7cyNnM5B645cBxXjD2KNfrTIyLI+meYxptzjRiB6fHLGFRA9fxpVqUnbq7EcGbwsTlILRuEPt58hZ9He88M45m0F8rkVZOewt4JSzsLsC+sQs+h/LXI8dbrg6xWpxJVi0trzYuMuY/MwygloWKtaFQYuPkJ7yqMZ3Aew+J3DupF6uxQ==";

            var key = await client.User.GitSshKey.Create(new NewPublicKey(keyTitle, keyData));

            return new PublicKeyContext(client.Connection, key);
        }

        internal static async Task<GpgKeyContext> CreateGpgKeyContext(this IGitHubClient client)
        {
            // Create a key
            string publicKey = @"
-----BEGIN PGP PUBLIC KEY BLOCK-----
Version: BCPG C# v1.6.1.0

mQENBFdTvCUBCADOaVtPoJTQOgMIVYEpI8uT60LA/kDqw/1OKn7ftKjAtxNVSgjQ
i/ZqZp8XKjTg2u6l4c/aPjER2BGTg90xCcbmpwq/kkQuHR4DK7dOlEOoTzDDESEF
v6XXlXGCnxN8AD8YNvSO+Sy4+35ihuKUBAHDxmOl7ZAMH0STo10KuW82/DhfT3cC
JNqmID7H+CW1H6IhwutPKt8XsVq2FQg2RMx+uX1KqkuBAd7b30KJ93SJqzgq5CC3
DticaC0/WdZnxmYD01UvMAS6o/REs+SICdsyTxgBx/X8SIXuX2TD9PG/O2785JI5
/xvBd4jU6bBH/4oWoHr3e/lyNqb1+GSeTFX3ABEBAAG0AIkBHAQQAQIABgUCV1O8
JQAKCRDohALS4SeiOs/QB/9PMeFNdPkB1xfBm3qvTErqvhTcQspPibucYefG6JHL
vhm6iCOVBeCuPS4P/T8RTzb0qJaTdZZWcwZ9UjRVqF/RKwdMJTBKBHRegc5hRjLH
Koxk0zXosk+CapIR6eVhHe4IzpVVxZOvunsFOclIh+qHx9UzJhz9wSO/XBn/6Rzr
DGzE9fpK1JRKC0I3PuiDCNuZvojXeUsT/zuHYsz00wnA2Em7CmcWWng3nPUSHvBB
GUJ7YE7NvYXqT09PdhoZnf9p1wOugiuG6CLzWf8stlNV3mZptpP+sCGcz/UVffRO
VO/+BCBsaoT4g1FFOmJhbBAD3G72yslBnUJmqKP/39pi
=O7Yi
-----END PGP PUBLIC KEY BLOCK-----
";

            var key = await client.User.GpgKey.Create(new NewGpgKey(publicKey));

            return new GpgKeyContext(client.Connection, key);
        }

        internal static MaintenanceModeContext CreateMaintenanceModeContext(this IGitHubClient client, bool enabled)
        {
            return new MaintenanceModeContext(client.Connection, enabled);
        }
    }
}