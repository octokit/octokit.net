using System;
using System.Collections.Generic;
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
                yield return new SkipTestCase(testMethod,
                    "Environment variables are not set for this test - set OCTOKIT_CLIENTID and OCTOKIT_CLIENTSECRET");
            }

            yield return new XunitTestCase(testMethod);
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.ApplicationTestDiscoverer", "Octokit.Tests.Integration")]
    public class ApplicationTestAttribute : FactAttribute
    {
    }

    public class SkipTestCase : XunitTestCase
    {
        public SkipTestCase(ITestMethod testMethod, string skipReason)
            : base(testMethod)
        {
            SkipReason = skipReason;
        }
    }
}
