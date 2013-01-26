using Octopi.Http;

namespace Octopi
{
    public interface IGitHubClient
    {
        IConnection Connection { get; }
    }
}
