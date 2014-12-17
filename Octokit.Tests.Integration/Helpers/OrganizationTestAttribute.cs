using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class OrganizationTestDiscoverer : IXunitTestCaseDiscoverer
    {
        public IEnumerable<IXunitTestCase> Discover(ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (Helper.Organization == null)
            {
                return Enumerable.Empty<IXunitTestCase>();
            }
            else
            {
                return new[] { new OrganizationTestCase(testMethod) };
            }
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.OrganizationTestDiscoverer", "Octokit.Tests.Integration")]
    public class OrganizationTestAttribute : FactAttribute
    {
    }

    [Serializable]
    public class OrganizationTestCase : XunitTestCase
    {
        public OrganizationTestCase(ITestMethod testMethod)
            : base(testMethod, testMethodArguments: null)
        {
        }
    }

}
