using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Creates a Webhook for the repository.
    /// </summary>
    /// <remarks>
    /// To create a webhook, the following fields are required by the config:
    /// <list type="bullet">
    /// <item>
    ///   <term>url</term>
    ///   <description>A required string defining the URL to which the payloads will be delivered.</description>
    /// </item>
    /// <item>
    ///   <term>content_type</term>
    ///   <description>
    ///     An optional string defining the media type used to serialize the payloads.Supported values include json and
    ///     form.The default is form.
    ///   </description>
    /// </item>
    /// <item>
    ///   <term>secret</term>
    ///   <description>
    ///     An optional string that’s passed with the HTTP requests as an X-Hub-Signature header. The value of this
    ///     header is computed as the HMAC hex digest of the body, using the secret as the key.
    ///   </description>
    /// </item>
    /// <item>
    ///   <term>insecure_ssl:</term>
    ///   <description>
    ///     An optional string that determines whether the SSL certificate of the host for url will be verified when 
    ///     delivering payloads.Supported values include "0" (verification is performed) and "1" (verification is not 
    ///     performed). The default is "0".
    ///   </description>
    /// </item>
    /// </list>
    /// <para>
    /// API: https://developer.github.com/v3/repos/hooks/#create-a-hook
    /// </para>
    /// </remarks>   
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepositoryWebHook : NewRepositoryHook
    {
        /// <summary>
        /// Initializes a new instance of the<see cref="NewRepositoryWebHook"/> class.   
        /// Using default values for ContentType, Secret and InsecureSsl.     
        /// </summary>
        /// <param name="name">Use web for a webhook or use the name of a valid service. (See /hooks for the list of valid service names.)
        /// Use "web" for a webhook or use the name of a valid service. (See 
        /// <see href="https://api.github.com/hooks">https://api.github.com/hooks</see> for the list of valid service
        /// names.)
        /// </param>
        /// <param name="config">
        /// Key/value pairs to provide settings for this hook. These settings vary between the services and are
        /// defined in the github-services repository. Booleans are stored internally as “1” for true, and “0” for
        /// false. Any JSON true/false values will be converted automatically.
        /// </param>
        /// <param name="url">
        /// A required string defining the URL to which the payloads will be delivered.
        /// </param>
        public NewRepositoryWebHook(string name, IReadOnlyDictionary<string, string> config, string url) : this(name, config, url, WebHookContentType.Form, string.Empty, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the<see cref="NewRepositoryWebHook"/> class.        
        /// </summary>
        /// <param name="name">Use web for a webhook or use the name of a valid service. (See /hooks for the list of valid service names.)
        /// Use "web" for a webhook or use the name of a valid service. (See 
        /// <see href="https://api.github.com/hooks">https://api.github.com/hooks</see> for the list of valid service
        /// names.)
        /// </param>
        /// <param name="config">
        /// Key/value pairs to provide settings for this hook. These settings vary between the services and are
        /// defined in the github-services repository. Booleans are stored internally as “1” for true, and “0” for
        /// false. Any JSON true/false values will be converted automatically.
        /// </param>
        /// <param name="url">
        /// A required string defining the URL to which the payloads will be delivered.
        /// </param>
        /// <param name="contentType">
        /// An optional string defining the media type used to serialize the payloads. 
        /// Supported values include json and form. 
        /// The default is form.
        /// </param>
        /// <param name="secret">
        /// An optional string that’s passed with the HTTP requests as an X-Hub-Signature header. 
        /// The value of this header is computed as the 
        /// <see href="https://github.com/github/github-services/blob/f3bb3dd780feb6318c42b2db064ed6d481b70a1f/lib/service/http_helper.rb#L77">
        /// HMAC hex digest of the body, using the secret as the key
        /// </see>.
        /// </param>
        /// <param name="insecureSsl">
        /// An optional string that determines whether the SSL certificate of the host for url will be verified when delivering payloads. 
        /// Supported values include "0" (verification is performed) and "1" (verification is not performed). 
        /// The default is "0".
        /// </param>
        public NewRepositoryWebHook(string name, IReadOnlyDictionary<string, string> config, string url, WebHookContentType contentType, string secret, bool insecureSsl)
            : base(name, GetWebHookConfig(url, contentType, secret, insecureSsl, config))
        {
            Ensure.ArgumentNotNullOrEmptyString(url, "url");

            Url = url;
            ContentType = contentType;
            Secret = secret;
            InsecureSsl = insecureSsl;
        }

        static Dictionary<string, string> GetWebHookConfig(string url, WebHookContentType contentType, string secret, bool insecureSsl, IReadOnlyDictionary<string, string> config)
        {
            var webHookConfig = new Dictionary<string, string>
            {
                { "url", url },
                { "content_type", contentType.ToParameter() },
                { "secret", secret },
                { "insecure_ssl", insecureSsl ? "1" : "0" }
            };
            return config.Union(webHookConfig).ToDictionary(k => k.Key, v => v.Value);
        }

        /// <summary>
        /// Gets the URL of the hook to create.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; protected set; }
        
        /// <summary>
        /// Gets the content type used to serialize the payload.
        /// </summary>
        /// <value>
        /// The content type.
        /// </value>
        public WebHookContentType ContentType { get; set; }
        
        /// <summary>
        /// Gets the secret used as the key for the HMAC hex digest
        /// of the body passed with the HTTP requests as an X-Hub-Signature header.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }
        
        /// <summary>
        /// Gets wether the SSL certificate of the host will be verified when 
        /// delivering payloads.
        /// </summary>
        /// <value>
        ///  <c>true</c> if SSL certificate verification is not performed; 
        /// otherwise, <c>false</c>.
        /// </value>
        public bool InsecureSsl { get; set; }
    }

    /// <summary>
    /// The supported content types for payload serialization.
    /// </summary>
    public enum WebHookContentType
    {
        Form,
        Json
    }
}
