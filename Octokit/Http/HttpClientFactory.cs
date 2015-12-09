using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using Octokit.Internal;

namespace Octokit
{
    public static class HttpClientFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static HttpClient Create()
        {
            var handler = HttpMessageHandlerFactory.CreateDefault();

            var http = new HttpClient(new RedirectHandler { InnerHandler = handler });

            return http;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "info")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static HttpClient Create(ClientInfo info)
        {
            Ensure.ArgumentNotNull(info, "info");
            
            var handler = HttpMessageHandlerFactory.CreateDefault();

            var http = new HttpClient(new RedirectHandler { InnerHandler = handler });

            if (info.Timeout.HasValue)
            {
                http.Timeout = info.Timeout.Value;
            }

            if (!string.IsNullOrWhiteSpace(info.UserAgent))
            {
                http.DefaultRequestHeaders.Add("User-Agent", FormatUserAgent(info.UserAgent));
            }

            if (info.Credentials != null)
            {
                // TODO: need to work out whether to embrace asynchrony here
                //       or change the interface
                var task = info.Credentials.GetCredentials();
                task.Wait();
                var credentials = task.Result ?? Credentials.Anonymous;
                http.DefaultRequestHeaders.Authorization = authenticators[credentials.AuthenticationType].GetAuthorizationHeaderValue(credentials);
            }

            http.BaseAddress = info.Server != null
                ? FixUpBaseUri(info.Server)
                : GitHubClient.GitHubApiUrl;

            return http;
        }

        static string FormatUserAgent(string userAgent)
        {
            return string.Format(CultureInfo.InvariantCulture,
                "{0} ({1} {2}; {3}; {4}; Octokit {5})",
                userAgent,
#if NETFX_CORE
                // Microsoft doesn't want you changing your Windows Store Application based on the processor or
                // Windows version. If we really wanted this information, we could do a best guess based on
                // this approach: http://attackpattern.com/2013/03/device-information-in-windows-8-store-apps/
                // But I don't think we care all that much.
                "WindowsRT",
                "8+",
                "unknown",
#else
                Environment.OSVersion.Platform,
                Environment.OSVersion.Version.ToString(3),
                Environment.Is64BitOperatingSystem ? "amd64" : "x86",
#endif
                CultureInfo.CurrentCulture.Name,
                AssemblyVersionInformation.Version);
        }

        static Uri FixUpBaseUri(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            if (uri.Host.Equals("github.com") || uri.Host.Equals("api.github.com"))
            {
                return GitHubClient.GitHubApiUrl;
            }

            return new Uri(uri, new Uri("/api/v3/", UriKind.Relative));
        }

        static readonly Dictionary<AuthenticationType, IAuthenticationHandler> authenticators =
                new Dictionary<AuthenticationType, IAuthenticationHandler>
                {
                            { AuthenticationType.Anonymous, new AnonymousAuthenticator() },
                            { AuthenticationType.Basic, new BasicAuthenticator() },
                            { AuthenticationType.Oauth, new TokenAuthenticator() }
                };


    }
}
