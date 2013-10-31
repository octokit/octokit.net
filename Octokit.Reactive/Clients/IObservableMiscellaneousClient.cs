using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableMiscellaneousClient
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        IObservable<IReadOnlyDictionary<string, Uri>> GetEmojis();

        IObservable<string> RenderRawMarkdown(string markdown);
    }
}
