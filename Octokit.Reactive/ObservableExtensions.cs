using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public static class ObservableExtensions
    {
        public static IObservable<string> GetReadmeAsHtml(this IObservableRepositoriesClient client,
            string owner,
            string name)
        {
            Ensure.ArgumentNotNull(client, "client");
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return client.GetReadme(owner, name).SelectMany(r => r.GetHtmlContent().ToObservable());
        }
    }
}
