using Cake.Common.Diagnostics;
using Cake.Frosting;
using Octokit;

public class CreateGitHubDraftRelease : FrostingTask<BuildContext>
{
    private readonly IGitHubClient _githubCllient;

    public CreateGitHubDraftRelease(IGitHubClient githubClient)
    {
        _githubCllient = githubClient;
    }

    public override void Run(BuildContext context)
    {
        var latestRelease = AsyncHelper.RunSync(() => _githubCllient.Repository.Release.GetLatest(Constants.OwnerName, Constants.RepositoryName));
        if (latestRelease == null || latestRelease.TagName != context.Version)
        {
            context.Information("No existing release for version {0} was found", context.Version);
        }
        else if (latestRelease.TagName == context.Version)
        {
            context.Information("An existing release for version {0} was found, skipping the creation of a new one", context.Version);
            return;
        }

        var newRelease = new NewRelease(context.Version)
        {
            Draft = true,
            Name = context.Version,
            TargetCommitish = Constants.MasterBranchName
        };

        var release = AsyncHelper.RunSync(() => _githubCllient.Repository.Release.Create(
            Constants.OwnerName,
            Constants.RepositoryName,
            newRelease));

        context.Information("Created a new draft release for version {0} at {1}",
            context.Version,
            release.Url);
    }

    public override bool ShouldRun(BuildContext context)
    {
        return context.IsOriginalRepo && context.IsReleaseBranch;
    }
}
