#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;
namespace Octokit
{
    public interface ISearchClient
    {
      Task<IReadOnlyList<SearchRepo>> SearchRepo(SearchTerm search);
    }
}