using System;

namespace Octokit.Reactive
{
    public interface IObservableCodesOfConductClient
    {
        /// <summary>
        /// Gets all code of conducts on GitHub.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#list-all-codes-of-conduct">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="IObservable{CodeOfConduct}"/> on GitHub.</returns>
        IObservable<CodeOfConduct> GetAll();

        /// <summary>
        /// Gets an individual code of conduct.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#get-an-individual-code-of-conduct">API documentation</a> for more information.</remarks>
        /// <param name="key">The unique key for the Code of Conduct</param>
        /// <returns>A <see cref="IObservable{CodeOfConduct}"/> that includes the code of conduct key, name, and API/HTML URL.</returns>
        IObservable<CodeOfConduct> Get(CodeOfConductType key);

        /// <summary>
        /// Gets the code of conduct for a repository, if one is detected.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#get-the-contents-of-a-repositorys-code-of-conduct">API documentation</a> for more information.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="IObservable{CodeOfConduct}"/> that the repository uses, if one is detected.</returns>
        IObservable<CodeOfConduct> Get(string owner, string name);
    }
}
