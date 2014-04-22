using System.Collections.Generic;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class OrganizationTestAttribute : IntegrationTestAttribute
    {
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo testMethod)
        {
            if (Helper.Organization == null)
                return new[] 
                { 
                   new SkipCommand(testMethod, MethodUtility.GetDisplayName(testMethod), "Automation settings not configured. Please set the OCTOKIT_GITHUBORGANIZATION environment variable to a GitHub organization owned by the test account specified in OCTOKIT_GITHUBUSERNAME.")
                };
            else
                return base.EnumerateTestCommands(testMethod);
        }
    }
}
