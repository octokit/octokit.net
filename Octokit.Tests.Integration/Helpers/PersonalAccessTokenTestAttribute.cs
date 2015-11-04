using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class PersonalAccessTokenTestDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;

        public PersonalAccessTokenTestDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            return Helper.IsUsingToken
                ? new[] { new XunitTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) }
                : Enumerable.Empty<IXunitTestCase>();
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.PersonalAccessTokenTestDiscoverer", "Octokit.Tests.Integration")]
    public class PersonalAccessTokenTestAttribute : FactAttribute
    {
    }
}
