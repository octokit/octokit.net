using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class GitHubEnterpriseTestDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;

        public GitHubEnterpriseTestDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (Helper.Credentials == null)
                return Enumerable.Empty<IXunitTestCase>();

            if (!EnterpriseHelper.IsGitHubEnterpriseEnabled)
                return Enumerable.Empty<IXunitTestCase>();

            return new[] { new XunitTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) };
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.GitHubEnterpriseTestDiscoverer", "Octokit.Tests.Integration")]
    public class GitHubEnterpriseTestAttribute : FactAttribute
    {
    }
}