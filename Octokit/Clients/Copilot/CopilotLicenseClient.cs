using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using Octokit.Models.Request.Enterprise;

/// <summary>
/// A client for managing licenses for GitHub Copilot for Business
/// </summary>
public class CopilotLicenseClient : ApiClient, ICopilotLicenseClient
{
    /// <summary>
    /// Initializes a new GitHub Copilot for Business License API client.
    /// </summary>
    /// <param name="apiConnection">An API connection</param>
    public CopilotLicenseClient(IApiConnection apiConnection) : base(apiConnection)
    {
    }
    
    /// <summary>
    /// Removes a license for a user
    /// </summary>
    /// <param name="organization">The organization name</param>
    /// <param name="userName">The github users profile name to remove a license from</param>
    /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
    [ManualRoute("DELETE", "/orgs/{org}/copilot/billing/selected_users")]
    public async Task<CopilotSeatAllocation> Remove(string organization, string userName)
    {
        Ensure.ArgumentNotNull(organization, nameof(organization));
        Ensure.ArgumentNotNull(userName, nameof(userName));

        var allocation = new UserSeatAllocation
        {
            SelectedUsernames = new[] { userName }
        };

        return await Remove(organization, allocation);
    }

    /// <summary>
    /// Removes a license for one or many users
    /// </summary>
    /// <param name="organization">The organization name</param>
    /// <param name="userSeatAllocation">A <see cref="UserSeatAllocation"/> instance, containing the names of the user(s) to remove licenses for</param>
    /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
    [ManualRoute("DELETE", "/orgs/{org}/copilot/billing/selected_users")]
    public async Task<CopilotSeatAllocation> Remove(string organization, UserSeatAllocation userSeatAllocation)
    {
        Ensure.ArgumentNotNull(organization, nameof(organization));
        Ensure.ArgumentNotNull(userSeatAllocation, nameof(userSeatAllocation));

        return await ApiConnection.Delete<CopilotSeatAllocation>(ApiUrls.CopilotBillingLicense(organization), userSeatAllocation);
    }

    /// <summary>
    /// Assigns a license to a user
    /// </summary>
    /// <param name="organization">The organization name</param>
    /// <param name="userName">The github users profile name to add a license to</param>
    /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
    [ManualRoute("POST", "/orgs/{org}/copilot/billing/selected_users")]
    public async Task<CopilotSeatAllocation> Assign(string organization, string userName)
    {
        Ensure.ArgumentNotNull(organization, nameof(organization));
        Ensure.ArgumentNotNull(userName, nameof(userName));

        var allocation = new UserSeatAllocation
        {
            SelectedUsernames = new[] { userName }
        };

        return await Assign(organization, allocation);
    }
    
    /// <summary>
    /// Assigns a license for one or many users
    /// </summary>
    /// <param name="organization">The organization name</param>
    /// <param name="userSeatAllocation">A <see cref="UserSeatAllocation"/> instance, containing the names of the user(s) to add licenses to</param>
    /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
    [ManualRoute("POST", "/orgs/{org}/copilot/billing/selected_users")]
    public async Task<CopilotSeatAllocation> Assign(string organization, UserSeatAllocation userSeatAllocation)
    {
        Ensure.ArgumentNotNull(organization, nameof(organization));
        Ensure.ArgumentNotNull(userSeatAllocation, nameof(userSeatAllocation));

        return await ApiConnection.Post<CopilotSeatAllocation>(ApiUrls.CopilotBillingLicense(organization), userSeatAllocation);
    }

    /// <summary>
    /// Gets all of the currently allocated licenses for an organization
    /// </summary>
    /// <param name="organization">The organization</param>
    /// <param name="options">Options to control page size when making API requests</param>
    /// <returns>A list of <see cref="CopilotSeats"/> instance containing the currently allocated user licenses.</returns>
    [ManualRoute("GET", "/orgs/{org}/copilot/billing/seats")]
    public async Task<IReadOnlyList<CopilotSeats>> GetAll(string organization, ApiOptions options)
    {
        Ensure.ArgumentNotNull(organization, nameof(organization));

        var extendedOptions = new ApiOptionsExtended()
        {
            PageSize = options.PageSize
        };

        return await ApiConnection.GetAll<CopilotSeats>(ApiUrls.CopilotAllocatedLicenses(organization), extendedOptions);
    }
}
