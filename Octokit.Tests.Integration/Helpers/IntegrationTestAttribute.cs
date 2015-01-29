using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class IntegrationTestDiscoverer : IXunitTestCaseDiscoverer
    {
        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions options, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            return Helper.Credentials == null
                ? Enumerable.Empty<IXunitTestCase>()
                : new [] { new XunitTestCase(testMethod) };
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.IntegrationTestDiscoverer", "Octokit.Tests.Integration")]
    public class IntegrationTestAttribute : FactAttribute
    {
    }
}
