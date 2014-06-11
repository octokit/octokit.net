**TODO:** required using statements for scripts?
**TODO:** unit tests and inject ala NSubstitute

# Working with Releases

### Get All

To retrieve all releases for a repository:

```
var releases = client.Release.GetAll("octokit", "octokit.net");
var latest = releases[0];
Console.WriteLine(
    "The latest release is tagged at {0} and is named {1}", 
    latest.TagName, 
    latest.Name);
```

### Create

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

### Update

**TODO:** I want an extension method to transform a `Release` into a `ReleaseUpdate`, like this:
**TODO:** `EditRelease` -> `Edit` in API

Once the release is ready for the public, you can apply an update to the release:

```
var release = client.Release.Get("octokit", "octokit.net", 1);
var updateRelease = release.ToEditOperation(); (???)
updateRelease.Draft = false;
updatedRelease.Name = "Version 1.0";
var updatedRelease = await client.Release.EditRelease("octokit", "octokit.net", 1, updateRelease);
```

### Upload Assets

If you have any assets to include with the release, you can upload them after creating the release:

```
var archiveContents = await File.ReadAllBytes("output.zip"); // TODO: better sample
var assetUpload = new ReleaseAssetUpload() 
{
     FileName = "my-cool-project-1.0.zip",
     Content-Type = "application/zip",
     RawData = archiveContents
};
var release = client.Release.Get("octokit", "octokit.net", 1);
var asset = await client.Release.UploadAsset(release, assetUpload);
```

**TODO:** are there any known limits documented?
