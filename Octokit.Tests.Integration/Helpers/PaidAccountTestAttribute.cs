using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class PaidAccountTestDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;

        public PaidAccountTestDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (Helper.Credentials == null)
                return Enumerable.Empty<IXunitTestCase>();

            if (!Helper.IsPaidAccount)
                return Enumerable.Empty<IXunitTestCase>();

            return new[] { new XunitTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) };
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.PaidAccountTestDiscoverer", "Octokit.Tests.Integration")]
    public class PaidAccountTestAttribute : FactAttribute
    {
    }
}