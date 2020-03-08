using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using OneOf;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiClientFileMetadata, ApiClientFileMetadata>;

    public class ApiBuilder
    {
        private List<TypeBuilderFunc> funcs = new List<TypeBuilderFunc>();

        public List<ApiClientFileMetadata> Build(List<PathMetadata> paths)
        {
            var results = new List<ApiClientFileMetadata>();

            foreach (var path in paths)
            {
                var result = new ApiClientFileMetadata();

                foreach (var func in funcs)
                {
                    result = func(path, result);
                }

                results.Add(result);
            }

            return results;
        }

        public void Register(TypeBuilderFunc func)
        {
            funcs.Add(func);
        }
    }

    /// <summary>This class
    public class ApiClientFileMetadata
    {
        public ApiClientFileMetadata()
        {
            Client = new ApiClientMetadata();
            Models = new List<ApiModelMetadata>();
        }

        public string FileName { get; set; }
        public ApiClientMetadata Client { get; set; }
        public List<ApiModelMetadata> Models { get; set; }
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

    public class ApiModelMetadata : IEquatable<ApiModelMetadata>
    {
        public ApiModelMetadata()
        {
            Properties = new List<ApiModelProperty>();
        }
        public Guid Id { get; }
        public string Kind { get; set; }
        public string Name { get; set; }
        // this will only be set for the top-level response model
        public HttpMethod Method { get; set; }
        // this will only be set for the top-level response model
        public string StatusCode { get; set; }
        public List<ApiModelProperty> Properties { get; set; }

        public bool Equals([AllowNull] ApiModelMetadata other)
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

    public class ApiModelProperty : IEquatable<ApiModelProperty>
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public bool Equals([AllowNull] ApiModelProperty other)
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

    public class ApiModelCompararer : IEqualityComparer<ApiModelMetadata>
    {
        public static ApiModelCompararer Default => new ApiModelCompararer();

        public bool Equals([AllowNull] ApiModelMetadata x, [AllowNull] ApiModelMetadata y)
        {
            if (object.ReferenceEquals(x, y)) return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

            return x.Kind == y.Kind && x.Name == y.Name && ListEquatable.ScrambledEquals(x.Properties, y.Properties);
        }

        public int GetHashCode([DisallowNull] ApiModelMetadata obj)
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
