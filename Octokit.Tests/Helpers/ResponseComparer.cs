using System.Collections.Generic;

namespace Octokit.Tests.Helpers
{
    public class ResponseComparer : IEqualityComparer<IResponse>
    {
        private static readonly StringKeyedDictionaryComparer<string> _stringKeyedDictionaryComparer = new StringKeyedDictionaryComparer<string>();
        private static readonly ApiInfoComparer _apiInfoComparer = new ApiInfoComparer();

        public bool Equals(IResponse x, IResponse y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return Equals(x.Body, y.Body) &&
                   _stringKeyedDictionaryComparer.Equals(x.Headers, y.Headers) &&
                   _apiInfoComparer.Equals(x.ApiInfo, y.ApiInfo) &&
                   x.StatusCode == y.StatusCode &&
                   x.ContentType == y.ContentType;
        }

        public int GetHashCode(IResponse obj)
        {
            unchecked
            {
                var hashCode = (obj.Body != null ? obj.Body.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Headers != null ? _stringKeyedDictionaryComparer.GetHashCode(obj.Headers) : 0);
                hashCode = (hashCode * 397) ^ (obj.ApiInfo != null ? _apiInfoComparer.GetHashCode(obj.ApiInfo) : 0);
                hashCode = (hashCode * 397) ^ (int)obj.StatusCode;
                hashCode = (hashCode * 397) ^ (obj.ContentType != null ? obj.ContentType.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
