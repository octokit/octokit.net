using System.Threading.Tasks;

namespace Octokit
{
    public interface IReleasesClient
    {
        Task<IReadOnlyCollection<Release>> GetAll(string owner, string repository);
    }
}
