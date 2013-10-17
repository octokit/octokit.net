using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableOrganizationsClient
    {
        /// <summary>
        /// Returns the specified organization.
        /// </summary>
        /// <param name="org">The login of the specified organization,</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get"
            , Justification = "It's fine. Trust us.")]
        IObservable<Organization> Get(string org);

        /// <summary>
        /// Returns all the organizations for the current user.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request")]
        IObservable<Organization> GetAllForCurrent();

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IObservable<Organization> GetAll(string user);
    }
}
