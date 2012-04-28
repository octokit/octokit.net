using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burr.Helpers
{
    public static class EnumerableExtentions
    {
        public static void Each<T>(this IEnumerable<T> e, Action<T> a)
        {
            foreach (var item in e)
            {
                a(item);
            }
        }
    }
}
