using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SecretsResponse
    {
        public SecretsResponse()
        {
        }

        public SecretsResponse(int totalCount, IReadOnlyList<Secret> secrets)
        {
            TotalCount = totalCount;
            Secrets = secrets;
        }

        public int TotalCount { get; protected set; }

        public IReadOnlyList<Secret> Secrets { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, WorkflowJobs: {1}", TotalCount, Secrets.Count);
    }
}
