﻿using System;
using System.Collections.Generic;
#if NET_45
using System.Collections.ObjectModel;
#endif

namespace Octokit
{
    /// <summary>
    /// Extra information returned as part of each api response.
    /// </summary>
    public class ApiInfo
    {
        public ApiInfo(IDictionary<string, Uri> links,
            IList<string> oauthScopes,
            IList<string> acceptedOauthScopes,
            string etag,
            RateLimit rateLimit)
        {
            Ensure.ArgumentNotNull(links, "links");
            Ensure.ArgumentNotNull(oauthScopes, "oauthScopes");

            Links = new ReadOnlyDictionary<string, Uri>(links);
            OauthScopes = new ReadOnlyCollection<string>(oauthScopes);
            AcceptedOauthScopes = new ReadOnlyCollection<string>(acceptedOauthScopes);
            Etag = etag;
            RateLimit = rateLimit;
        }

        /// <summary>
        /// Oauth scopes that were included in the token used to make the request.
        /// </summary>
        public IReadOnlyList<string> OauthScopes { get; private set; }

        /// <summary>
        /// Oauth scopes accepted for this particular call.
        /// </summary>
        public IReadOnlyList<string> AcceptedOauthScopes { get; private set; }

        /// <summary>
        /// Etag
        /// </summary>
        public string Etag { get; private set; }

        /// <summary>
        /// Links to things like next/previous pages
        /// </summary>
        public IReadOnlyDictionary<string, Uri> Links { get; private set; }

        /// <summary>
        /// Information about the API rate limit
        /// </summary>
        public RateLimit RateLimit { get; private set; }

        /// <summary>
        /// Allows you to clone ApiInfo 
        /// </summary>
        /// <returns>A clone of <seealso cref="ApiInfo"/></returns>
        public ApiInfo Clone()
        {
            // Seem to have to do this to pass a whole bunch of tests (for example Octokit.Tests.Clients.EventsClientTests.DeserializesCommitCommentEventCorrectly)
            // I believe this has something to do with the Mocking framework.
            if (this.Links == null || this.OauthScopes == null || this.RateLimit == null || this.Etag == null)
                return null;

            return new ApiInfo(this.Links.Clone(),
                                this.OauthScopes.Clone(),
                                this.AcceptedOauthScopes.Clone(),
                                new String(this.Etag.ToCharArray()),
                                this.RateLimit.Clone());
        }


    }
}
