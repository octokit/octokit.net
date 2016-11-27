# Working with the Git Database

### Tag a Commit

Tags can be created through the GitHub API

```
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
var result = await client.Git.Tags.Create("octokit", "octokit.net", tag);
Console.WriteLine("Created a tag for {0} at {1}", result.Tag, result.Sha);
```

Or you can fetch an existing tag from the API:

```
var tag = await client.Git.Tags.Get("octokit", "octokit.net", "v1.0.0");
```
