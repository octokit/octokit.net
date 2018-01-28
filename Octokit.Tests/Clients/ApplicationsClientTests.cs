using System;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ApplicationClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ApplicationsClient(null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void GetFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ApplicationsClient(connection);

                client.Create();

                connection.Received().Get<Application>(Arg.Is<Uri>(u => u.ToString() == "app"));
            }
        }

    }
}
