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
        readonly IMessageSink diagnosticMessageSink;

        public ApplicationTestDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }


        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (string.IsNullOrWhiteSpace(Helper.ClientId)
                && string.IsNullOrWhiteSpace(Helper.ClientSecret))
            {
                return Enumerable.Empty<IXunitTestCase>();
            }

            return new[] { new XunitTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) };
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.ApplicationTestDiscoverer", "Octokit.Tests.Integration")]
    public class ApplicationTestAttribute : FactAttribute
    {
    }
}
