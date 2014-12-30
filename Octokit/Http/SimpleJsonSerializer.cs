using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Octokit.Helpers;
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
            readonly List<string> membersWhichShouldPublishNull = new List<string>();
            readonly List<string> membersWhichShouldBeBase64Encoded = new List<string>();

            protected override string MapClrMemberToJsonFieldName(MemberInfo member)
            {
                var memberName = member.Name;
                var paramAttr = member.GetCustomAttribute<ParameterAttribute>();

                if (paramAttr != null && !string.IsNullOrEmpty(paramAttr.Key))
                {
                    memberName = paramAttr.Key;
                }

                return memberName.ToRubyCase();
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
                    var base64Attribute = propertyInfo.GetCustomAttribute<SerializeAsBase64Attribute>();
                    if (base64Attribute != null)
                    {
                        membersWhichShouldBeBase64Encoded.Add(fullName + MapClrMemberToJsonFieldName(propertyInfo));
                    }
                    var serializeNullAttribute = propertyInfo.GetCustomAttribute<SerializeNullAttribute>();
                    if (serializeNullAttribute == null)
                        continue;
                    membersWhichShouldPublishNull.Add(fullName + MapClrMemberToJsonFieldName(propertyInfo));
                }

                foreach (var fieldInfo in ReflectionUtils.GetFields(type))
                {
                    if (fieldInfo.IsStatic || !fieldInfo.IsPublic)
                        continue;
                    var base64Attribute = fieldInfo.GetCustomAttribute<SerializeAsBase64Attribute>();
                    if (base64Attribute != null)
                    {
                        membersWhichShouldBeBase64Encoded.Add(fullName + MapClrMemberToJsonFieldName(fieldInfo));
                    }
                    var attribute = fieldInfo.GetCustomAttribute<SerializeNullAttribute>();
                    if (attribute == null)
                        continue;
                    membersWhichShouldPublishNull.Add(fullName + MapClrMemberToJsonFieldName(fieldInfo));
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
                            if (!membersWhichShouldPublishNull.Contains(key))
                                continue;
                        }
                        else
                        {
                            var key = type.FullName + "-" + getter.Key;
                            if (membersWhichShouldBeBase64Encoded.Contains(key))
                            {
                                var stringValue = value as string ?? "";
                                value = Convert.ToBase64String(Encoding.UTF8.GetBytes(stringValue));
                            }
                        }
                        jsonObject.Add(getter.Key, value);
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

                var deserialized = base.DeserializeObject(value, type);

                // Handle base64 encoding
                foreach (var propertyInfo in ReflectionUtils.GetProperties(type))
                {
                    if (!propertyInfo.CanRead) continue;
                    if (!propertyInfo.CanWrite) continue;
                    if (propertyInfo.GetCustomAttribute<SerializeAsBase64Attribute>() != null)
                    {
                        var propertyValue = propertyInfo.GetValue(deserialized) as string;
                        if (propertyValue != null)
                        {
                            var unencoded = Encoding.UTF8.GetString(Convert.FromBase64String(propertyValue));
                            propertyInfo.SetValue(deserialized, unencoded);
                        }
                    }
                }

                foreach (var fieldInfo in ReflectionUtils.GetFields(type))
                {
                    if (fieldInfo.GetCustomAttribute<SerializeAsBase64Attribute>() != null)
                    {
                        var propertyValue = fieldInfo.GetValue(deserialized) as string;
                        if (propertyValue != null)
                        {
                            var unencoded = Encoding.UTF8.GetString(Convert.FromBase64String(propertyValue));
                            fieldInfo.SetValue(deserialized, unencoded);
                        }
                    }
                }

                return deserialized;
            }
        }
    }
}
