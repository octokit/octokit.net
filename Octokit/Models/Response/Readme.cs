using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Readme
    {
        readonly Lazy<Task<string>> htmlContent;

        internal Readme(ReadmeResponse response, IApiConnection client)
        {
            Ensure.ArgumentNotNull(response, "response");
            Ensure.ArgumentNotNull(client, "client");

            Name = response.Name;
            Url = new Uri(response.Url);
            HtmlUrl = new Uri(response.HtmlUrl);
            if (response.Encoding.Equals("base64", StringComparison.OrdinalIgnoreCase))
            {
                var contentAsBytes = Convert.FromBase64String(response.Content);
                Content = Encoding.UTF8.GetString(contentAsBytes, 0, contentAsBytes.Length);
            }
            htmlContent = new Lazy<Task<string>>(async () => await client.GetHtml(HtmlUrl).ConfigureAwait(false));
        }

        public string Content { get; private set; }
        public string Name { get; private set; }
        public Uri HtmlUrl { get; private set; }
        public Uri Url { get; private set; }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makse a network request")]
        public Task<string> GetHtmlContent()
        {
            return htmlContent.Value;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0} ", Name);
            }
        }
    }
}