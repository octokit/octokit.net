namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/">Activity API documentation</a> for more information.
    /// </remarks>
    public interface IActivitiesClient
    {
        IEventsClient Events { get; }
        IStarredClient Starring { get; }
        IWatchedClient Watching { get; }
        IFeedsClient Feeds { get; }
    }
}