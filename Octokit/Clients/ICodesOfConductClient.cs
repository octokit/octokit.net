using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// GitHub client for the Codes of Conduct API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/codes_of_conduct/">API documentation</a> for more information.
    /// </remarks>
    public interface ICodesOfConductClient
    {
        /// <summary>
        /// Gets all code of conducts on GitHub.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#list-all-codes-of-conduct">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="IReadOnlyList{CodeOfConduct}"/> on GitHub.</returns>
        Task<IReadOnlyList<CodeOfConduct>> GetAll();

        /// <summary>
        /// Gets an individual code of conduct.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#get-an-individual-code-of-conduct">API documentation</a> for more information.</remarks>
        /// <param name="key">The unique key for the Code of Conduct</param>
        /// <returns>A <see cref="CodeOfConduct"/> that includes the code of conduct key, name, and API URL.</returns>
        Task<CodeOfConduct> GetCodeOfConduct(CodeOfConductType key);

        /// <summary>
        /// Gets the code of conduct for a repository, if one is detected.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#get-the-contents-of-a-repositorys-code-of-conduct">API documentation</a> for more information.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="CodeOfConduct"/> that the repository uses, if one is detected.</returns>
        Task<CodeOfConduct> GetCodeOfConduct(string owner, string name);
    }

    /// <summary>
    /// The Code of Conduct to return from the API.
    /// </summary>
    public enum CodeOfConductType
    {
        /// <summary>
        /// The Citizen Code of Conduct
        /// </summary>
        [Parameter(Value = "citizen_code_of_conduct")]
        CitizenCodeOfConduct,

        /// <summary>
        /// The Contributor Covenant
        /// </summary>
        [Parameter(Value = "contributor_covenant")]
        ContributorCovenant
    }
}
