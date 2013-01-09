using Nocto.Http;

namespace Nocto
{
    public interface IGitHubClient
    {
        AuthenticationType AuthenticationType { get; }
        IConnection Connection { get; }
    }
}
