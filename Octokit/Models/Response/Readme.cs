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

        public Readme() { }

        internal Readme(ReadmeResponse response, IApiConnection client)
        {
            Ensure.ArgumentNotNull(response, nameof(response));
            Ensure.ArgumentNotNull(client, nameof(client));

            Name = response.Name;
            Url = response.Url;
            HtmlUrl = response.HtmlUrl;
            if (response.Encoding.Equals("base64", StringComparison.OrdinalIgnoreCase))
            {
                var contentAsBytes = Convert.FromBase64String(response.Content);
                Content = Encoding.UTF8.GetString(contentAsBytes, 0, contentAsBytes.Length);
            }
            htmlContent = new Lazy<Task<string>>(async () => await client.GetHtml(new Uri(Url)).ConfigureAwait(false));
        }

        public Readme(Lazy<Task<string>> htmlContent, string content, string name, string htmlUrl, string url)
        {
            this.htmlContent = htmlContent;
            Content = content;
            Name = name;
            HtmlUrl = htmlUrl;
            Url = url;
        }

        public string Content { get; private set; }
        public string Name { get; private set; }
        public string HtmlUrl { get; private set; }
        public string Url { get; private set; }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        public Task<string> GetHtmlContent()
        {
            return htmlContent.Value;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0} ", Name);
            }
        }
    }
}