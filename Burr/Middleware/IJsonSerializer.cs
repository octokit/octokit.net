namespace Burr.Middleware
{
    public interface IJsonSerializer
    {
        string Serialize(object item);
        T Deserialize<T>(string json);
    }
}
