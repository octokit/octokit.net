using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class GitHubAppsTestDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;

        public GitHubAppsTestDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (!Helper.IsGitHubAppsEnabled)
                return Enumerable.Empty<IXunitTestCase>();

            Credentials creds = null;
            try
            {
                // Make sure we can generate GitHub App credentials
                creds = Helper.GitHubAppCredentials;
            }
            catch
            {
            }

            if (creds == null)
            {
                return Enumerable.Empty<IXunitTestCase>();
            }

            return new[] { new XunitTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), TestMethodDisplayOptions.None, testMethod) };
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.GitHubAppsTestDiscoverer", "Octokit.Tests.Integration")]
    public class GitHubAppsTestAttribute : FactAttribute
    {
    }
}