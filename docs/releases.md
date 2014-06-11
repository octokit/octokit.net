**TODO:** required using statements for scripts?
**TODO:** unit tests and inject ala NSubstitute

# Working with Releases

To retrieve all releases for a repository:

```
var releases = client.Release.GetAll("octokit", "octokit.net");
var latest = releases[0];
Console.WriteLine(
    "The latest release is tagged at {0} and is named {1}", 
    latest.TagName, 
    latest.Name);
```

To create a new release you must have a corresponding tag in the repository

**TODO:** docs for tagging repository using Octokit

**TODO:** `CreateRelease` -> `Create` in API

```
var newRelease = new ReleaseUpdate("1.0.0");
newRelease.Name = "Version One Point Oh";
newRelease.Body = "**This** is some *Markdown*";
newRelease.Draft = true;
newRelease.PreRelease = false;

var result = await client.Release.CreateRelease("octokit", "octokit.net", newRelease);
Console.WriteLine("You just created release id {0}", release.Id);
```

**TODO:** refer to GitHub docs for definition of PreRelease flag and update

Note that the `Draft` flag is used to indicate when a release should be published to the world, whereas the `PreRelease` flag is used to indicate whether a release is unofficial or preview release.

**TODO:** I want an extension method to transform a `Release` into a `ReleaseUpdate`, like this:
**TODO:** `EditRelease` -> `Edit` in API

Once the release is ready for the public, you can craft an update to the release:

```
var release = client.Release.Get("octokit", "octokit.net", 1);
var updateRelease = release.ToEditOperation(); (???)
updateRelease.Draft = false; 
var updatedRelease = await client.Release.EditRelease("octokit", "octokit.net", 1, updateRelease);
```
