using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    public static class RepositorySetupHelper
    {
        public static async Task<TreeResponse> CreateTree(this IGitHubClient client, Repository repository, IEnumerable<KeyValuePair<string, string>> treeContents)
        {
            var collection = new List<NewTreeItem>();

            foreach (var c in treeContents)
            {
                var baselineBlob = new NewBlob
                {
                    Content = c.Value,
                    Encoding = EncodingType.Utf8
                };
                var baselineBlobResult = await client.Git.Blob.Create(repository.Owner.Login, repository.Name, baselineBlob);

                collection.Add(new NewTreeItem
                {
                    Type = TreeType.Blob,
                    Mode = FileMode.File,
                    Path = c.Key,
                    Sha = baselineBlobResult.Sha
                });
            }

            var newTree = new NewTree();
            foreach (var item in collection)
            {
                newTree.Tree.Add(item);
            }

            return await client.Git.Tree.Create(repository.Owner.Login, repository.Name, newTree);
        }

        public static async Task<Commit> CreateCommit(this IGitHubClient client, Repository repository, string message, string sha, string parent)
        {
            var newCommit = new NewCommit(message, sha, parent);
            return await client.Git.Commit.Create(repository.Owner.Login, repository.Name, newCommit);
        }

        public static async Task<Reference> CreateTheWorld(this IGitHubClient client, Repository repository)
        {
            var master = await client.Git.Reference.Get(repository.Owner.Login, repository.Name, "heads/master");

            // create new commit for master branch
            var newMasterTree = await client.CreateTree(repository, new Dictionary<string, string> { { "README.md", "Hello World!" } });
            var newMaster = await client.CreateCommit(repository, "baseline for pull request", newMasterTree.Sha, master.Object.Sha);

            // update master
            await client.Git.Reference.Update(repository.Owner.Login, repository.Name, "heads/master", new ReferenceUpdate(newMaster.Sha));

            // create new commit for feature branch
            var featureBranchTree = await client.CreateTree(repository, new Dictionary<string, string> { { "README.md", "I am overwriting this blob with something new" } });
            var featureBranchCommit = await client.CreateCommit(repository, "this is the commit to merge into the pull request", featureBranchTree.Sha, newMaster.Sha);

            // create branch
            return await client.Git.Reference.Create(repository.Owner.Login, repository.Name, new NewReference("refs/heads/my-branch", featureBranchCommit.Sha));
        }
    }
}
