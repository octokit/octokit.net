using System.Threading.Tasks;

namespace Octokit
{
    public interface IApplicationClient
    {
        Task<Application> Create();
    }
}