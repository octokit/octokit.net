using Burr.Http;

namespace Burr
{
    public interface IGitHubClient
    {
        AuthenticationType AuthenticationType { get; }
        IConnection Connection { get; }
    }
}
