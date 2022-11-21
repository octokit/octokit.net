using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Text;
using System.Globalization;

namespace Octokit.Models.Response
{
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces",
        Justification = "People can use fully qualified names if they want to use both.")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeploymentEnvironmentsResponse
    {
        public DeploymentEnvironmentsResponse() { }


        public DeploymentEnvironmentsResponse(int totalCount, IReadOnlyList<DeploymentEnvironment> environments)
        {
            TotalCount = totalCount;
            Environments = environments;
        }

        /// <summary>
        /// The total count of secrets for the repository
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The list of secrets for the repository
        /// </summary>
        public IReadOnlyList<DeploymentEnvironment> Environments { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Count: {0}", TotalCount);
            }
        }
    }
}