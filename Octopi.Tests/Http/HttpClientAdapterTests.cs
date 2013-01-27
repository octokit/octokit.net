using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests.Http
{
    public class HttpClientAdapterTests
    {
        public class TheBuildRequestMessageMethod
        {
            [Fact]
            public void AddsHeadersToRequestMessage()
            {
                var request = new Request
                {
                    Method = HttpMethod.Post,
                    Headers =
                    {
                        { "foo", "bar" },
                        { "blah", "blase" }
                    }
                };
                var tester = new HttpClientAdapterTester();
                
                var responseMessage = tester.BuildRequestMessageTester(request);
                
                responseMessage.Headers.Count().Should().Be(2);
                var firstHeader = responseMessage.Headers.First();
                firstHeader.Key.Should().Be("foo");
                firstHeader.Value.First().Should().Be("bar");
                var lastHeader = responseMessage.Headers.Last();
                lastHeader.Key.Should().Be("blah");
                lastHeader.Value.First().Should().Be("blase");
            }

            [Fact]
            public void EnsuresArguments()
            {
                var tester = new HttpClientAdapterTester();
                Assert.Throws<ArgumentNullException>(() => tester.BuildRequestMessageTester(null));
            }
        }

        public class TheBuildResponseMethod
        {
            [Fact]
            public async Task BuildsResponseFromResponseMessage()
            {
                var responseMessage = new HttpResponseMessage {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(Encoding.UTF8.GetBytes("{}")),
                    Headers =
                    {
                        {"peanut", "butter"},
                        {"ele", "phant"}
                    }
                };
                var tester = new HttpClientAdapterTester();

                var response = await tester.BuildResponseTester<string>(responseMessage);
                
                var firstHeader = response.Headers.First();
                firstHeader.Key.Should().Be("peanut");
                firstHeader.Value.Should().Be("butter");
                var lastHeader = response.Headers.Last();
                lastHeader.Key.Should().Be("ele");
                lastHeader.Value.Should().Be("phant");
            }
        }

        sealed class HttpClientAdapterTester : HttpClientAdapter
        {
            public HttpRequestMessage BuildRequestMessageTester(IRequest request)
            {
                return BuildRequestMessage(request);
            }

            public async Task<IResponse<T>> BuildResponseTester<T>(HttpResponseMessage responseMessage)
            {
                return await BuildResponse<T>(responseMessage);
            }

        }


    }

    public class TheSendMethod
    {
        [Fact]
        public void EnsuresRequestNotNull()
        {
        }
    }
}
