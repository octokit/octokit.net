using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class IntegrationTestAttribute : FactAttribute
    {
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo testMethod)
        {
            if (Helper.Credentials == null)
                yield return new SkipCommand(testMethod, MethodUtility.GetDisplayName(testMethod), "Automation settings not configured. Please set the OCTOKIT_GITHUBUSERNAME and OCTOKIT_GITHUBPASSWORD environment variables to a GitHub test account (i.e, DO NOT USE A \"REAL\" ACCOUNT).");
            else
                yield return new FactCommand(testMethod);
        }
    }
}
