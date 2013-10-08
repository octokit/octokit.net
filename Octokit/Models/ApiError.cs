using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class ApiError
    {
        public string Message { get; set; }

        // TODO: This ought to be an IReadOnlyList<ApiErrorDetail> but we need to add support to SimpleJson for that.
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<ApiErrorDetail> Errors { get; set; }
    }
}