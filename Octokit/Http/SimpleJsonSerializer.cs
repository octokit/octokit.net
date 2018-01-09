using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Octokit.Reflection;

namespace Octokit.Internal
{
    public class SimpleJsonSerializer : IJsonSerializer
    {
        static readonly GitHubSerializerStrategy _serializationStrategy = new GitHubSerializerStrategy();

        public string Serialize(object item)
        {
            return SimpleJson.SerializeObject(item, _serializationStrategy);
        }

        public T Deserialize<T>(string json)
        {
            return SimpleJson.DeserializeObject<T>(json, _serializationStrategy);
        }

        internal static string SerializeEnum(Enum value)
        {
            return _serializationStrategy.SerializeEnumHelper(value).ToString();
        }

        internal static object DeserializeEnum(string value, Type type)
        {
            return _serializationStrategy.DeserializeEnumHelper(value, type);
        }

        class GitHubSerializerStrategy : PocoJsonSerializerStrategy
        {
            readonly List<string> _membersWhichShouldPublishNull = new List<string>();
            Dictionary<Type, Dictionary<object, object>> _cachedEnums = new Dictionary<Type, Dictionary<object, object>>();

            protected override string MapClrMemberToJsonFieldName(MemberInfo member)
            {
                return member.GetJsonFieldName();
            }

            internal override IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
            {
                var propertiesAndFields = type.GetPropertiesAndFields().Where(p => p.CanSerialize).ToList();

                foreach (var property in propertiesAndFields.Where(p => p.SerializeNull))
                {
                    var key = type.FullName + "-" + property.JsonFieldName;

                    _membersWhichShouldPublishNull.Add(key);
                }

                return propertiesAndFields
                    .ToDictionary(
                        p => p.JsonFieldName,
                        p => p.GetDelegate);
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
                        jsonObject.Add(getter.Key, value);
                    }
                }
                output = jsonObject;
                return true;
            }

            internal object SerializeEnumHelper(Enum p)
            {
                return SerializeEnum(p);
            }

            [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
                Justification = "The API expects lowercase values")]
            protected override object SerializeEnum(Enum p)
            {
                return p.ToParameter();
            }

            internal object DeserializeEnumHelper(string value, Type type)
            {
                if (!_cachedEnums.ContainsKey(type))
                {
                    //First add type to Dictionary
                    _cachedEnums.Add(type, new Dictionary<object, object>());

                    //then try to get all custom attributes, this happens only once per type
                    var fields = type.GetRuntimeFields();
                    foreach (var field in fields)
                    {
                        if (field.Name == "value__")
                            continue;
                        var attribute = (ParameterAttribute)field.GetCustomAttribute(typeof(ParameterAttribute));
                        if (attribute != null)
                        {
                            if (!_cachedEnums[type].ContainsKey(attribute.Value))
                            {
                                var fieldValue = field.GetValue(null);
                                _cachedEnums[type].Add(attribute.Value, fieldValue);
                            }
                        }
                    }
                }
                if (_cachedEnums[type].ContainsKey(value))
                {
                    return _cachedEnums[type][value];
                }
                else
                {
                    //dictionary does not contain enum value and has no custom attribute. So add it for future loops and return value
                    var parsed = Enum.Parse(type, value, ignoreCase: true);
                    _cachedEnums[type].Add(value, parsed);
                    return parsed;
                }
            }

            private string _type;

            // Overridden to handle enums.
            public override object DeserializeObject(object value, Type type)
            {
                var stringValue = value as string;
                var jsonValue = value as JsonObject;

                if (stringValue != null)
                {
                    var typeInfo = ReflectionUtils.GetTypeInfo(type);

                    if (typeInfo.IsEnum)
                    {
                        return DeserializeEnumHelper(stringValue, type);
                    }

                    if (ReflectionUtils.IsNullableType(type))
                    {
                        var underlyingType = Nullable.GetUnderlyingType(type);
                        if (ReflectionUtils.GetTypeInfo(underlyingType).IsEnum)
                        {
                            return DeserializeEnumHelper(stringValue, underlyingType);
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

                    if (typeInfo.IsGenericType)
                    {
                        var typeDefinition = typeInfo.GetGenericTypeDefinition();

                        if (typeof(StringEnum<>).IsAssignableFrom(typeDefinition))
                        {
                            return Activator.CreateInstance(type, stringValue);
                        }
                    }
                }
                else if (jsonValue != null)
                {
                    if (type == typeof(Activity))
                    {
                        _type = jsonValue["type"].ToString();
                    }
                }

                if (type == typeof(ActivityPayload))
                {
                    var payloadType = GetPayloadType(_type);
                    return base.DeserializeObject(value, payloadType);
                }

                return base.DeserializeObject(value, type);
            }

            internal override IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> SetterValueFactory(Type type)
            {
                return type.GetPropertiesAndFields()
                    .Where(p => p.CanDeserialize)
                    .ToDictionary(
                        p => p.JsonFieldName,
                        p => new KeyValuePair<Type, ReflectionUtils.SetDelegate>(p.Type, p.SetDelegate));
            }

            private static Type GetPayloadType(string activityType)
            {
                switch (activityType)
                {
                    case "CommitCommentEvent":
                        return typeof(CommitCommentPayload);
                    case "ForkEvent":
                        return typeof(ForkEventPayload);
                    case "IssueCommentEvent":
                        return typeof(IssueCommentPayload);
                    case "IssuesEvent":
                        return typeof(IssueEventPayload);
                    case "PullRequestEvent":
                        return typeof(PullRequestEventPayload);
                    case "PullRequestReviewCommentEvent":
                        return typeof(PullRequestCommentPayload);
                    case "PushEvent":
                        return typeof(PushEventPayload);
                    case "StatusEvent":
                        return typeof(StatusEventPayload);
                    case "WatchEvent":
                        return typeof(StarredEventPayload);
                }
                return typeof(ActivityPayload);
            }
        }
    }
}
