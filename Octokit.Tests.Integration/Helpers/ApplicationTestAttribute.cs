using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.Tests.Integration
{
    public class ApplicationTestDiscoverer : IXunitTestCaseDiscoverer
    {
        public IEnumerable<IXunitTestCase> Discover(ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (String.IsNullOrWhiteSpace(Helper.ClientId)
                && String.IsNullOrWhiteSpace(Helper.ClientSecret))
            {
                return Enumerable.Empty<IXunitTestCase>();
            }
            else
            {
                return new[] { new XunitTestCase(testMethod) };
            }
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.ApplicationTestDiscoverer", "Octokit.Tests.Integration")]
    public class ApplicationTestAttribute : FactAttribute
    {
    }
}
