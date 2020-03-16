using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Searching Code/Files
    /// https://developer.github.com/v3/search/#search-commits
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCommitsRequest : BaseSearchRequest
    {
    }
}
