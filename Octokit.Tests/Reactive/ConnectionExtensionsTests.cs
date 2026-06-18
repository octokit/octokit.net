using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Reactive
{
    public class ConnectionExtensionsTests
    {
        public class TheGetAndFlattenAllPagesMethod
        {
            [Fact]
            public async Task PassesCancellationTokenToConnectionGet()
            {
                var cts = new CancellationTokenSource();
                var items = new List<object> { new object() };
                IApiResponse<List<object>> response = new ApiResponse<List<object>>(CreateResponse(HttpStatusCode.OK), items);
                var connection = Substitute.For<IConnection>();
                connection.Get<List<object>>(Arg.Any<Uri>(), Arg.Any<IDictionary<string, string>>(), Arg.Any<string>(), cts.Token)
                    .Returns(Task.FromResult(response));

                await connection.GetAndFlattenAllPages<object>(new Uri("anything", UriKind.Relative), cancellationToken: cts.Token).ToList();

                connection.Received().Get<List<object>>(Arg.Any<Uri>(), Arg.Any<IDictionary<string, string>>(), Arg.Any<string>(), cts.Token);
            }
        }
    }
}
