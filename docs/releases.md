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

To create a new release you must have a corresponding tag in the repository. See the `git-database.md` docs for details.

```
var newRelease = new ReleaseUpdate("v1.0.0");
newRelease.Name = "Version One Point Oh";
newRelease.Body = "**This** is some *Markdown*";
newRelease.Draft = true;
newRelease.Prerelease = false;

var result = await client.Release.Create("octokit", "octokit.net", newRelease);
Console.WriteLine("Created release id {0}", release.Id);
```

Note that the `Draft` flag is used to indicate when a release should be published to the world, whereas the `PreRelease` flag is used to indicate whether a release is unofficial or preview release.

### Update

Once the release is ready for the public, you can apply an update to the release:

```
var release = client.Release.Get("octokit", "octokit.net", 1);
var updateRelease = release.ToUpdate();
updateRelease.Draft = false;
updateRelease.Name = "Version 1.0";
updateRelease.TargetCommitish = "0edef870ecd885cc6506f1e3f08341e8b87370f2" // can also be a ref
var result = await client.Release.Edit("octokit", "octokit.net", 1, updateRelease);
```

### Upload Assets

If you have any assets to include with the release, you can upload them after creating the release:

```
var archiveContents = await File.OpenRead("output.zip"); // TODO: better sample
var assetUpload = new ReleaseAssetUpload() 
{
     FileName = "my-cool-project-1.0.zip",
     ContentType = "application/zip",
     RawData = archiveContents
};
var release = client.Release.Get("octokit", "octokit.net", 1);
var asset = await client.Release.UploadAsset(release, assetUpload);
```

**TODO:** are there any known limits documented to upload assets?
