﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

public class PullRequestReviewCommentsClientTests
{
    public class TheModelConstructors
    {
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
        public void PullRequestReviewCommentCreateEnsuresArgumentsNotNull()
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
        public void PullRequestReviewCommentEditEnsuresArgumentsNotNull()
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
        public void PullRequestReviewCommentReplyCreateEnsuresArgumentsNotNull()
        {
            int inReplyTo = 1;

            Assert.Throws<ArgumentNullException>(() => new PullRequestReviewCommentReplyCreate(null, inReplyTo));
            Assert.Throws<ArgumentException>(() => new PullRequestReviewCommentReplyCreate("", inReplyTo));
        }
    }

    public class TheGetForPullRequestMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            client.GetForPullRequest("fakeOwner", "fakeRepoName", 7);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/7/comments"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForPullRequest(null, "name", 1));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForPullRequest("", "name", 1));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForPullRequest("owner", null, 1));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForPullRequest("owner", "", 1));
        }
    }

    public class TheGetForRepositoryMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var request = new PullRequestReviewCommentRequest
            {
                Direction = SortDirection.Descending,
                Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                Sort = PullRequestReviewCommentSort.Updated,
            };

            client.GetForRepository("fakeOwner", "fakeRepoName", request);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"));
        }

        [Fact]
        public void RequestsCorrectUrlWithoutSelectedSortingArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            client.GetForRepository("fakeOwner", "fakeRepoName");

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/comments"),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var client = new PullRequestReviewCommentsClient(Substitute.For<IApiConnection>());

            var request = new PullRequestReviewCommentRequest();

            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository(null, "name", request));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("", "name", request));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository("owner", null, request));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("owner", "", request));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository("owner", "name", null));
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

            client.GetComment("fakeOwner", "fakeRepoName", 53);

            connection.Received().Get<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/comments/53"),
                null);
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var client = new PullRequestReviewCommentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetComment(null, "name", 1));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetComment("", "name", 1));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetComment("owner", null, 1));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetComment("owner", "", 1));
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
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            string body = "Comment content";
            string commitId = "qe3dsdsf6";
            string path = "file.css";
            int position = 7;

            var comment = new PullRequestReviewCommentCreate(body, commitId, path, position);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "fakeRepoName", 1, comment));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "fakeRepoName", 1, comment));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("fakeOwner", null, 1, comment));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("fakeOwner", "", 1, comment));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("fakeOwner", "fakeRepoName", 1, null));
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

            client.CreateReply("fakeOwner", "fakeRepoName", 13, comment);

            connection.Connection.Received().Post<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/13/comments"),
                comment, null, null);
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            string body = "Comment content";
            int inReplyTo = 7;

            var comment = new PullRequestReviewCommentReplyCreate(body, inReplyTo);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.CreateReply(null, "fakeRepoName", 1, comment));
            await AssertEx.Throws<ArgumentException>(async () => await client.CreateReply("", "fakeRepoName", 1, comment));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.CreateReply("fakeOwner", null, 1, comment));
            await AssertEx.Throws<ArgumentException>(async () => await client.CreateReply("fakeOwner", "", 1, comment));
            await AssertEx.Throws<ArgumentException>(async () => await client.CreateReply("fakeOwner", "fakeRepoName", 1, null));
        }
    }

    public class TheEditMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var comment = new PullRequestReviewCommentEdit("New comment content");

            client.Edit("fakeOwner", "fakeRepoName", 13, comment);

            connection.Received().Patch<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/comments/13"), comment);
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            var body = "New comment content";

            var comment = new PullRequestReviewCommentEdit(body);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Edit(null, "fakeRepoName", 1, comment));
            await AssertEx.Throws<ArgumentException>(async () => await client.Edit("", "fakeRepoName", 1, comment));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Edit("fakeOwner", null, 1, comment));
            await AssertEx.Throws<ArgumentException>(async () => await client.Edit("fakeOwner", "", 1, comment));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Edit("fakeOwner", null, 1, null));
        }
    }

    public class TheDeleteMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            client.Delete("fakeOwner", "fakeRepoName", 13);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/comments/13"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewCommentsClient(connection);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete(null, "fakeRepoName", 1));
            await AssertEx.Throws<ArgumentException>(async () => await client.Delete("", "fakeRepoName", 1));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("fakeOwner", null, 1));
            await AssertEx.Throws<ArgumentException>(async () => await client.Delete("fakeOwner", "", 1));
        }
    }
}
