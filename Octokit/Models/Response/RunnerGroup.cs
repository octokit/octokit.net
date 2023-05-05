
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

        public RunnerGroup(long id, string name, string visibility, bool @default, string runnersUrl, bool inherited, bool allowsPublicRepositories, bool restrictedToWorkflows, List<string> selectedWorkflows, bool workflowRestrictionsReadOnly)
        {
            Id = id;
            Name = name;
            Visibility = visibility;
            Default = @default;
            RunnersUrl = runnersUrl;
            Inherited = inherited;
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
        public bool Inherited { get; private set; }
        public bool AllowsPublicRepositories { get; private set; }
        public bool RestrictedToWorkflows { get; private set; }
        public IReadOnlyList<string> SelectedWorkflows { get; private set; }
        public bool WorkflowRestrictionsReadOnly { get; private set; }

        internal string DebuggerDisplay => string.Format("Id: {0}, Name: {1}, Visibility: {2}, Default: {3}, RunnersUrl: {4}, Inherited: {5}, AllowsPublicRepositories: {6}, RestrictedToWorkflows: {7}, SelectedWorkflows: {8}, WorkflowRestrictionsReadOnly: {9}",
         Id, Name, Visibility, Default, RunnersUrl, Inherited, AllowsPublicRepositories, RestrictedToWorkflows, string.Join(", ", SelectedWorkflows.Select(x => x.ToString())), WorkflowRestrictionsReadOnly);
    }
}
