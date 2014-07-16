#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Teams API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/teams/">Organization Teams API documentation</a> for more information.
    /// </remarks>
    public interface ITeamsClient
    {
        /// <summary>
        /// Gets a single <see cref="Team"/> by identifier.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#get-team
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <returns>The <see cref="Team"/> with the given identifier.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<Team> Get(int id);

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        Task<IReadOnlyList<Team>> GetAll(string org);

        /// <summary>
        /// Returns all members of the given team. 
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// https://developer.github.com/v3/orgs/teams/#list-team-members
        /// </remarks>
        /// <returns>A list of the team's member <see cref="User"/>s.</returns>
        Task<IReadOnlyList<User>> GetMembers(int id);

        /// <summary>
        /// Returns newly created <see cref="Team" /> for the current org.
        /// </summary>
        /// <returns>Newly created <see cref="Team"/></returns>
        Task<Team> Create(string org, NewTeam team);

        /// <summary>
        /// Returns updated <see cref="Team" /> for the current org.
        /// </summary>
        /// <returns>Updated <see cref="Team"/></returns>
        Task<Team> Update(int id, UpdateTeam team);

        /// <summary>
        /// Delte a team - must have owner permissions to this
        /// </summary>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// Adds a <see cref="User"/> to a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-team-member">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to add to the team.</param>
        /// <exception cref="ApiValidationException">Thrown if you attempt to add an organization to a team.</exception>
        /// <returns><see langword="true"/> if the user was added to the team; <see langword="false"/> otherwise.</returns>
        Task<bool> AddMember(int id, string login);

        /// <summary>
        /// Removes a <see cref="User"/> from a <see cref="Team"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#remove-team-member">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The team identifier.</param>
        /// <param name="login">The user to remove from the team.</param>
        /// <returns><see langword="true"/> if the user was removed from the team; <see langword="false"/> otherwise.</returns>
        Task<bool> RemoveMember(int id, string login);

        /// <summary>
        /// Gets whether the user with the given <paramref name="login"/> 
        /// is a member of the team with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The team to check.</param>
        /// <param name="login">The user to check.</param>
        /// <returns><see langword="true"/> if the user is a member of the team; <see langword="false"/> otherwise.</returns>
        Task<bool> IsMember(int id, string login);

        /// <summary>
        /// Returns all <see cref="Repository"/>(ies) associated with the given team. 
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#list-team-repos">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A list of the team's <see cref="Repository"/>(ies).</returns>
        Task<IReadOnlyList<Repository>> GetRepositories(int id);

        /// <summary>
        /// Adds a <see cref="Repository"/> to a <see cref="Team"/>.
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="org">Org to associate the repo with.</param>
        /// <param name="repo">Name of the repo.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#add-team-repo">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        Task<bool> AddRepository(int id, string org, string repo);

        /// <summary>
        /// Removes a <see cref="Repository"/> from a <see cref="Team"/>.
        /// </summary>
        /// <param name="id">The team identifier.</param>
        /// <param name="owner">Owner of the org the team is associated with.</param>
        /// <param name="repo">Name of the repo.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#remove-team-repo">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        Task<bool> RemoveRepository(int id, string owner, string repo);

        /// <summary>
        /// Gets whether or not the given repository is managed by the given team.
        /// </summary>
        /// <param name="id">The team identifier</param>
        /// <param name="owner">Owner of the org the team is associated with.</param>
        /// <param name="repo">Name of the repo.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/orgs/teams/#get-team-repo">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        Task<bool> IsRepositoryManagedByTeam(int id, string owner, string repo);
    }
}
