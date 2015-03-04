using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Octokit
{
    public static class CustomAwaiter
    {
        public static TaskAwaiter<IReadOnlyList<T>> GetAwaiter<T>(this ILazyRequest<T> request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return request.ToTask().GetAwaiter();
        }
    }
}