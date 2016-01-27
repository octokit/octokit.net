using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Http
{
    public class ApiConnectionTests
    {
        public class TheGetMethod
        {
            [Fact]
            public async Task MakesGetRequestForItem()
            {
                var getUri = new Uri("anything", UriKind.Relative);
                IApiResponse<object> response = new ApiResponse<object>(new Response());
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Args.Uri, null, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Get<object>(getUri);

                Assert.Same(response.Body, data);
                connection.Received().GetResponse<object>(getUri);
            }

            [Fact]
            public async Task MakesGetRequestForItemWithAcceptsOverride()
            {
                var getUri = new Uri("anything", UriKind.Relative);
                const string accepts = "custom/accepts";
                IApiResponse<object> response = new ApiResponse<object>(new Response());
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Args.Uri, null, Args.String).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Get<object>(getUri, null, accepts);

                Assert.Same(response.Body, data);
                connection.Received().Get<object>(getUri, null, accepts);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var getUri = new Uri("anything", UriKind.Relative);
                var client = new ApiConnection(Substitute.For<IConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get<object>(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get<object>(getUri, new Dictionary<string, string>(), null));
            }
        }

        public class TheGetHtmlMethod
        {
            [Fact]
            public async Task MakesHtmlRequest()
            {
                var getUri = new Uri("anything", UriKind.Relative);
                IApiResponse<string> response = new ApiResponse<string>(new Response(), "<html />");
                var connection = Substitute.For<IConnection>();
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.GetHtml(getUri);

                Assert.Equal("<html />", data);
                connection.Received().GetHtml(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection(Substitute.For<IConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetHtml(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task MakesGetRequestForAllItems()
            {
                var getAllUri = new Uri("anything", UriKind.Relative);
                IApiResponse<List<object>> response = new ApiResponse<List<object>>(
                    new Response(),
                    new List<object> { new object(), new object() });
                var connection = Substitute.For<IConnection>();
                connection.Get<List<object>>(Args.Uri, null, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.GetAll<object>(getAllUri);

                Assert.Equal(2, data.Count);
                connection.Received().Get<List<object>>(getAllUri, null, null);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection(Substitute.For<IConnection>());

                // One argument
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll<object>(null));

                // Two argument
                await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await client.GetAll<object>(null, new Dictionary<string, string>()));

                // Three arguments
                await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await client.GetAll<object>(null, new Dictionary<string, string>(), "accepts"));
            }
        }

        public class ThePatchMethod
        {
            [Fact]
            public async Task MakesPatchRequestWithSuppliedData()
            {
                var patchUri = new Uri("anything", UriKind.Relative);
                var sentData = new object();
                IApiResponse<object> response = new ApiResponse<object>(new Response(), new object());
                var connection = Substitute.For<IConnection>();
                connection.Patch<object>(Args.Uri, Args.Object).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Patch<object>(patchUri, sentData);

                Assert.Same(data, response.Body);
                connection.Received().Patch<object>(patchUri, sentData);
            }

            [Fact]
            public async Task MakesPatchRequestWithAcceptsOverride()
            {
                var patchUri = new Uri("anything", UriKind.Relative);
                var sentData = new object();
                var accepts = "custom/accepts";
                IApiResponse<object> response = new ApiResponse<object>(new Response());
                var connection = Substitute.For<IConnection>();
                connection.Patch<object>(Args.Uri, Args.Object, Args.String).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Patch<object>(patchUri, sentData, accepts);

                Assert.Same(data, response.Body);
                connection.Received().Patch<object>(patchUri, sentData, accepts);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection(Substitute.For<IConnection>());
                var patchUri = new Uri("", UriKind.Relative);
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Patch<object>(null, new object()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Patch<object>(patchUri, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Patch<object>(patchUri, new object(), null));
            }
        }

        public class ThePostMethod
        {
            [Fact]
            public async Task MakesPostRequestWithoutData()
            {
                var postUri = new Uri("anything", UriKind.Relative);
                var statusCode = HttpStatusCode.Accepted;
                var connection = Substitute.For<IConnection>();
                connection.Post(Args.Uri).Returns(Task.FromResult(statusCode));
                var apiConnection = new ApiConnection(connection);

                await apiConnection.Post(postUri);

                connection.Received().Post(postUri);
            }

            [Fact]
            public async Task MakesPostRequestWithSuppliedData()
            {
                var postUri = new Uri("anything", UriKind.Relative);
                var sentData = new object();
                IApiResponse<object> response = new ApiResponse<object>(new Response(), new object());
                var connection = Substitute.For<IConnection>();
                connection.Post<object>(Args.Uri, Args.Object, null, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Post<object>(postUri, sentData);

                Assert.Same(data, response.Body);
                connection.Received().Post<object>(postUri, sentData, null, null);
            }

            [Fact]
            public async Task MakesUploadRequest()
            {
                var uploadUrl = new Uri("anything", UriKind.Relative);
                IApiResponse<string> response = new ApiResponse<string>(new Response(), "the response");
                var connection = Substitute.For<IConnection>();
                connection.Post<string>(Args.Uri, Arg.Any<Stream>(), Args.String, Args.String)
                    .Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);
                var rawData = new MemoryStream();

                await apiConnection.Post<string>(uploadUrl, rawData, "accepts", "content-type");

                connection.Received().Post<string>(uploadUrl, rawData, "accepts", "content-type");
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var postUri = new Uri("", UriKind.Relative);
                var connection = new ApiConnection(Substitute.For<IConnection>());

                // 1 parameter overload
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Post(null));

                // 2 parameter overload
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Post<object>(null, new object()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Post<object>(postUri, null));

                // 3 parameters
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Post<object>(null, new MemoryStream(), "anAccept", "some-content-type"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Post<object>(postUri, null, "anAccept", "some-content-type"));
            }
        }

        public class ThePutMethod
        {
            [Fact]
            public async Task MakesPutRequestWithSuppliedData()
            {
                var putUri = new Uri("anything", UriKind.Relative);
                var sentData = new object();
                IApiResponse<object> response = new ApiResponse<object>(new Response());
                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Args.Uri, Args.Object).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Put<object>(putUri, sentData);

                Assert.Same(data, response.Body);
                connection.Received().Put<object>(putUri, sentData);
            }

            [Fact]
            public async Task MakesPutRequestWithSuppliedDataAndTwoFactorCode()
            {
                var putUri = new Uri("anything", UriKind.Relative);
                var sentData = new object();
                IApiResponse<object> response = new ApiResponse<object>(new Response());
                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Args.Uri, Args.Object, "two-factor").Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Put<object>(putUri, sentData, "two-factor");

                Assert.Same(data, response.Body);
                connection.Received().Put<object>(putUri, sentData, "two-factor");
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var putUri = new Uri("", UriKind.Relative);
                var connection = new ApiConnection(Substitute.For<IConnection>());

                // 2 parameter overload
                await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await connection.Put<object>(null, new object()));
                await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await connection.Put<object>(putUri, null));

                // 3 parameters
                await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await connection.Put<object>(null, new MemoryStream(), "two-factor"));
                await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await connection.Put<object>(putUri, null, "two-factor"));
                await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await connection.Put<object>(putUri, new MemoryStream(), null));
                await Assert.ThrowsAsync<ArgumentException>(async () =>
                    await connection.Put<object>(putUri, new MemoryStream(), ""));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task MakesDeleteRequest()
            {
                var deleteUri = new Uri("anything", UriKind.Relative);
                HttpStatusCode statusCode = HttpStatusCode.NoContent;
                var connection = Substitute.For<IConnection>();
                connection.Delete(Args.Uri).Returns(Task.FromResult(statusCode));
                var apiConnection = new ApiConnection(connection);

                await apiConnection.Delete(deleteUri);

                connection.Received().Delete(deleteUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection(Substitute.For<IConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.Delete(null));
            }
        }

        public class TheGetQueuedOperationMethod
        {
            [Fact]
            public async Task WhenGetReturnsNotOkOrAcceptedApiExceptionIsThrown()
            {
                var queuedOperationUrl = new Uri("anything", UriKind.Relative);

                const HttpStatusCode statusCode = HttpStatusCode.PartialContent;
                IApiResponse<object> response = new ApiResponse<object>(new Response(statusCode, null, new Dictionary<string, string>(), "application/json"), new object());
                var connection = Substitute.For<IConnection>();
                connection.GetResponse<object>(queuedOperationUrl, Args.CancellationToken).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                await Assert.ThrowsAsync<ApiException>(() => apiConnection.GetQueuedOperation<object>(queuedOperationUrl, Args.CancellationToken));
            }

            [Fact]
            public async Task WhenGetReturnsOkThenBodyAsObjectIsReturned()
            {
                var queuedOperationUrl = new Uri("anything", UriKind.Relative);

                var result = new[] { new object() };
                const HttpStatusCode statusCode = HttpStatusCode.OK;
                var httpResponse = new Response(statusCode, null, new Dictionary<string, string>(), "application/json");
                IApiResponse<IReadOnlyList<object>> response = new ApiResponse<IReadOnlyList<object>>(httpResponse, result);
                var connection = Substitute.For<IConnection>();
                connection.GetResponse<IReadOnlyList<object>>(queuedOperationUrl, Args.CancellationToken)
                    .Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var actualResult = await apiConnection.GetQueuedOperation<object>(queuedOperationUrl, Args.CancellationToken);
                Assert.Same(actualResult, result);
            }

            [Fact]
            public async Task WhenGetReturnsNoContentThenReturnsEmptyCollection()
            {
                var queuedOperationUrl = new Uri("anything", UriKind.Relative);

                var result = new[] { new object() };
                const HttpStatusCode statusCode = HttpStatusCode.NoContent;
                var httpResponse = new Response(statusCode, null, new Dictionary<string, string>(), "application/json");
                IApiResponse<IReadOnlyList<object>> response = new ApiResponse<IReadOnlyList<object>>(
                    httpResponse, result);
                var connection = Substitute.For<IConnection>();
                connection.GetResponse<IReadOnlyList<object>>(queuedOperationUrl, Args.CancellationToken)
                    .Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var actualResult = await apiConnection.GetQueuedOperation<object>(queuedOperationUrl, Args.CancellationToken);
                Assert.Empty(actualResult);
            }

            [Fact]
            public async Task GetIsRepeatedUntilHttpStatusCodeOkIsReturned()
            {
                var queuedOperationUrl = new Uri("anything", UriKind.Relative);

                var result = new[] { new object() };
                IApiResponse<IReadOnlyList<object>> firstResponse = new ApiResponse<IReadOnlyList<object>>(
                    new Response(HttpStatusCode.Accepted, null, new Dictionary<string, string>(), "application/json"), result);
                IApiResponse<IReadOnlyList<object>> completedResponse = new ApiResponse<IReadOnlyList<object>>(
                    new Response(HttpStatusCode.OK, null, new Dictionary<string, string>(), "application/json"), result);
                var connection = Substitute.For<IConnection>();
                connection.GetResponse<IReadOnlyList<object>>(queuedOperationUrl, Args.CancellationToken)
                          .Returns(x => Task.FromResult(firstResponse),
                          x => Task.FromResult(firstResponse),
                          x => Task.FromResult(completedResponse));

                var apiConnection = new ApiConnection(connection);

                await apiConnection.GetQueuedOperation<object>(queuedOperationUrl, CancellationToken.None);

                connection.Received(3).GetResponse<IReadOnlyList<object>>(queuedOperationUrl, Args.CancellationToken);
            }

            public async Task CanCancelQueuedOperation()
            {
                var queuedOperationUrl = new Uri("anything", UriKind.Relative);

                var result = new[] { new object() };
                IApiResponse<IReadOnlyList<object>> accepted = new ApiResponse<IReadOnlyList<object>>(
                    new Response(HttpStatusCode.Accepted, null, new Dictionary<string, string>(), "application/json"), result);
                var connection = Substitute.For<IConnection>();
                connection.GetResponse<IReadOnlyList<object>>(queuedOperationUrl, Args.CancellationToken)
                    .Returns(x => Task.FromResult(accepted));

                var apiConnection = new ApiConnection(connection);

                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(100);
                var canceled = false;

                var operationResult = await apiConnection.GetQueuedOperation<object>(queuedOperationUrl, cancellationTokenSource.Token)
                    .ContinueWith(task =>
                    {
                        canceled = task.IsCanceled;
                        return task;
                    }, TaskContinuationOptions.OnlyOnCanceled)
                    .ContinueWith(task => task, TaskContinuationOptions.OnlyOnFaulted);

                Assert.True(canceled);
                Assert.Null(operationResult);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection(Substitute.For<IConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => connection.GetQueuedOperation<object>(null, CancellationToken.None));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ApiConnection(null));
            }
        }
    }
}
