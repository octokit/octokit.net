using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            yield return new IntegrationTestCase(testMethod);
        }
    }


    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.IntegrationTestDiscoverer", "Octokit.Tests.Integration")]
    public class IntegrationTestAttribute : FactAttribute
    {
    }

    [Serializable]
    public class IntegrationTestCase : XunitTestCase
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer", true)]
        public IntegrationTestCase() { }

        public IntegrationTestCase(ITestMethod testMethod)
            : base(testMethod, testMethodArguments: null)
        {
        }
    }
}
