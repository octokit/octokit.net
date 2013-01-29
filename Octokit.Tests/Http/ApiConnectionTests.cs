using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octokit.Http;
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
                connection.GetAsync<object>(Args.Uri).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.Get(getUri);

                data.Should().BeSameAs(response.BodyAsObject);
                connection.Received().GetAsync<object>(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection<object>(Substitute.For<IConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null));
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
                connection.GetAsync<object>(Args.Uri).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.GetItem<object>(getUri);

                data.Should().BeSameAs(response.BodyAsObject);
                connection.Received().GetAsync<object>(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection<object>(Substitute.For<IConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await connection.GetItem<object>(null));
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
                connection.GetHtml(Args.Uri).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.GetHtml(getUri);

                data.Should().Be("<html />");
                connection.Received().GetHtml(getUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection<object>(Substitute.For<IConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await client.GetHtml(null));
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
                connection.GetAsync<List<object>>(Args.Uri).Returns(Task.FromResult(response));
                var apiConnection = new ApiConnection<object>(connection);

                var data = await apiConnection.GetAll(getAllUri);

                data.Count.Should().Be(2);
                connection.Received().GetAsync<List<object>>(getAllUri);
            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection<object>(Substitute.For<IConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null));
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

                data.Should().BeSameAs(response.BodyAsObject);
                connection.Received().PatchAsync<object>(patchUri, sentData);

            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var connection = new ApiConnection<object>(Substitute.For<IConnection>());
                var patchUri = new Uri("/", UriKind.Relative);
                AssertEx.Throws<ArgumentNullException>(async () => await connection.Update(null, new object()));
                AssertEx.Throws<ArgumentNullException>(async () => await connection.Update(patchUri, null));
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

                data.Should().BeSameAs(response.BodyAsObject);
                connection.Received().PostAsync<object>(postUri, sentData);

            }

            [Fact]
            public async Task EnsuresArgumentNotNull()
            {
                var client = new ApiConnection<object>(Substitute.For<IConnection>());
                var postUri = new Uri("/", UriKind.Relative);
                AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, new object()));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Create(postUri, null));
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
                AssertEx.Throws<ArgumentNullException>(async () => await connection.Delete(null));
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
