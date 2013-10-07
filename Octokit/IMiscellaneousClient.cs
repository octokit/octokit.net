using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IMiscellaneousClient
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyDictionary<string, Uri>> GetEmojis();
        Task<string> RenderRawMarkdown(string markdown);
    }
}
