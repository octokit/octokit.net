using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Octokit.Reflection;

namespace Octokit.Internal
{
    public class SimpleJsonSerializer : IJsonSerializer
    {
        readonly GitHubSerializerStrategy _serializationStrategy = new GitHubSerializerStrategy();

        public string Serialize(object item)
        {
            return SimpleJson.SerializeObject(item, _serializationStrategy);
        }

        public T Deserialize<T>(string json)
        {
            return SimpleJson.DeserializeObject<T>(json, _serializationStrategy);
        }

        class GitHubSerializerStrategy : PocoJsonSerializerStrategy
        {
            protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
            {
                return clrPropertyName.ToRubyCase();
            }

            // This is overridden so that null values are omitted from serialized objects.
            [SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Justification = "Need to support .NET 2")]
            protected override bool TrySerializeUnknownTypes(object input, out object output)
            {
                if (input == null) throw new ArgumentNullException("input");
                output = null;
                Type type = input.GetType();
                if (type.FullName == null)
                    return false;
                IDictionary<string, object> obj = new JsonObject();
                IDictionary<string, ReflectionUtils.GetDelegate> getters = GetCache[type];
                foreach (KeyValuePair<string, ReflectionUtils.GetDelegate> getter in getters)
                {
                    if (getter.Value != null)
                    {
                        var value = getter.Value(input);
                        if (value == null)
                            continue;

                        obj.Add(MapClrMemberNameToJsonFieldName(getter.Key), value);
                    }
                }
                output = obj;
                return true;
            }
        }
    }
}
