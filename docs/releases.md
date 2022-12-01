# Working with Releases

### Get All

To retrieve all releases for a repository:

```csharp
var releases = await client.Repository.Release.GetAll("octokit", "octokit.net");
var latest = releases.Result.ElementAt(0);
Console.WriteLine(
    "The latest release is tagged at {0} and is named {1}",
    latest.TagName,
    latest.Name);
```

### Get Latest

To retrieve the latest release for a repository:

```csharp
var releases = await client.Repository.Release.GetLatest("octokit", "octokit.net");
var latest = releases.Result;
Console.WriteLine(
    "The latest release is tagged at {0} and is named {1}",
    latest.TagName,
    latest.Name);
```

### Create

To create a new release you must have a corresponding tag in the repository. See the `git-database.md` docs for details.

```csharp
var newRelease = new NewRelease("v1.0.0");
newRelease.Name = "Version One Point Oh";
newRelease.Body = "**This** is some *Markdown*";
newRelease.Draft = true;
newRelease.Prerelease = false;

var result = await client.Repository.Release.Create("octokit", "octokit.net", newRelease);
Console.WriteLine("Created release id {0}", result.Id);
```

Note that the `Draft` flag is used to indicate when a release should be published to the world, whereas the `PreRelease` flag is used to indicate whether a release is unofficial or preview release.

### Generate release notes

GitHub can generate a name and body for a new release [automatically](https://github.blog/2021-10-04-beta-github-releases-improving-release-experience/#introducing-auto-generated-release-notes), based upon merged pull requests.
[This is an example](https://github.com/MylesBorins/release-notes-test/releases/tag/v2.0.0) of automatically generated text.

```csharp
var newTag = "v1.5.7";
var newRelease = new NewRelease(newTag);
newRelease.GenerateReleaseNotes = true; // Set for Name and Body to be generated.
newRelease.TargetCommitish = "main"; // Optional, can be a branch, tag, or SHA; defaults to the main branch.
```

#### Customizing generated notes
```csharp
var newTag = "v1.5.7";
var generationRequest = new GenerateReleaseNotesRequest(newTag);
generationRequest.TargetCommitish = "main"; // Optional, can be a branch, tag, or SHA; defaults to the main branch.
generationRequest.PreviousTagName = "v1.5.6"; // Optional; default is automagically determined, based on existing tags.
var releaseNotes = await client.Repository.Release.GenerateReleaseNotes("octokit", "octokit.net", generationRequest);

var newRelease = new NewRelease(newTag); // Use the same tag as before, because it now appears in generated text.
newRelease.Name = releaseNotes.Name;
newRelease.Body = releaseNotes.Body;
```

This feature can be customized at the repository level, by following [these instructions](https://docs.github.com/repositories/releasing-projects-on-github/automatically-generated-release-notes#configuring-automatically-generated-release-notes).

### Update

Once the release is ready for the public, you can apply an update to the release:

```csharp
var release = client.Repository.Release.Get("octokit", "octokit.net", 1);
var updateRelease = release.ToUpdate();
updateRelease.Draft = false;
updateRelease.Name = "Version 1.0";
updateRelease.TargetCommitish = "0edef870ecd885cc6506f1e3f08341e8b87370f2" // can also be a ref
var result = await client.Repository.Release.Edit("octokit", "octokit.net", 1, updateRelease);
```

### Upload Assets

If you have any assets to include with the release, you can upload them after creating the release:

```csharp
using(var archiveContents = File.OpenRead("output.zip")) { // TODO: better sample
    var assetUpload = new ReleaseAssetUpload()
    {
         FileName = "my-cool-project-1.0.zip",
         ContentType = "application/zip",
         RawData = archiveContents
    };
    var release = client.Repository.Release.Get("octokit", "octokit.net", 1);
    var asset = await client.Repository.Release.UploadAsset(release, assetUpload);
}
```

**TODO:** are there any known limits documented to upload assets?
