using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class DualAccountTestDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;

        public DualAccountTestDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (Helper.CredentialsSecondUser == null)
            {
                return Enumerable.Empty<IXunitTestCase>();
            }

            return new[] { new XunitTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), TestMethodDisplayOptions.None, testMethod) };
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.DualAccountTestDiscoverer", "Octokit.Tests.Integration")]
    public class DualAccountTestAttribute : FactAttribute
    {
    }
}
