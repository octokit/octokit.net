using System.Collections.Generic;
using System.Linq;

namespace Octokit.Tests.Helpers
{
    public class StringKeyedDictionaryComparer<T> : IEqualityComparer<IReadOnlyDictionary<string, T>>
    {
        public bool Equals(IReadOnlyDictionary<string, T> x, IReadOnlyDictionary<string, T> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.Count == y.Count && x.Intersect(y).Count() == x.Count;
        }

        public int GetHashCode(IReadOnlyDictionary<string, T> obj)
        {
            return obj.Count;
        }
    }
}
