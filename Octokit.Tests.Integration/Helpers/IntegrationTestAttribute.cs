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
    public class IntegrationTestDiscoverer : IXunitTestCaseDiscoverer
    {
        public IEnumerable<IXunitTestCase> Discover(ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (Helper.Credentials == null)
            {
                return Enumerable.Empty<IXunitTestCase>();
            }
            else
            {
                return new [] { new XunitTestCase(testMethod) };
            }
        }
    }


    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.IntegrationTestDiscoverer", "Octokit.Tests.Integration")]
    public class IntegrationTestAttribute : FactAttribute
    {
    }
}
