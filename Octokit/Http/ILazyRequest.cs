using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Octokit
{
    public class ApiOptions
    {
        public int? StartPage { get; set; }
        public int? PageCount { get; set; }
        public int? PageSize { get; set; }
        public string Accepts { get; set; }
    }

    public interface ILazyRequest<T>
    {
        ILazyRequest<T> WithOptions(ApiOptions options);

        TaskAwaiter<IReadOnlyList<T>> ToTask();
    }
}
