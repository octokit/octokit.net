using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Filter jobs for a workflow run.
    /// </summary>
    /// <remarks>
    /// See https://developer.github.com/v3/actions/workflow-jobs/#list-jobs-for-a-workflow-run for details.
    /// </remarks>
    public enum WorkflowRunJobsFilter
    {
        /// <summary>
        /// Returns jobs from the most recent execution of the workflow run.
        /// </summary>
        [Parameter(Value = "latest")]
        Latest,
        /// <summary>
        /// Returns all jobs for a workflow run, including from old executions of the workflow run.
        /// </summary>
        [Parameter(Value = "all")]
        All,
    }
}
