using Burr.SimpleJSON;

namespace Burr
{
    public interface IApiObjectMap
    {
        T For<T>(JObject obj);
    }
}
