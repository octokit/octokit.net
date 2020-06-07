using System;
using System.Collections.Generic;

namespace Octokit.Tests.Integration.Fixtures
{
    public class OrganizationsHooksFixture : IDisposable
    {
        readonly IGitHubClient _github;
        readonly OrganizationHook _hook;
        readonly private string _organizationFixture;
        readonly IList<OrganizationHook> _hooks;

        public OrganizationsHooksFixture()
        {
            _github = Helper.GetAuthenticatedClient();
            _organizationFixture = Helper.Organization;
            _hooks = new List<OrganizationHook>(5)
            {
                CreateHook(_github, _organizationFixture, "awscodedeploy", "deployment"),
                CreateHook(_github, _organizationFixture, "awsopsworks", "push"),
                CreateHook(_github, _organizationFixture, "activecollab", "push"),
                CreateHook(_github, _organizationFixture, "acunote", "push")
            };
            _hook = _hooks[0];
        }

        public string org { get { return _organizationFixture; } }

        public OrganizationHook ExpectedHook { get { return _hook; } }

        public IList<OrganizationHook> ExpectedHooks { get { return _hooks; } }

        public void Dispose()
        {
            _github.Organization.Hook.Delete(_organizationFixture, _hook.Id);
        }

        static OrganizationHook CreateHook(IGitHubClient github, string orgFixture, string hookName, string eventName)
        {
            var config = new Dictionary<string, string> { { "content_type", "json" }, { "url", "http://test.com/example" } };
            var parameters = new NewOrganizationHook(hookName, config)
            {
                Events = new[] { eventName },
                Active = false
            };
            var createdHook = github.Organization.Hook.Create(orgFixture, parameters);

            return createdHook.Result;
        }
    }
}
