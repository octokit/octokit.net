using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Exception thrown when creating a private repository, but the user's private quota is or would be exceeded
    /// by creating it.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class PrivateRepositoryQuotaExceededException : ApiValidationException
    {
        /// <summary>
        /// Constructs an instance of PrivateRepositoryQuotaExceededException.
        /// </summary>
        /// <param name="innerException">The inner validation exception.</param>
        public PrivateRepositoryQuotaExceededException(ApiValidationException innerException)
            : base(innerException)
        {
        }

        public override string Message
        {
            get
            {
                // TODO: Would be nice to show the actual numbers, but that requires another request.
                return "You are currently at your limit of private repositories. Either delete a private repository "
                    + "you no longer use or upgrade your account to a plan that allows for more private repositories.";
            }
        }

#if !NETFX_CORE
        protected PrivateRepositoryQuotaExceededException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}