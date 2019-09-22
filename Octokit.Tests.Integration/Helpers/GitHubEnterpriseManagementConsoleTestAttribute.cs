using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class GitHubEnterpriseManagementConsoleTestDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;

        public GitHubEnterpriseManagementConsoleTestDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (Helper.Credentials == null)
                return Enumerable.Empty<IXunitTestCase>();

            if (!EnterpriseHelper.IsGitHubEnterpriseEnabled)
                return Enumerable.Empty<IXunitTestCase>();

            if (String.IsNullOrEmpty(EnterpriseHelper.ManagementConsolePassword))
                return Enumerable.Empty<IXunitTestCase>();

            return new[] { new XunitTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), TestMethodDisplayOptions.None, testMethod) };
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.GitHubEnterpriseManagementConsoleTestDiscoverer", "Octokit.Tests.Integration")]
    public class GitHubEnterpriseManagementConsoleTestAttribute : FactAttribute
    {
    }
}