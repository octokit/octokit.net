using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using OneOf;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiClientFileMetadata, ApiClientFileMetadata>;

    public partial class Builders
    {
        static readonly Func<VerbResult, string> convertVerbToMethodName = (verb) =>
        {
            if (verb.Method == HttpMethod.Get)
            {
                // what about Get with 200 response being a list?
                // this should be GetAll instead to align with our conventions?
                return "Get";
            }

            if (verb.Method == HttpMethod.Delete)
            {
                return "Delete";
            }

            if (verb.Method == HttpMethod.Put)
            {
                return "GetOrCreate";
            }

            return "???";
        };
        static readonly Func<VerbResult, List<ApiParameterMetadata>> convertToParameters = (verb) =>
        {
            var list = new List<ApiParameterMetadata>();

            foreach (var parameter in verb.Parameters.Where(p => p.In == "path" && p.Required))
            {
                var segments = parameter.Name.Replace("_", " ").Replace("-", " ").Split(" ");
                var pascalCaseSegments = segments.Select(s =>
                {
                    if (s.Length < 2)
                    {
                        return s;
                    }
                    else
                    {
                        return Char.ToUpper(s[0]) + s.Substring(1);
                    }
                });
                var parameterName = string.Join("", pascalCaseSegments);
                parameterName = Char.ToLower(parameterName[0]) + parameterName.Substring(1);

                list.Add(new ApiParameterMetadata
                {
                    Name = parameterName,
                    Replaces = parameter.Name,
                    Type = parameter.Type
                });
            }

            return list;
        };

        static readonly Func<VerbResult, List<ApiModelMetadata>, OneOf<TaskOfType, TaskOfListType, UnknownReturnType>> convertToReturnType = (verb, models) =>
        {
            if (verb.Responses.Any(r => r.StatusCode == "204") && verb.Responses.Any(r => r.StatusCode == "204"))
            {
                return new TaskOfType("boolean");
            }

            var singleJsonContent = verb.Responses.SingleOrDefault(r => r.StatusCode == "200" && r.ContentType == "application/json");

            if (singleJsonContent != null)
            {
                var existingModel = models.FirstOrDefault(m => m.Method == verb.Method && m.StatusCode == singleJsonContent.StatusCode);

                return singleJsonContent.Content.Match<OneOf<TaskOfType, TaskOfListType, UnknownReturnType>>(
                  objectContent =>
                  {
                      if (existingModel != null)
                      {
                          return new TaskOfType(existingModel.Name);
                      }

                      // this is a fallback value to catch cases where we aren't handling things
                      // as we should, and should eventually go away
                      return new TaskOfType("SomeObject");
                  },
                  arrayContent =>
                  {
                      if (existingModel != null)
                      {
                          return new TaskOfListType(existingModel.Name);
                      }

                      // this is a fallback value to catch cases where we aren't handling things
                      // as we should, and should eventually go away
                      return new TaskOfListType("SomeList");
                  }
                );
            }

            return new UnknownReturnType();
        };

        public static readonly TypeBuilderFunc AddMethodForEachVerb = (metadata, data) =>
        {
            foreach (var verb in metadata.Verbs)
            {
                data.Client.Methods.Add(new ApiMethodMetadata
                {
                    Name = convertVerbToMethodName(verb),
                    Parameters = convertToParameters(verb),
                    ReturnType = convertToReturnType(verb, data.Models),
                    SourceMetadata = new SourceRouteMetadata
                    {
                        Path = metadata.Path,
                        Verb = verb.Method.Method
                    }
                });
            }

            return data;
        };
    }
}
