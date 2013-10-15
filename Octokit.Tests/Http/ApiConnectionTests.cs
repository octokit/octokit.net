﻿using System;
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
                IResponse<object> response = new ApiResponse<object> {BodyAsObject = new object()};
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Args.Uri, null, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Get<object>(getUri);

                Assert.Same(response.BodyAsObject, data);
                connection.Received().GetAsync<object>(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection(Substitute.For<IConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get<object>(null));
            }
        }

        public class TheGetHtmlMethod
        {
            [Fact]
            public async Task MakesHtmlRequest()
            {
                var getUri = new Uri("/anything", UriKind.Relative);
                IResponse<string> response = new ApiResponse<string> {Body = "<html />"};
                var connection = Substitute.For<IConnection>();
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.GetHtml(getUri);

                Assert.Same("<html />", data);
                connection.Received().GetHtml(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection(Substitute.For<IConnection>());
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
                    BodyAsObject = new List<object> {new object(), new object()}
                };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<List<object>>(Args.Uri, null, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.GetAll<object>(getAllUri);

                Assert.Equal(2, data.Count);
                connection.Received().GetAsync<List<object>>(getAllUri, null, null);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection(Substitute.For<IConnection>());
                
                // One argument
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll<object>(null));
                
                // Two argument
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.GetAll<object>(null, new Dictionary<string, string>()));

                // Three arguments
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.GetAll<object>(null, new Dictionary<string, string>(), "accepts"));
            }
        }

        public class ThePatchMethod
        {
            [Fact]
            public async Task MakesPatchRequestWithSuppliedData()
            {
                var patchUri = new Uri("/anything", UriKind.Relative);
                var sentData = new object();
                IResponse<object> response = new ApiResponse<object> {BodyAsObject = new object()};
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<object>(Args.Uri, Args.Object).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Patch<object>(patchUri, sentData);

                Assert.Same(data, response.BodyAsObject);
                connection.Received().PatchAsync<object>(patchUri, sentData);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection(Substitute.For<IConnection>());
                var patchUri = new Uri("/", UriKind.Relative);
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Patch<object>(null, new object()));
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Patch<object>(patchUri, null));
            }
        }

        public class ThePostMethod
        {
            [Fact]
            public async Task MakesPostRequestWithSuppliedData()
            {
                var postUri = new Uri("/anything", UriKind.Relative);
                var sentData = new object();
                IResponse<object> response = new ApiResponse<object> {BodyAsObject = new object()};
                var connection = Substitute.For<IConnection>();
                connection.PostAsync<object>(Args.Uri, Args.Object, null, null).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Post<object>(postUri, sentData);

                Assert.Same(data, response.BodyAsObject);
                connection.Received().PostAsync<object>(postUri, sentData, null, null);
            }

            [Fact]
            public async Task MakesUploadRequest()
            {
                var uploadUrl = new Uri("/anything", UriKind.Relative);
                IResponse<string> response = new ApiResponse<string> {BodyAsObject = "the response"};
                var connection = Substitute.For<IConnection>();
                connection.PostAsync<string>(Args.Uri, Arg.Any<Stream>(), Args.String, Args.String)
                    .Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);
                var rawData = new MemoryStream();

                await apiConnection.Post<string>(uploadUrl, rawData, "accepts", "content-type");

                connection.Received().PostAsync<string>(uploadUrl, rawData, "accepts", "content-type");
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var postUri = new Uri("/", UriKind.Relative);
                var connection = new ApiConnection(Substitute.For<IConnection>());

                // 2 parameter overload
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Post<object>(null, new object()));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Post<object>(postUri, null));

                // 3 parameters
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Post<object>(null, new MemoryStream(), "anAccept", "some-content-type"));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Post<object>(postUri, null, "anAccept", "some-content-type"));
            }
        }

        public class ThePutMethod
        {
            [Fact]
            public async Task MakesPutRequestWithSuppliedData()
            {
                var putUri = new Uri("/anything", UriKind.Relative);
                var sentData = new object();
                IResponse<object> response = new ApiResponse<object> { BodyAsObject = new object() };
                var connection = Substitute.For<IConnection>();
                connection.PutAsync<object>(Args.Uri, Args.Object).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Put<object>(putUri, sentData);

                Assert.Same(data, response.BodyAsObject);
                connection.Received().PutAsync<object>(putUri, sentData);
            }

            [Fact]
            public async Task MakesPutRequestWithSuppliedDataAndTwoFactorCode()
            {
                var putUri = new Uri("/anything", UriKind.Relative);
                var sentData = new object();
                IResponse<object> response = new ApiResponse<object> { BodyAsObject = new object() };
                var connection = Substitute.For<IConnection>();
                connection.PutAsync<object>(Args.Uri, Args.Object, "two-factor").Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                var data = await apiConnection.Put<object>(putUri, sentData, "two-factor");

                Assert.Same(data, response.BodyAsObject);
                connection.Received().PutAsync<object>(putUri, sentData, "two-factor");
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var putUri = new Uri("/", UriKind.Relative);
                var connection = new ApiConnection(Substitute.For<IConnection>());

                // 2 parameter overload
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Put<object>(null, new object()));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Put<object>(putUri, null));

                // 3 parameters
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Put<object>(null, new MemoryStream(), "two-factor"));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Put<object>(putUri, null, "two-factor"));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await connection.Put<object>(putUri, new MemoryStream(), null));
                await AssertEx.Throws<ArgumentException>(async () =>
                    await connection.Put<object>(putUri, new MemoryStream(), ""));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task MakesDeleteRequest()
            {
                var deleteUri = new Uri("/anything", UriKind.Relative);
                IResponse<object> response = new ApiResponse<object> {BodyAsObject = new object()};
                var connection = Substitute.For<IConnection>();
                connection.DeleteAsync(Args.Uri).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection(connection);

                await apiConnection.Delete(deleteUri);

                connection.Received().DeleteAsync(deleteUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection(Substitute.For<IConnection>());
                await AssertEx.Throws<ArgumentNullException>(async () => await connection.Delete(null));
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
