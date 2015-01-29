using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class OrganizationTestDiscoverer : IXunitTestCaseDiscoverer
    {
        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions options, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (Helper.Organization == null)
            {
                return Enumerable.Empty<IXunitTestCase>();
            }
            else
            {
                return new[] { new XunitTestCase(TestMethodDisplay.ClassAndMethod, testMethod) };
            }
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.OrganizationTestDiscoverer", "Octokit.Tests.Integration")]
    public class OrganizationTestAttribute : FactAttribute
    {
    }
}
