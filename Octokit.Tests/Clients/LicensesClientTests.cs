using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class LicensesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new LicensesClient(null));
            }
        }

        public class TheGetAllLicensesMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new LicensesClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllLicenses(null));
            }

            [Fact]
            public async Task RequestsTheLicensesEndpoint()
            {
                IReadOnlyList<LicenseMetadata> response = new ReadOnlyCollection<LicenseMetadata>(new List<LicenseMetadata>()
                {
                    new LicenseMetadata("foo1", "node-id-1", "foo2", "something", "http://example.com/foo1",  true),
                    new LicenseMetadata("bar1", "node-id-1", "bar2", "something else", "http://example.com/bar1", false)
                });

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<LicenseMetadata>(Arg.Is<Uri>(u => u.ToString() == "licenses"), Args.ApiOptions)
                    .Returns(Task.FromResult(response));
                var client = new LicensesClient(connection);

                var licenses = await client.GetAllLicenses();

                Assert.Equal(2, licenses.Count);
                Assert.Equal("foo1", licenses[0].Key);
                Assert.Equal("foo2", licenses[0].Name);
                Assert.Equal("http://example.com/foo1", licenses[0].Url);
                Assert.Equal("bar1", licenses[1].Key);
                Assert.Equal("bar2", licenses[1].Name);
                Assert.Equal("http://example.com/bar1", licenses[1].Url);
                connection.Received()
                    .GetAll<LicenseMetadata>(Arg.Is<Uri>(u => u.ToString() == "licenses"), Args.ApiOptions);
            }
        }
    }
}
