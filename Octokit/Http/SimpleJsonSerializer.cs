using System;
using System.Collections;
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
            readonly List<string> _membersWhichShouldPublishNull
                 = new List<string>();

            protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
            {
                var rubyCased = clrPropertyName.ToRubyCase();
                if (rubyCased == "links") return "_links"; // Special case for GitHub API
                return rubyCased;
            }

            internal override IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
            {
                var fullName = type.FullName + "-";

                // sometimes Octokit needs to send a null with the payload so the user
                // can unset the value of a property.
                // This method uses the same checks as PocoJsonSerializerStrategy
                // to identify the right fields and properties to serialize
                // but it then filters on the presence of SerializeNullAttribute.

                foreach (var propertyInfo in ReflectionUtils.GetProperties(type))
                {
                    if (!propertyInfo.CanRead)
                        continue;
                    var getMethod = ReflectionUtils.GetGetterMethodInfo(propertyInfo);
                    if (getMethod.IsStatic || !getMethod.IsPublic)
                        continue;
                    var attribute = propertyInfo.GetCustomAttribute<SerializeNullAttribute>();
                    if (attribute == null)
                        continue;
                    _membersWhichShouldPublishNull.Add(fullName + MapClrMemberNameToJsonFieldName(propertyInfo.Name));
                }
                foreach (var fieldInfo in ReflectionUtils.GetFields(type))
                {
                    if (fieldInfo.IsStatic || !fieldInfo.IsPublic)
                        continue;
                    var attribute = fieldInfo.GetCustomAttribute<SerializeNullAttribute>();
                    if (attribute == null)
                        continue;
                    _membersWhichShouldPublishNull.Add(fullName + MapClrMemberNameToJsonFieldName(fieldInfo.Name));
                }

                return base.GetterValueFactory(type);
            }

            // This is overridden so that null values are omitted from serialized objects.
            [SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Justification = "Need to support .NET 2")]
            protected override bool TrySerializeUnknownTypes(object input, out object output)
            {
                Ensure.ArgumentNotNull(input, "input");

                var type = input.GetType();
                var jsonObject = new JsonObject();
                var getters = GetCache[type];
                foreach (var getter in getters)
                {
                    if (getter.Value != null)
                    {
                        var value = getter.Value(input);
                        if (value == null)
                        {
                            var key = type.FullName + "-" + getter.Key;
                            if (!_membersWhichShouldPublishNull.Contains(key))
                                continue;
                        }

                        jsonObject.Add(MapClrMemberNameToJsonFieldName(getter.Key), value);
                    }
                }
                output = jsonObject;
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

                    if (ReflectionUtils.IsTypeGenericeCollectionInterface(type))
                    {
                        // OAuth tokens might be a string of comma-separated values
                        // we should only try this if the return array is a collection of strings
                        var innerType = ReflectionUtils.GetGenericListElementType(type);
                        if (innerType.IsAssignableFrom(typeof(string)))
                        {
                            return stringValue.Split(',');
                        }
                    }
                }

                return base.DeserializeObject(value, type);
            }
        }
    }
}
