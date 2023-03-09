using System;
using System.Collections.Generic;

namespace Octokit.Tests.Helpers
{
    public class ApiInfoComparer : IEqualityComparer<ApiInfo>
    {
        private static readonly CollectionComparer<string> _stringCollectionComparer = new CollectionComparer<string>();
        private static readonly StringKeyedDictionaryComparer<Uri> _stringKeyedDictionaryComparer = new StringKeyedDictionaryComparer<Uri>();
        private static readonly RateLimitComparer _rateLimitComparer = new RateLimitComparer();

        public bool Equals(ApiInfo x, ApiInfo y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return _stringCollectionComparer.Equals(x.OauthScopes, y.OauthScopes) &&
                   _stringCollectionComparer.Equals(x.AcceptedOauthScopes, y.AcceptedOauthScopes) &&
                   x.Etag == y.Etag &&
                   _stringKeyedDictionaryComparer.Equals(x.Links, y.Links) &&
                   _rateLimitComparer.Equals(x.RateLimit, y.RateLimit) &&
                   x.ServerTimeDifference.Equals(y.ServerTimeDifference);
        }

        public int GetHashCode(ApiInfo obj)
        {
            unchecked
            {
                var hashCode = (obj.OauthScopes != null ? _stringCollectionComparer.GetHashCode(obj.OauthScopes) : 0);
                hashCode = (hashCode * 397) ^ (obj.AcceptedOauthScopes != null ? _stringCollectionComparer.GetHashCode(obj.AcceptedOauthScopes) : 0);
                hashCode = (hashCode * 397) ^ (obj.Etag != null ? obj.Etag.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Links != null ? _stringKeyedDictionaryComparer.GetHashCode(obj.Links) : 0);
                hashCode = (hashCode * 397) ^ (obj.RateLimit != null ? _rateLimitComparer.GetHashCode(obj.RateLimit) : 0);
                hashCode = (hashCode * 397) ^ obj.ServerTimeDifference.GetHashCode();
                return hashCode;
            }
        }
    }
}
