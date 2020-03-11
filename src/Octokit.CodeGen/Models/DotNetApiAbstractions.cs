using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using OneOf;

namespace Octokit.CodeGen
{
    // This file contains the application-specific representations of the API
    // to translate into source code using Roslyn
    //
    // This represents the opinions about the shape of the API and how things
    // should come together.

    public class ApiClientFileMetadata
    {
        public ApiClientFileMetadata()
        {
            Client = new ApiClientMetadata();
            ResponseModels = new List<ApiResponseModelMetadata>();
            RequestModels = new List<ApiResponseModelMetadata>();
        }

        public string FileName { get; set; }
        public ApiClientMetadata Client { get; set; }
        public List<ApiResponseModelMetadata> ResponseModels { get; set; }
        // TODO: split this out into it's own shape that will suit our needs
        public List<ApiResponseModelMetadata> RequestModels { get; set; }
    }

    public class ApiClientMetadata
    {
        public ApiClientMetadata()
        {
            Methods = new List<ApiMethodMetadata>();
        }
        public string InterfaceName { get; set; }
        public string ClassName { get; set; }
        public List<ApiMethodMetadata> Methods { get; set; }
    }

    public class ApiResponseModelMetadata : IEquatable<ApiResponseModelMetadata>
    {
        public ApiResponseModelMetadata()
        {
            Properties = new List<ApiResponseModelProperty>();
        }
        public string Kind { get; set; }
        public string Name { get; set; }
        // this will only be set for the top-level response model
        public HttpMethod Method { get; set; }
        // this will only be set for the top-level response model
        public string StatusCode { get; set; }
        public List<ApiResponseModelProperty> Properties { get; set; }

        public bool Equals([AllowNull] ApiResponseModelMetadata other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;

            var methodEquals = Method == null ? true : Method.Equals(other.Method);
            var statusCodeEquals = StatusCode == null ? true : StatusCode.Equals(other.StatusCode);

            return Kind.Equals(other.Kind)
              && Name.Equals(other.Name)
              && methodEquals
              && statusCodeEquals
              && ListEquatable.ScrambledEquals(Properties, other.Properties);
        }
    }

    public class ApiResponseModelProperty : IEquatable<ApiResponseModelProperty>
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public bool Equals([AllowNull] ApiResponseModelProperty other)
        {
            if (Object.ReferenceEquals(other, null)) return false;

            if (Object.ReferenceEquals(this, other)) return true;

            return Name.Equals(other.Name) && Type.Equals(other.Type);
        }

        public override int GetHashCode()
        {
            int hashName = Name == null ? 0 : Name.GetHashCode();

            int hashType = Type == null ? 0 : Type.GetHashCode();

            return hashName ^ hashType;
        }
    }

    public class ApiMethodMetadata
    {
        public ApiMethodMetadata()
        {
            Parameters = new List<ApiParameterMetadata>();
        }
        public string Name { get; set; }
        public List<ApiParameterMetadata> Parameters { get; set; }
        public OneOf<TaskOfType, TaskOfListType, UnknownReturnType> ReturnType { get; set; }
        public SourceRouteMetadata SourceMetadata { get; set; }
    }

    public class ApiParameterMetadata
    {
        public string Name { get; set; }
        public string Replaces { get; set; }
        public string Type { get; set; }
    }

    public class TaskOfType
    {
        public TaskOfType(string type)
        {
            Type = type;
        }
        public string Type { get; private set; }
    }

    public class TaskOfListType
    {
        public TaskOfListType(string listType)
        {
            ListType = listType;
        }
        public string ListType { get; private set; }
    }

    public class UnknownReturnType
    {
    }

    public class SourceRouteMetadata
    {
        public string Verb { get; set; }
        public string Path { get; set; }
    }

    public class ApiModelCompararer : IEqualityComparer<ApiResponseModelMetadata>
    {
        public static ApiModelCompararer Default => new ApiModelCompararer();

        public bool Equals([AllowNull] ApiResponseModelMetadata x, [AllowNull] ApiResponseModelMetadata y)
        {
            if (object.ReferenceEquals(x, y)) return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

            return x.Kind == y.Kind && x.Name == y.Name && ListEquatable.ScrambledEquals(x.Properties, y.Properties);
        }

        public int GetHashCode([DisallowNull] ApiResponseModelMetadata obj)
        {
            int hash = 13;
            hash = (hash * 7) + obj.Kind.GetHashCode();
            hash = (hash * 7) + obj.Name.GetHashCode();
            hash = (hash * 7) + ListEquatable.GetSequenceHashCode(obj.Properties);
            return hash;
        }
    }
    public static class ListEquatable
    {
        // taken from https://stackoverflow.com/a/3670089/1363815
        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        // sourced from https://stackoverflow.com/a/48192420/1363815
        public static int GetSequenceHashCode<TItem>(this IEnumerable<TItem> list)
        {
            if (list == null) return 0;
            const int seedValue = 0x2D2816FE;
            const int primeNumber = 397;
            return list.Aggregate(seedValue, (current, item) => (current * primeNumber) + (Equals(item, default(TItem)) ? 0 : item.GetHashCode()));
        }
    }
}
