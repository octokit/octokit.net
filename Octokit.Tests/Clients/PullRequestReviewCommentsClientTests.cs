using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests;
using Xunit;

public class PullRequestReviewCommentsClientTests
{
    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new PullRequestReviewCommentsClient(null));
        }

        [Fact]
        public void PullRequestReviewCommentCreateEnsuresArgumentsValue()
        {
            string body = "body";
            string commitId = "sha";
            string path = "path";
            int position = 1;

            var comment = new PullRequestReviewCommentCreate(body, commitId, path, position);

            Assert.Equal(body, comment.Body);
            Assert.Equal(commitId, comment.CommitId);
            Assert.Equal(path, comment.Path);
            Assert.Equal(position, comment.Position);
        }

        [Fact]
        public void PullRequestReviewCommentCreateEnsuresNonNullArguments()
        {
            string body = "body";
            string commitId = "sha";
            string path = "path";
            int position = 1;

            Assert.Throws<ArgumentNullException>(() => new PullRequestReviewCommentCreate(null, commitId, path, position));
            Assert.Throws<ArgumentException>(() => new PullRequestReviewCommentCreate("", commitId, path, position));
            Assert.Throws<ArgumentNullException>(() => new PullRequestReviewCommentCreate(body, null, path, position));
            Assert.Throws<ArgumentException>(() => new PullRequestReviewCommentCreate(body, "", path, position));
            Assert.Throws<ArgumentNullException>(() => new PullRequestReviewCommentCreate(body, commitId, null, position));
            Assert.Throws<ArgumentException>(() => new PullRequestReviewCommentCreate(body, commitId, "", position));
        }

        [Fact]
        public void PullRequestReviewCommentEditEnsuresArgumentsValue()
        {
            string body = "body";

            var comment = new PullRequestReviewCommentEdit(body);

            Assert.Equal(body, comment.Body);
        }

        [Fact]
        public void PullRequestReviewCommentEditEnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new PullRequestReviewCommentEdit(null));
            Assert.Throws<ArgumentException>(() => new PullRequestReviewCommentEdit(""));
        }

        [Fact]
        public void PullRequestReviewCommentReplyCreateEnsuresArgumentsValue()
        {
            string body = "body";
            int inReplyTo = 1;

            var comment = new PullRequestReviewCommentReplyCreate(body, inReplyTo);

            Assert.Equal(body, comment.Body);
            Assert.Equal(inReplyTo, comment.InReplyTo);
        }

        [Fact]
        public void PullRequestReviewCommentReplyCreateEnsuresNonNullArguments()
        {
            int inReplyTo = 1;

            Assert.Throws<ArgumentNullException>(() => new PullRequestReviewCommentReplyCreate(null, inReplyTo));
            Assert.Throws<ArgumentException>(() => new PullRequestReviewCommentReplyCreate("", inReplyTo));
        }
    }

    public class TheGetForPullRequestMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await client.GetAll("owner", "name", 7);

            connection.Received().GetAll<PullRequestReviewComment>(
                Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/7/comments"),
                Arg.Any<Dictionary<string, string>>(), Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await client.GetAll(1, 7);

            connection.Received().GetAll<PullRequestReviewComment>(
                Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/7/comments"), Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var options = new ApiOptions
            {
                StartPage = 1,
                PageCount = 1,
                PageSize = 1
            };

            await client.GetAll("owner", "name", 7, options);

            connection.Received().GetAll<PullRequestReviewComment>(
                Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/7/comments"),
                Arg.Any<Dictionary<string, string>>(), options);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var options = new ApiOptions
            {
                StartPage = 1,
                PageCount = 1,
                PageSize = 1
            };

            await client.GetAll(1, 7, options);

            connection.Received().GetAll<PullRequestReviewComment>(
                Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/7/comments"), options);
        }

        [Fact]
        public async Task EnsuresNotNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", 1, null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, 1, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1, ApiOptions.None));
        }
    }

    public class TheGetForRepositoryMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var request = new PullRequestReviewCommentRequest
            {
                Direction = SortDirection.Descending,
                Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                Sort = PullRequestReviewCommentSort.Updated
            };

            await client.GetAllForRepository("owner", "name", request);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"),
                Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var request = new PullRequestReviewCommentRequest
            {
                Direction = SortDirection.Descending,
                Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                Sort = PullRequestReviewCommentSort.Updated
            };

            await client.GetAllForRepository(1, request);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"),
                Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var request = new PullRequestReviewCommentRequest
            {
                Direction = SortDirection.Descending,
                Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                Sort = PullRequestReviewCommentSort.Updated
            };

            var options = new ApiOptions
            {
                PageCount = 1,
                StartPage = 1,
                PageSize = 1
            };

            await client.GetAllForRepository("owner", "name", request, options);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"),
                options);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var request = new PullRequestReviewCommentRequest
            {
                Direction = SortDirection.Descending,
                Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                Sort = PullRequestReviewCommentSort.Updated
            };

            var options = new ApiOptions
            {
                PageCount = 1,
                StartPage = 1,
                PageSize = 1
            };

            await client.GetAllForRepository(1, request, options);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"),
                options);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithoutSelectedSortingArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await client.GetAllForRepository("owner", "name");

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"),
                Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithoutSelectedSortingArgumentsWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await client.GetAllForRepository(1);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"),
                Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithoutSelectedSortingArgumentsWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var options = new ApiOptions
            {
                PageCount = 1,
                StartPage = 1,
                PageSize = 1
            };

            await client.GetAllForRepository("owner", "name", options);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"),
                options);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithoutSelectedSortingArgumentsWithApiOptionsWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var options = new ApiOptions
            {
                PageCount = 1,
                StartPage = 1,
                PageSize = 1
            };

            await client.GetAllForRepository(1, options);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"),
                options);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new PullRequestReviewCommentsClient(Substitute.For<IApiConnection>());

            var request = new PullRequestReviewCommentRequest();

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (PullRequestReviewCommentRequest)null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", request, null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (PullRequestReviewCommentRequest)null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, request, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request, ApiOptions.None));
        }

        [Fact]
        public async Task EnsuresDefaultValues()
        {
            var request = new PullRequestReviewCommentRequest();

            Assert.Equal(SortDirection.Ascending, request.Direction);
            Assert.Null(request.Since);
            Assert.Equal(PullRequestReviewCommentSort.Created, request.Sort);
        }
    }

    public class TheGetCommentMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            client.GetComment("owner", "name", 53);

            connection.Received().Get<PullRequestReviewComment>(
                Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/comments/53"),
                Arg.Any<Dictionary<string, string>>());
        }

        [Fact]
        public void RequestsCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            client.GetComment(1, 53);

            connection.Received().Get<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/comments/53"));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new PullRequestReviewCommentsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetComment(null, "name", 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetComment("owner", null, 1));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetComment("", "name", 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetComment("owner", "", 1));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var comment = new PullRequestReviewCommentCreate("Comment content", "qe3dsdsf6", "file.css", 7);

            client.Create("fakeOwner", "fakeRepoName", 13, comment);

            connection.Connection.Received().Post<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/13/comments"),
                comment, null, null);
        }

        [Fact]
        public void PostsToCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var comment = new PullRequestReviewCommentCreate("Comment content", "qe3dsdsf6", "file.css", 7);

            client.Create(1, 13, comment);

            connection.Connection.Received().Post<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/13/comments"),
                comment, null, null);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            string body = "Comment content";
            string commitId = "qe3dsdsf6";
            string path = "file.css";
            int position = 7;

            var comment = new PullRequestReviewCommentCreate(body, commitId, path, position);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "fakeRepoName", 1, comment));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fakeOwner", null, 1, comment));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fakeOwner", "fakeRepoName", 1, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "fakeRepoName", 1, comment));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("fakeOwner", "", 1, comment));
        }
    }

    public class TheCreateReplyMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var comment = new PullRequestReviewCommentReplyCreate("Comment content", 5);

            client.CreateReply("owner", "name", 13, comment);

            connection.Connection.Received().Post<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/13/comments"),
                comment, null, null);
        }

        [Fact]
        public void PostsToCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var comment = new PullRequestReviewCommentReplyCreate("Comment content", 5);

            client.CreateReply(1, 13, comment);

            connection.Connection.Received().Post<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/13/comments"),
                comment, null, null);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            string body = "Comment content";
            int inReplyTo = 7;

            var comment = new PullRequestReviewCommentReplyCreate(body, inReplyTo);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateReply(null, "name", 1, comment));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateReply("owner", null, 1, comment));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateReply("owner", "name", 1, null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateReply(1, 1, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.CreateReply("", "name", 1, comment));
            await Assert.ThrowsAsync<ArgumentException>(() => client.CreateReply("owner", "", 1, comment));
        }
    }

    public class TheEditMethod
    {
        [Fact]
        public async Task PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var comment = new PullRequestReviewCommentEdit("New comment content");

            await client.Edit("owner", "name", 13, comment);

            connection.Received().Patch<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/comments/13"), comment);
        }

        [Fact]
        public async Task PostsToCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var comment = new PullRequestReviewCommentEdit("New comment content");

            await client.Edit(1, 13, comment);

            connection.Received().Patch<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/comments/13"), comment);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var body = "New comment content";

            var comment = new PullRequestReviewCommentEdit(body);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(null, "name", 1, comment));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", null, 1, comment));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", "name", 1, null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(1, 1, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("", "name", 1, comment));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("owner", "", 1, comment));
        }
    }

    public class TheDeleteMethod
    {
        [Fact]
        public async Task PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await client.Delete("owner", "name", 13);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/comments/13"));
        }

        [Fact]
        public async Task PostsToCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await client.Delete(1, 13);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/comments/13"));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 1));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 1));
        }
    }
}
