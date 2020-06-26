using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit
{
    public class OrganizationSecretsClient : ApiClient, IOrganizationSecretsClient
    {
        public OrganizationSecretsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}
