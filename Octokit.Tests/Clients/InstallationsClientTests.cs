using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class InstallationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new InstallationsClient(null));
            }

            public class TheGetAllMethod
            {
                [Fact]
                public async Task EnsuresNonNullArguments()
                {
                    var connection = Substitute.For<IApiConnection>();
                    var client = new InstallationsClient(connection);

                    Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                }

                [Fact]
                public async Task RequestsCorrectUrl()
                {
                    var connection = Substitute.For<IApiConnection>();
                    var client = new InstallationsClient(connection);

                    client.GetAll();

                    connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations"), null, AcceptHeaders.MachineManPreview);
                }


                [Fact]
                public void RequestsTheCorrectUrlWithApiOptions()
                {
                    var connection = Substitute.For<IApiConnection>();
                    var client = new InstallationsClient(connection);

                    var options = new ApiOptions
                    {
                        PageSize = 1,
                        PageCount = 1,
                        StartPage = 1
                    };

                    client.GetAll(options);

                    connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations"), null, AcceptHeaders.MachineManPreview, options);
                }

            }

        }
    }
}
