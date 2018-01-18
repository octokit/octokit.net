using System.Threading.Tasks;

namespace Octokit
{
    public interface IApplicationsClient
    {
        Task<Application> Create();
    }
}