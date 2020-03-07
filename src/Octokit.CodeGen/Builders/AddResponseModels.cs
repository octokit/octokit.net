using System;
using System.Linq;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiClientFileMetadata, ApiClientFileMetadata>;

    public partial class Builders
    {
        public static readonly TypeBuilderFunc AddResponseModels = (metadata, data) =>
        {
            bool isPlaceHolder(string str)
            {
                return str.StartsWith("{") && str.EndsWith("}");
            }

            string singularize(string str)
            {
                if (str.Length < 3)
                {
                    return str;
                }

                if (str.EndsWith("ies"))
                {
                  return str;
                }

                if (str[^1] == 's' && str[^2] != 's')
                {
                    return str.Substring(0, str.Length - 1);
                }

                return str;
            }

            string convertToPascalCase(string s, bool ensureSingular = false)
            {
                if (translations.ContainsKey(s))
                {
                    s = translations[s];
                }

                if (s.Length < 2)
                {
                    return ensureSingular ? singularize(s) : s;
                }
                else
                {
                    var newString = Char.ToUpper(s[0]) + s.Substring(1);
                    return ensureSingular ? singularize(newString) : newString;
                }
            }

            string getPropertyName(IResponseProperty property)
            {
              var segments = property.Name.Replace("_", " ").Replace("-", " ").Split(" ");
              var pascalCaseSegments = segments.Select(s => convertToPascalCase(s, false));
              return string.Join("", pascalCaseSegments);
            }

            string getClassName(PathMetadata metadata)
            {
                var className = "";
                var tokens = metadata.Path.Split("/");
                foreach (var token in tokens)
                {
                    if (token.Length == 0)
                    {
                        continue;
                    }

                    if (isPlaceHolder(token))
                    {
                        continue;
                    }

                    var segments = token.Replace("_", " ").Replace("-", " ").Split(" ");
                    var pascalCaseSegments = segments.Select(s => convertToPascalCase(s, true));
                    className += string.Join("", pascalCaseSegments);
                }

                return className;
            }


            ApiModelProperty convertProperty(IResponseProperty property)
            {
                var primitiveProperty = property as PrimitiveResponseProperty;
                if (primitiveProperty != null)
                {
                    return new ApiModelProperty
                    {
                        Name = getPropertyName(primitiveProperty),
                        Type = primitiveProperty.Type,
                    };
                }

                var objectResponse = property as ObjectResponseProperty;
                if (objectResponse != null)
                {
                    // also need to extract this to a model of your own
                    return new ApiModelProperty
                    {
                        Name = getPropertyName(objectResponse),
                        Type = "???"
                    };
                }

                throw new InvalidOperationException($"Unable to convert property {property.Name} of type {property.Type}. Giving up...");
            }

            foreach (var verb in metadata.Verbs)
            {
                foreach (var response in verb.Responses)
                {
                    if (response.ContentType == "application/json")
                    {
                        if (response.Content.Type == "array")
                        {
                            var arrayResponse = response.Content as ArrayResponseContent;
                            var className = getClassName(metadata);
                            var properties = arrayResponse.ItemProperties.Select(convertProperty).ToList();
                            data.Models.Add(new ApiModelMetadata
                            {
                                Kind = ModelKind.Response,
                                Name = className,
                                Properties = properties,
                            });
                        }
                        else if (response.Content.Type == "object")
                        {
                            var objectResponse = response.Content as ObjectResponseContent;
                            var className = getClassName(metadata);
                            var properties = objectResponse.Properties.Select(convertProperty).ToList();
                            data.Models.Add(new ApiModelMetadata
                            {
                                Kind = ModelKind.Response,
                                Name = className,
                                Properties = properties,
                            });
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unable to convert response type {response.ContentType} for status code {response.StatusCode}. Giving up...");
                        }
                    }
                }
            }

            return data;
        };

    }
}
