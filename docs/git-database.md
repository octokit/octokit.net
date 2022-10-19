# Working with the Git Database

### Create a Branch

Branches can be created through the Git client. These are known as References. To create a reference, the first property you need is the name of the branch you want to create. You must ensure the reference starts with "refs" and contain two "/", otherwise it will be rejected. The second property is the SHA of the commit you want to branch from. This can be any commit but it must exist in the repository to be accepted.

```csharp
var reference = new NewReference($"refs/heads/{branchName}", commit.Sha);
```

You can then create the Reference using the Reference.Create Api, either passing in the Repository Owner and Name or the Repository Id.
```csharp
var branch = await github.Git.Reference.Create(owner, repo, reference);
var branch = await github.Git.Reference.Create(id, reference);
```

### Tag a Commit

Tags can be created through the GitHub API

```csharp
var tag = new NewTag {
    Message = "Tagging a new release of Octokit",
    Tag = "v1.0.0",
    Object = "ee062e0", // short SHA
    Type = TaggedType.Commit, // TODO: what are the defaults when nothing specified?
    Tagger = new Signature
    {
        Name = "Brendan Forster",
        Email = "brendan@github.com",
        Date = DateTime.UtcNow
    }	
};
var result = await client.Git.Tag.Create("octokit", "octokit.net", tag);
Console.WriteLine("Created a tag for {0} at {1}", result.Tag, result.Sha);
```

Or you can fetch an existing tag from the API:

```csharp
var tag = await client.Git.Tag.Get("octokit", "octokit.net", "v1.0.0");
```
