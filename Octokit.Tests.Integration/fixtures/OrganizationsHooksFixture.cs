using System;
using System.Collections.Generic;

namespace Octokit.Tests.Integration.fixtures
{
    public class OrganizationsHooksFixture : IDisposable
    {
        readonly IGitHubClient _github;
        readonly OrganizationHook _hook;
        private string organizationFixture;

        public OrganizationsHooksFixture()
        {
            _github = Helper.GetAuthenticatedClient();
            organizationFixture = "octokit";
            _hook = CreateHook(_github, organizationFixture);
        }


        public string OrganizationName { get { return organizationFixture; } }

        public OrganizationHook ExpectedHook { get { return _hook; } }

        public void Dispose()
        {
            _github.Organization.Hooks.Delete(organizationFixture,_hook.Id);
        }

        static OrganizationHook CreateHook(IGitHubClient github, string _organizationFixture)
        {
            var config = new Dictionary<string, string> { { "content_type", "json" }, { "url", "http://test.com/example" } };
            var parameters = new NewOrganizationHook("apropos", config)
            {
                Events = new[] { "commit_comment" },
                Active = false
            };
            var createdHook = github.Organization.Hooks.Create(_organizationFixture, parameters);

            return createdHook.Result;
        }
    }
}
