using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

            [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
                Justification = "The API expects lowercase values")]
            protected override object SerializeEnum(Enum p)
            {
                return p.ToString().ToLowerInvariant();
            }

            // Overridden to handle enums.
            public override object DeserializeObject(object value, Type type)
            {
                var stringValue = value as string;
                if (stringValue != null)
                {
                    if (ReflectionUtils.IsUri(type))
                    {
                        Uri result;
                        if (Uri.TryCreate(stringValue, UriKind.RelativeOrAbsolute, out result))
                        {
                            return result;
                        }
                    }

                    if (ReflectionUtils.GetTypeInfo(type).IsEnum)
                    {
                        // remove '-' from values coming in to be able to enum utf-8
                        stringValue = stringValue.Replace("-", "");
                        return Enum.Parse(type, stringValue, ignoreCase: true);
                    }

                    if (ReflectionUtils.IsNullableType(type))
                    {
                        var underlyingType = Nullable.GetUnderlyingType(type);
                        if (ReflectionUtils.GetTypeInfo(underlyingType).IsEnum)
                        {
                            return Enum.Parse(underlyingType, stringValue, ignoreCase: true);
                        }
                    }
                }

                return base.DeserializeObject(value, type);
            }
        }
    }
}
