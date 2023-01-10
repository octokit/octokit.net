using System.Collections.Generic;
using System.Linq;

namespace Octokit.Tests.Helpers
{
    public class CollectionComparer<T> : IEqualityComparer<IReadOnlyCollection<T>>
    {
        public bool Equals(IReadOnlyCollection<T> x, IReadOnlyCollection<T> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.Count == y.Count && x.Intersect(y).Count() == x.Count();
        }

        public int GetHashCode(IReadOnlyCollection<T> obj)
        {
            return obj.Count;
        }
    }
}
