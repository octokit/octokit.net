using System;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseLicenseClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseLicenseClient(connection);

                string expectedUri = "enterprise/settings/license";
                client.Get();
                connection.Received().Get<LicenseInfo>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }
    }
}
