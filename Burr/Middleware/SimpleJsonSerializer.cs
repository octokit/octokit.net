using System;
using Burr.Helpers;

namespace Burr.Middleware
{
    public class SimpleJsonSerializer : IJsonSerializer
    {
        readonly GitHubSerializerStrategy serializationStrategy = new GitHubSerializerStrategy();
        public string Serialize(object item)
        {
            return SimpleJson.SerializeObject(item, serializationStrategy);
        }

        public T Deserialize<T>(string json)
        {
            return SimpleJson.DeserializeObject<T>(json, serializationStrategy);
        }

        class GitHubSerializerStrategy : PocoJsonSerializerStrategy
        {
            protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
            {
                return clrPropertyName.ToRubyCase();
            }
        }
    }
}
