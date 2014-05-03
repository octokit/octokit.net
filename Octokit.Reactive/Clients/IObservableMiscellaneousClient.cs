using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableMiscellaneousClient
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        IObservable<Emoji> GetEmojis();

        IObservable<string> RenderRawMarkdown(string markdown);
    }
}
