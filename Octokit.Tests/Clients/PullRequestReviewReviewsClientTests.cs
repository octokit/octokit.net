﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests;
using Xunit;

public class PullRequestReviewsClientTests
{
    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new PullRequestReviewCommentsClient(null));
        }

        [Fact]
        public void PullRequestReviewCreateEnsuresArgumentsValue()
        {
            string body = "body";
            string commitId = "sha";
            string path = "path";
            string evt = "event";
            int position = 1;

            var comment = new PullRequestReviewCommentCreate(body, commitId, path, position);

            var review = new PullRequestReviewCreate()
            {
                CommitId = commitId,
                Body = body,
                Event = evt
            };

            review.Comments.Add(comment);


            Assert.Equal(body, review.Body);
            Assert.Equal(commitId, review.CommitId);
            ReleaseAsset.Equals(evt, review.Event);
            Assert.NotEmpty(review.Comments);
        }
    }

    public class TheGetForPullRequestMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            await client.GetAll("owner", "name", 7);

            connection.Received().GetAll<PullRequestReview>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/7/reviews"), null, Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            await client.GetAll(1, 7);

            connection.Received().GetAll<PullRequestReview>(
                Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/7/reviews"), Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            var options = new ApiOptions
            {
                StartPage = 1,
                PageCount = 1,
                PageSize = 1
            };

            await client.GetAll("owner", "name", 7, options);

            connection.Received().GetAll<PullRequestReview>(
                Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/7/reviews"), null, options);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            var options = new ApiOptions
            {
                StartPage = 1,
                PageCount = 1,
                PageSize = 1
            };

            await client.GetAll(1, 7, options);

            connection.Received().GetAll<PullRequestReview>(
                Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/7/reviews"), options);
        }

        [Fact]
        public async Task EnsuresNotNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

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

    public class TheGetReviewMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            client.GetReview("owner", "name", 53, 2);

            connection.Received().Get<PullRequestReview>(
                Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/53/reviews/2"));
        }

        [Fact]
        public void RequestsCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            client.GetReview(1, 53, 2);

            connection.Received().Get<PullRequestReview>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/53/reviews/2"));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new PullRequestReviewClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReview(null, "name", 1, 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReview("owner", null, 1, 1));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetReview("", "name", 1, 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetReview("owner", "", 1, 1));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);
            
            var comment = new PullRequestReviewCommentCreate("Comment content", "qe3dsdsf6", "file.css", 7);

            var review = new PullRequestReviewCreate()
            {
                CommitId = "commit",
                Body = "body",
                Event = "event"
            };

            review.Comments.Add(comment);
            client.Create("fakeOwner", "fakeRepoName", 13, review);

            connection.Connection.Received().Post<PullRequestReview>(Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/13/reviews"),
                review, null, null);
        }

        [Fact]
        public void PostsToCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            var comment = new PullRequestReviewCommentCreate("Comment content", "qe3dsdsf6", "file.css", 7);

            var review = new PullRequestReviewCreate()
            {
                CommitId = "commit",
                Body = "body",
                Event = "event"
            };

            review.Comments.Add(comment);

            client.Create(1, 13, review);

            connection.Connection.Received().Post<PullRequestReview>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/13/reviews"),
                review, null, null);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            string body = "Comment content";
            string commitId = "qe3dsdsf6";
            string path = "file.css";
            int position = 7;

            var comment = new PullRequestReviewCommentCreate(body, commitId, path, position);


            var review = new PullRequestReviewCreate()
            {
                CommitId = "commit",
                Body = "body",
                Event = "event"
            };

            review.Comments.Add(comment);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "fakeRepoName", 1, review));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fakeOwner", null, 1, review));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fakeOwner", "fakeRepoName", 1, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "fakeRepoName", 1, review));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("fakeOwner", "", 1, review));
        }
    }
    
    public class TheDeleteMethod
    {
        [Fact]
        public async Task PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            await client.Delete("owner", "name", 13, 13);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/13/reviews/13"));
        }

        [Fact]
        public async Task PostsToCorrectUrlWithRepositoryId()
        {

            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            await client.Delete(1, 13, 13);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/13/reviews/13"));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 1, 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 1, 1));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 1, 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 1, 1));
        }
    }

    public class TheDismissMethod
    {
        [Fact]
        public async Task PostsToCorrectUrl()
        {

            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            var dismissMessage = new PullRequestReviewDismiss()
            {
                Message = "test message"
            };
            await client.Dismiss("owner", "name", 13, 13, dismissMessage);

            connection.Received().Put<PullRequestReview>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/13/reviews/13/dismissals"), dismissMessage);
        }

        [Fact]
        public async Task PostsToCorrectUrlWithRepositoryId()
        {

            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            var dismissMessage = new PullRequestReviewDismiss()
            {
                Message = "test message"
            };
            await client.Dismiss(1, 13, 13, dismissMessage);

            connection.Received().Put<PullRequestReview>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/13/reviews/13/dismissals"), dismissMessage);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            var dismissMessage = new PullRequestReviewDismiss()
            {
                Message = "test message"
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Dismiss(null, "name", 1, 1, dismissMessage));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Dismiss("owner", null, 1, 1, dismissMessage));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Dismiss("owner", "name", 1, 1, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Dismiss("", "name", 1, 1, dismissMessage));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Dismiss("owner", "", 1, 1, dismissMessage));
        }
    }

    public class TheGetCommentsMethod
    {
        [Fact]
        public async Task GetsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);
            
            await client.GetAllComments("owner", "name", 13, 13);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/13/reviews/13/comments"));
        }

        [Fact]
        public async Task PostsToCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            await client.GetAllComments(1, 13, 13);

            connection.Received().GetAll<PullRequestReviewComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/13/reviews/13/comments"));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);
            
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllComments(null, "name", 1, 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllComments("owner", null, 1, 1));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllComments("", "name", 1, 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllComments("owner", "", 1, 1));
        }
    }

    public class TheSubmitMethod
    {
        [Fact]
        public async Task PostsToCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            var submitMessage = new PullRequestReviewSubmit()
            {
                Body = "string", 
                Event = PullRequestReviewSubmitEvents.APPROVE
            };
            await client.Submit("owner", "name", 13, 13, submitMessage);

            connection.Received().Post<PullRequestReview>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/13/reviews/13/events"), submitMessage, null, null);
        }

        [Fact]
        public async Task PostsToCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);
            var submitMessage = new PullRequestReviewSubmit()
            {
                Body = "string",
                Event = PullRequestReviewSubmitEvents.APPROVE
            };
            await client.Submit(1, 13, 13, submitMessage);

            connection.Received().Post<PullRequestReview>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/13/reviews/13/events"), submitMessage, null, null);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new PullRequestReviewClient(connection);

            var submitMessage = new PullRequestReviewSubmit()
            {
                Body = "string",
                Event = PullRequestReviewSubmitEvents.APPROVE
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Submit(null, "name", 1, 1, submitMessage));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Submit("owner", null, 1, 1, submitMessage));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Submit("owner", "name", 1, 1, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Submit("", "name", 1, 1, submitMessage));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Submit("owner", "", 1, 1, submitMessage));
        }
    }
}
