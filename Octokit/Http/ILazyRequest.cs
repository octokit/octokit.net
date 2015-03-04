using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Octokit
{
    public interface ILazyRequest<T>
    {
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed",
            Justification = "let me make this money")]
        ILazyRequest<T> WithOptions(
            int startPage = 0,
            int pageCount = -1,
            int pageSize = 30,
            string accepts = null);

        TaskAwaiter<IReadOnlyList<T>> ToTask();
    }
}
