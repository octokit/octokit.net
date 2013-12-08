﻿namespace Octokit.Reactive
{
    /// <summary>
    /// Used to maintain api structure therefore contains no methods
    /// </summary>
    public interface IObservableGitDatabaseClient
    {
        IObservableTagsClient Tag { get; set; }
        IObservableCommitsClient Commit { get; set; }
        IObservableReferencesClient Reference { get; set; }
    }
}