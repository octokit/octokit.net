using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration.Helpers;
using Octokit.Tests.Integration;
using Xunit;

public class CommitsClientTests
{
    [IntegrationTest]
    public async Task CanCreateAndRetrieveCommit()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Git.Commit;

        using (var context = await github.CreateRepositoryContext("public-repo"))
        {
            var owner = context.Repository.Owner.Login;

            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await github.Git.Blob.Create(owner, context.Repository.Name, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Type = TreeType.Blob,
                Mode = FileMode.File,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await github.Git.Tree.Create(owner, context.Repository.Name, newTree);

            var newCommit = new NewCommit("test-commit", treeResult.Sha);

            var commit = await fixture.Create(owner, context.Repository.Name, newCommit);
            Assert.NotNull(commit);

            var retrieved = await fixture.Get(owner, context.Repository.Name, commit.Sha);
            Assert.NotNull(retrieved);
        }
    }

    [IntegrationTest]
    public async Task CanDeserializeVerificationObjectInResponse()
    {
        var github = Helper.GetAuthenticatedClient();

        var commit = await github.Git.Commit.Get("noonari", "Signature-Verification", "1965d149ce1151cf411300d15f8d890d9259bd21");

        Assert.False(commit.Verification.Verified);
        Assert.Equal("-----BEGIN PGP SIGNATURE-----\nVersion: GnuPG v1\n\niQEcBAABAgAGBQJXYT2BAAoJEJyZ1vxIV0+N9ZwIAKlf3dk9n1q1mD5AT3Ahtj9o\nF4H25zsHynJk2lnH4YxVvDBEc/uMCXzX6orihZiSdA5UXE7tPyEEZddQdp8pxulX\ncIsFKcrfQqHJnTbT90z5PhAk94lyN9fFngzPW1tgZZVjp2YiiqgXduBWWm6EREOh\nS1Iu9wBqScQomhTXoksmNZyGTZ0LviSi0pkqRY64pQhKnpLlu1OFXaeDvhYocB+E\nY5URZsXodvIkBuzCkWCu8ra4eaXIIARkas4+jIvn0FIx9CzEVz0Zau/5Fk+BR+Te\n7a3/7JH7yuObPB0hqPSuFYyxtvPfxtayvhkGD3YkQqDAkWCpISGyVFzxrrC7z0Y=\n=kbih\n-----END PGP SIGNATURE-----",
            commit.Verification.Signature);

        Assert.Equal("tree c91c844f37974093a3f0a864755441b577e7663a\nparent 6eb645f6badd46de65700b4d7b6fcdb97684ce5a\nauthor noonari <SarmadSattar1@gmail.com> 1465990529 +0500\ncommitter noonari <SarmadSattar1@gmail.com> 1465990529 +0500\n\ngpg stuff\n",
            commit.Verification.Payload);

        Assert.Equal(VerificationReason.UnknownKey, commit.Verification.Reason);
    }

    [IntegrationTest]
    public async Task CanCreateAndRetrieveCommitWithRepositoryId()
    {
        var github = Helper.GetAuthenticatedClient();
        var fixture = github.Git.Commit;

        using (var context = await github.CreateRepositoryContext("public-repo"))
        {
            var owner = context.Repository.Owner.Login;

            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await github.Git.Blob.Create(owner, context.Repository.Name, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Type = TreeType.Blob,
                Mode = FileMode.File,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await github.Git.Tree.Create(owner, context.Repository.Name, newTree);

            var newCommit = new NewCommit("test-commit", treeResult.Sha);

            var commit = await fixture.Create(context.Repository.Id, newCommit);
            Assert.NotNull(commit);

            var retrieved = await fixture.Get(context.Repository.Id, commit.Sha);
            Assert.NotNull(retrieved);
        }
    }
}