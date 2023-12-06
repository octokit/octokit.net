using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;
using Octokit.Models.Request.Enterprise;

public class CopilotLicenseClient : ApiClient, ICopilotLicenseClient
{
    public CopilotLicenseClient(IApiConnection apiConnection) : base(apiConnection)
    {
    }
    
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

    public async Task<CopilotSeatAllocation> Remove(string organization, UserSeatAllocation userSeatAllocation)
    {
        Ensure.ArgumentNotNull(organization, nameof(organization));
        Ensure.ArgumentNotNull(userSeatAllocation, nameof(userSeatAllocation));

        return await ApiConnection.Delete<CopilotSeatAllocation>(ApiUrls.CopilotBillingLicense(organization), userSeatAllocation);
    }

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
    /// <param name="copilotApiOptions">Options to control page size when making API requests</param>
    /// <returns>A list of <see cref="CopilotSeats"/> instance containing the currently allocated user licenses.</returns>
    public async Task<IReadOnlyList<CopilotSeats>> GetAll(string organization, ApiOptions copilotApiOptions)
    {
        Ensure.ArgumentNotNull(organization, nameof(organization));

        ApiOptionsExtended options = new ApiOptionsExtended()
        {
            PageSize = copilotApiOptions.PageSize
        };

        return await ApiConnection.GetAll<CopilotSeats>(ApiUrls.CopilotAllocatedLicenses(organization), options);
    }
}
