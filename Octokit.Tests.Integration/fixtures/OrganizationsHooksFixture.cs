using System;
using System.Collections.Generic;

namespace Octokit.Tests.Integration.Fixtures
{
    public class OrganizationsHooksFixture : IDisposable
    {
        readonly IGitHubClient _github;
        readonly OrganizationHook _hook;
        readonly private string _organizationFixture;

        public OrganizationsHooksFixture()
        {
            _github = Helper.GetAuthenticatedClient();
            _organizationFixture = Helper.Organization;
            _hook = CreateHook(_github, _organizationFixture);
        }


        public string org { get { return _organizationFixture; } }

        public OrganizationHook ExpectedHook { get { return _hook; } }

        public void Dispose()
        {
            _github.Organization.Hook.Delete(_organizationFixture,_hook.Id);
        }

        static OrganizationHook CreateHook(IGitHubClient github, string orgFixture)
        {
            var config = new Dictionary<string, string> { { "content_type", "json" }, { "url", "http://test.com/example" } };
            var parameters = new NewOrganizationHook("web", config)
            {
                Events = new[] { "commit_comment" },
                Active = false
            };
            var createdHook = github.Organization.Hook.Create(orgFixture, parameters);

            return createdHook.Result;
        }
    }
}
