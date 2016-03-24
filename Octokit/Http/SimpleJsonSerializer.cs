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
            readonly List<string> _membersWhichShouldPublishNull = new List<string>();

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

            [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
                Justification = "The API expects lowercase values")]
            protected override object SerializeEnum(Enum p)
            {
                return p.ToString().ToLowerInvariant();
            }

            private string _type;

            // Overridden to handle enums.
            public override object DeserializeObject(object value, Type type)
            {
                var stringValue = value as string;
                var jsonValue = value as JsonObject;
                if (stringValue != null)
                {
                    if (ReflectionUtils.GetTypeInfo(type).IsEnum)
                    {
                        // remove '-' from values coming in to be able to enum utf-8
                        stringValue = RemoveHyphenAndUnderscore(stringValue);
                        return Enum.Parse(type, stringValue, ignoreCase: true);
                    }

                    if (ReflectionUtils.IsNullableType(type))
                    {
                        var underlyingType = Nullable.GetUnderlyingType(type);
                        if (ReflectionUtils.GetTypeInfo(underlyingType).IsEnum)
                        {
                            stringValue = RemoveHyphenAndUnderscore(stringValue);
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

            static string RemoveHyphenAndUnderscore(string stringValue)
            {
                // remove '-' from values coming in to be able to enum utf-8
                stringValue = stringValue.Replace("-", "");
                // remove '-' from values coming in to be able to enum EventInfoState names with underscores in them. Like "head_ref_deleted" 
                stringValue = stringValue.Replace("_", "");
                return stringValue;
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
                    case "WatchEvent":
                        return typeof(StarredEventPayload);
                }
                return typeof(ActivityPayload);
            }
        }
    }
}
