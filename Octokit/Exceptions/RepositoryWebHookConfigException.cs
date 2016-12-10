using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
#if !NO_SERIALIZABLE
using System.Runtime.Serialization;
#endif
using System.Security;

namespace Octokit
{
#if !NO_SERIALIZABLE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class RepositoryWebHookConfigException : Exception
    {
        readonly string message;

        public RepositoryWebHookConfigException(IEnumerable<string> invalidConfig)
        {
            var parameterList = string.Join(", ", invalidConfig.Select(ic => ic.FromRubyCase()));
            message = string.Format(CultureInfo.InvariantCulture,
                "Duplicate webhook config values found - these values: {0} should not be passed in as part of the config values. Use the properties on the NewRepositoryWebHook class instead.",
                parameterList);
        }

        public override string Message
        {
            get { return message; }
        }

#if !NO_SERIALIZABLE
        /// <summary>
        /// Constructs an instance of RepositoryWebHookConfigException
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected RepositoryWebHookConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) return;
            message = info.GetString("Message");
        }

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Message", Message);
        }
#endif
    }
}
