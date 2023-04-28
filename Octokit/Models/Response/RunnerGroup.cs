
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerGroup
    {
        public RunnerGroup() { }

        public RunnerGroup(long id)
        {
            Id = id;
        }

        public RunnerGroup(long id, string name, string visibility, bool Default, string runnersUrl, bool allowsPublicRepositories, bool restrictedToWorkflows, List<string> selectedWorkflows, bool workflowRestrictionsReadOnly)
        {
            Id = id;
            Name = name;
            Visibility = visibility;
            this.Default = Default; // default is a reserved keyword
            RunnersUrl = runnersUrl;
            AllowsPublicRepositories = allowsPublicRepositories;
            RestrictedToWorkflows = restrictedToWorkflows;
            SelectedWorkflows = selectedWorkflows;
            WorkflowRestrictionsReadOnly = workflowRestrictionsReadOnly;
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Visibility { get; private set; }
        public bool Default { get; private set; }
        public string RunnersUrl { get; private set; }
        public bool AllowsPublicRepositories { get; private set; }
        public bool RestrictedToWorkflows { get; private set; }
        public IReadOnlyList<string> SelectedWorkflows { get; private set; }
        public bool WorkflowRestrictionsReadOnly { get; private set; }

        internal string DebuggerDisplay => string.Format("Id: {0}, Name: {1}, Visibility: {2}, Default: {3}, RunnersUrl: {4}, AllowsPublicRepositories: {5}, RestrictedToWorkflows: {6}, SelectedWorkflows: {7}, WorkflowRestrictionsReadOnly: {8}",
         Id, Name, Visibility, Default, RunnersUrl, AllowsPublicRepositories, RestrictedToWorkflows, string.Join(", ", SelectedWorkflows.Select(x => x.ToString())), WorkflowRestrictionsReadOnly);
    }
}
