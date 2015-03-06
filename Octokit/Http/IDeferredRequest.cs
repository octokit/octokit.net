using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class ApiOptions
    {
        public int? StartPage { get; set; }
        public int? PageCount { get; set; }
        public int? PageSize { get; set; }
        public string Accepts { get; set; }
    }

    public interface IDeferredRequest<T>
    {
        IDeferredRequest<T> WithOptions(ApiOptions options);

        Task<IReadOnlyList<T>> ToTask();
    }
}
