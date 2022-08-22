using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IPackagesClient
    {
        Task<IReadOnlyList<Package>> List(string org, PackageType packageType, PackageVisibility? packageVisibility = null);
    }
}