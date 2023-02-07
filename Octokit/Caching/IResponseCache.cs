using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit.Caching
{
    public interface IResponseCache
    {
        Task<CachedResponse.V1> GetAsync(IRequest request);

        Task SetAsync(IRequest request, CachedResponse.V1 cachedResponse);
    }
}
