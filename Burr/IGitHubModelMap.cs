using Burr.SimpleJSON;

namespace Burr
{
    public interface IGitHubModelMap
    {
        T For<T>(JObject obj);
    }
}
