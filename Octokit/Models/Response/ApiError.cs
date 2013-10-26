using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    public class ApiError
    {
        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// URL to the documentation for this error.
        /// </summary>
        public string DocumentationUrl { get; set; }

        // TODO: This ought to be an IReadOnlyList<ApiErrorDetail> but we need to add support to SimpleJson for that.
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<ApiErrorDetail> Errors { get; set; }
    }
}