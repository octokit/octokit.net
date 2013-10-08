using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
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
                var getUri = new Uri("/anything", UriKind.Relative);
                IResponse<object> response = new ApiResponse<object> { BodyAsObject = new object() };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Args.Uri, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.Get(getUri);

                Assert.Same(response.BodyAsObject, data);
                connection.Received().GetAsync<object>(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection<object>(Substitute.For<IConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null));
            }
        }

        public class TheGetItemMethod
        {
            [Fact]
            public async Task MakesGetRequestForItem()
            {
                var getUri = new Uri("/anything", UriKind.Relative);
                IResponse<object> response = new ApiResponse<object> { BodyAsObject = new object() };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Args.Uri, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.GetItem<object>(getUri, null);

                Assert.Same(response.BodyAsObject, data);
                connection.Received().GetAsync<object>(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection<object>(Substitute.For<IConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.GetItem<object>(null, null));
            }
        }

        public class TheGetHtmlMethod
        {
            [Fact]
            public async Task MakesHtmlRequest()
            {
                var getUri = new Uri("/anything", UriKind.Relative);
                IResponse<string> response = new ApiResponse<string> { Body = "<html />" };
                var connection = Substitute.For<IConnection>();
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.GetHtml(getUri);

                Assert.Same("<html />", data);
                connection.Received().GetHtml(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection<object>(Substitute.For<IConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetHtml(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task MakesGetRequestForAllItems()
            {
                var getAllUri = new Uri("/anything", UriKind.Relative);
                var links = new Dictionary<string, Uri>();
                var scopes = new List<string>();
                IResponse<List<object>> response = new ApiResponse<List<object>>
                {
                    ApiInfo = new ApiInfo(links, scopes, scopes, "etag", 1, 1),
                    BodyAsObject = new List<object> { new object(), new object() }
                };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<List<object>>(Args.Uri, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.GetAll(getAllUri);

                Assert.Equal(2, data.Count);
                connection.Received().GetAsync<List<object>>(getAllUri, null);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection<object>(Substitute.For<IConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task MakesPatchRequestWithSuppliedData()
            {
                var patchUri = new Uri("/anything", UriKind.Relative);
                var sentData = new object();
                IResponse<object> response = new ApiResponse<object> { BodyAsObject = new object() };
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<object>(Args.Uri, Args.Object).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.Update(patchUri, sentData);

                Assert.Same(data, response.BodyAsObject);
                connection.Received().PatchAsync<object>(patchUri, sentData);

            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection<object>(Substitute.For<IConnection>());
                var patchUri = new Uri("/", UriKind.Relative);
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Update(null, new object()));
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Update(patchUri, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task MakesPostRequestWithSuppliedData()
            {
                var postUri = new Uri("/anything", UriKind.Relative);
                var sentData = new object();
                IResponse<object> response = new ApiResponse<object> { BodyAsObject = new object() };
                var connection = Substitute.For<IConnection>();
                connection.PostAsync<object>(Args.Uri, Args.Object).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.Create(postUri, sentData);

                Assert.Same(data, response.BodyAsObject);
                connection.Received().PostAsync<object>(postUri, sentData);

            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection<object>(Substitute.For<IConnection>());
                var postUri = new Uri("/", UriKind.Relative);
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, new object()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(postUri, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task MakesDeleteRequest()
            {
                var deleteUri = new Uri("/anything", UriKind.Relative);
                IResponse<object> response = new ApiResponse<object> { BodyAsObject = new object() };
                var connection = Substitute.For<IConnection>();
                connection.DeleteAsync<object>(Args.Uri).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                await apiConnection.Delete(deleteUri);

                connection.Received().DeleteAsync<object>(deleteUri);

            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection<object>(Substitute.For<IConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Delete(null));
            }
        }

        public class TheUploadMethod
        {
            [Fact]
            public async Task MakesUploadRequest()
            {
                var uploadUrl = new Uri("/anything", UriKind.Relative);
                IResponse<string> response = new ApiResponse<string> { BodyAsObject = "the response" };
                var connection = Substitute.For<IConnection>();
                connection.PostAsync<string>(Args.Uri, Arg.Any<Stream>(), Args.String, Args.String)
                    .Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<Boolean>(connection);
                var rawData = new MemoryStream();

                await apiConnection.Upload<string>(uploadUrl, rawData, "B");

                connection.Received().PostAsync<string>(uploadUrl, rawData, Args.String, Args.String);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection<object>(Substitute.For<IConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Upload<object>(null, Stream.Null, "some-content-type"));
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Upload<object>(new Uri("/ok", UriKind.Relative), null, "some-content-type"));
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Upload<object>(new Uri("/ok", UriKind.Relative), null, null));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ApiConnection<object>(null));
            }
        }
    }
}
