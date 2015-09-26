using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration.Helpers;
using Octokit.Tests.Integration;
using Xunit;

public class CommitsClientTests
{
    public CommitsClientTests() { }

    [IntegrationTest]
    public async Task CanCreateAndRetrieveCommit()
    {
        var client = Helper.GetAuthenticatedClient();
        var fixture = client.GitDatabase.Commit;
        
        using (var context = await client.CreateRepositoryContext("public-repo"))
        {
            var owner = context.Repository.Owner.Login;

            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await client.GitDatabase.Blob.Create(owner, context.Repository.Name, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Type = TreeType.Blob,
                Mode = FileMode.File,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await client.GitDatabase.Tree.Create(owner, context.Repository.Name, newTree);

            var newCommit = new NewCommit("test-commit", treeResult.Sha);

            var commit = await fixture.Create(owner, context.Repository.Name, newCommit);
            Assert.NotNull(commit);

            var retrieved = await fixture.Get(owner, context.Repository.Name, commit.Sha);
            Assert.NotNull(retrieved);
        }
    }
}