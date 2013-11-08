using System;
using Octokit;
using Octokit.Internal;
using Xunit;

public class CommitTests
{
    public class Serialization
    {
        [Fact]
        public void PerformsCommitSerialization()
        {
            var tree = new GitReference { Sha = "tree-reference", Url = "tree-url" };
            var parent1 = new GitReference { Sha = "parent1-reference", Url = "parent1-url" };
            var parent2 = new GitReference { Sha = "parent2-reference", Url = "parent2-url" };

            var author = new Signature
            {
                Name = "author-name", 
                Email = "author-email", 
                Date = new DateTime(2013, 10, 15, 13, 40, 14, DateTimeKind.Utc)
            };
            
            var committer = new Signature { 
                Name = "committer-name", 
                Email = "committer-email",
                Date = new DateTime(2013, 06, 29, 10, 12, 50, DateTimeKind.Utc)
            };

            var commit = new Commit
            {
                Sha = "commit-reference",
                Url = "commit-url",
                Message = "commit-message",
                Parents = new[]{ parent1, parent2 },
                Tree = tree,
                Author = author,
                Committer = committer,
            };

            var json = new SimpleJsonSerializer().Serialize(commit);

            const string expectedResult = "{\"message\":\"commit-message\"," +
                                            "\"author\":{" +
                                                "\"name\":\"author-name\"," +
                                                "\"email\":\"author-email\"," +
                                                "\"date\":\"2013-10-15T13:40:14Z\"" +
                                            "}," +
                                            "\"committer\":{" +
                                                "\"name\":\"committer-name\"," +
                                                "\"email\":\"committer-email\"," +
                                                "\"date\":\"2013-06-29T10:12:50Z\"" +
                                            "}," +
                                            "\"tree\":{" +
                                                "\"url\":\"tree-url\"," +
                                                "\"sha\":\"tree-reference\"" +
                                            "}," +
                                            "\"parents\":[{" +
                                                "\"url\":\"parent1-url\"," +
                                                "\"sha\":\"parent1-reference\"" +
                                            "}," +
                                            "{" +
                                                "\"url\":\"parent2-url\"," +
                                                "\"sha\":\"parent2-reference\"" +
                                            "}]," +
                                            "\"url\":\"commit-url\"," +
                                            "\"sha\":\"commit-reference\"" +
                                          "}";

            Assert.Equal(expectedResult, json);
        }
    }    
}