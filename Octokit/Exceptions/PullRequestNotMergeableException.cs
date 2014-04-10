using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class PullRequestNotMergeableException : ApiValidationException
    {
        public PullRequestNotMergeableException()
        {
        }

        public override string Message
        {
            get
            {
                return "The pull request is not mergeable.";
            }
        }

#if !NETFX_CORE
        protected PullRequestNotMergeableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}