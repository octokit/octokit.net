using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class TagsClientTests
    {
        public class TheCreateMethod
        {
            readonly RepositoryContext context;
            readonly ITagsClient fixture;
            readonly string sha;

            public TheCreateMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                fixture = github.Git.Tag;
                context = github.CreateRepositoryContext("public-repo").Result;

                var blob = new NewBlob
                {
                    Content = "Hello World!",
                    Encoding = EncodingType.Utf8
                };
                var blobResult = github.Git.Blob.Create(context.RepositoryOwner, context.RepositoryName, blob).Result;

                sha = blobResult.Sha;
            }

            [IntegrationTest]
            public async Task CreatesTagForRepository()
            {
                var newTag = new NewTag { Message = "Hello", Type = TaggedType.Blob, Object = sha, Tag = "tag" };

                var tag = await fixture.Create(context.Repository.Id, newTag);

                Assert.Equal(tag.Object.Type, TaggedType.Blob);
                Assert.Equal(tag.Message, "Hello");
                Assert.Equal(tag.Object.Sha, sha);
            }

            [IntegrationTest]
            public async Task CreatesTagForRepositoryWithRepositoryId()
            {
                var newTag = new NewTag { Message = "Hello", Type = TaggedType.Tree, Object = sha, Tag = "tag" };

                var tag = await fixture.Create(context.Repository.Id, newTag);

                Assert.Equal(tag.Object.Type, TaggedType.Blob);
                Assert.Equal(tag.Message, "Hello");
                Assert.Equal(tag.Object.Sha, sha);
            }
        }

        public class TheGetMethod
        {
            readonly RepositoryContext context;
            readonly ITagsClient fixture;
            readonly string sha;

            public TheGetMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                fixture = github.Git.Tag;
                context = github.CreateRepositoryContext("public-repo").Result;

                var blob = new NewBlob
                {
                    Content = "Hello World!",
                    Encoding = EncodingType.Utf8
                };
                var blobResult = github.Git.Blob.Create(context.RepositoryOwner, context.RepositoryName, blob).Result;

                sha = blobResult.Sha;
            }

            [IntegrationTest]
            public async Task CreatesTagForRepository()
            {
                var newTag = new NewTag { Message = "Hello", Type = TaggedType.Blob, Object = sha, Tag = "tag" };

                var tag = await fixture.Create(context.RepositoryOwner, context.RepositoryName, newTag);
                var gitTag = await fixture.Get(context.RepositoryOwner, context.RepositoryName, tag.Sha);

                Assert.NotNull(gitTag);
                Assert.Equal(gitTag.Object.Type, TaggedType.Blob);
                Assert.Equal(gitTag.Message, "Hello");
                Assert.Equal(gitTag.Object.Sha, sha);
            }

            [IntegrationTest]
            public async Task CreatesTagForRepositoryWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var newTag = new NewTag { Message = "Hello", Type = TaggedType.Blob, Object = sha, Tag = "tag" };

                var tag = await github.Git.Tag.Create(context.Repository.Id, newTag);

                var gitTag = await github.Git.Tag.Get(context.Repository.Id, tag.Sha);

                Assert.NotNull(gitTag);
                Assert.Equal(gitTag.Object.Type, TaggedType.Blob);
                Assert.Equal(gitTag.Message, "Hello");
                Assert.Equal(gitTag.Object.Sha, sha);
            }

            [IntegrationTest]
            public async Task DeserializeTagSignatureVerification()
            {
                var github = Helper.GetAuthenticatedClient();

                var newTag = new NewTag { Message = "Hello", Type = TaggedType.Blob, Object = sha, Tag = "tag" };

                var tag = await github.Git.Tag.Create(context.Repository.Id, newTag);

                var gitTag = await github.Git.Tag.Get(context.Repository.Id, tag.Sha);

                Assert.NotNull(gitTag);

                Assert.False(gitTag.Verification.Verified);
                Assert.Equal(gitTag.Verification.Reason, VerificationReason.Unsigned);
                Assert.Null(gitTag.Verification.Signature);
                Assert.Null(gitTag.Verification.Payload);

            }
        }
    }
}
