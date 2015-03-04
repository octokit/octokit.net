using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Octokit
{
    public interface ILazyRequest<T>
    {
        ILazyRequest<T> WithOptions(
            int startPage = 0,
            int pageCount = -1,
            int pageSize = 30,
            string userAgent = null);

        TaskAwaiter<IReadOnlyList<T>> ToTask();
    }
}
