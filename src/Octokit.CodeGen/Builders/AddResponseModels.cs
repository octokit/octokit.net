using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

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

            string getPropertyName(string name, bool ensureSingular = false)
            {
                var segments = name.Replace("_", " ").Replace("-", " ").Split(" ");
                var pascalCaseSegments = segments.Select(s => convertToPascalCase(s, ensureSingular));
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

            // first parameter of return type is the current model (needed for assigning to property)
            // second parameter is any additional models that were deserialized
            (ApiModelMetadata, List<ApiModelMetadata>) parseInnerModel(ObjectResponseProperty objectProperty, string classPrefix)
            {
                var additionalModels = new List<ApiModelMetadata>();
                var additionalName = getPropertyName(objectProperty.Name, true);

                // TODO: what if we just skip the prefix here? how far can we
                // get without needing to worry about clashes and denormalizing
                // models?

                var classNamePrefix = $"{classPrefix}{additionalName}";
                var properties = new List<ApiModelProperty>();

                foreach (var property in objectProperty.Properties)
                {
                    var primitiveProperty = property as PrimitiveResponseProperty;
                    if (primitiveProperty != null)
                    {
                        properties.Add(new ApiModelProperty
                        {
                            Name = getPropertyName(primitiveProperty.Name),
                            Type = primitiveProperty.Type,
                        });
                        continue;
                    }

                    var objectResponse = property as ObjectResponseProperty;
                    if (objectResponse != null)
                    {
                        var (current, others) = parseInnerModel(objectResponse, classNamePrefix);
                        additionalModels.Add(current);
                        additionalModels.AddRange(others);
                        properties.Add(new ApiModelProperty
                        {
                            Name = getPropertyName(objectResponse.Name),
                            Type = current.Name,
                        });
                    }

                    // TODO: what about other things here? log or fail fast?
                }

                var top = new ApiModelMetadata
                {
                    Kind = "response",
                    Name = classNamePrefix,
                    Properties = properties,
                };

                return (top, additionalModels);
            }

            List<ApiModelMetadata> parseArrayResponseToModels(ArrayResponseContent arrayContent, HttpMethod method, string statusCode)
            {
                var models = new List<ApiModelMetadata>();

                var classNamePrefix = getClassName(metadata);
                var properties = new List<ApiModelProperty>();

                foreach (var property in arrayContent.ItemProperties)
                {
                    var primitiveProperty = property as PrimitiveResponseProperty;
                    if (primitiveProperty != null)
                    {
                        properties.Add(new ApiModelProperty
                        {
                            Name = getPropertyName(primitiveProperty.Name),
                            Type = primitiveProperty.Type,
                        });
                        continue;
                    }

                    var objectResponse = property as ObjectResponseProperty;
                    if (objectResponse != null)
                    {
                        var (current, others) = parseInnerModel(objectResponse, "");
                        models.Add(current);
                        models.AddRange(others);
                        properties.Add(new ApiModelProperty
                        {
                            Name = getPropertyName(objectResponse.Name),
                            Type = current.Name,
                        });
                    }
                }

                var top = new ApiModelMetadata
                {
                    Kind = "response",
                    Name = classNamePrefix,
                    Properties = properties,
                    Method = method,
                    StatusCode = statusCode,
                };

                models.Add(top);

                return models;
            }

            List<ApiModelMetadata> parseObjectResponseToModels(ObjectResponseContent objectContent, HttpMethod method, string statusCode)
            {
                var models = new List<ApiModelMetadata>();

                var classNamePrefix = getClassName(metadata);
                var properties = new List<ApiModelProperty>();

                foreach (var property in objectContent.Properties)
                {
                    var primitiveProperty = property as PrimitiveResponseProperty;
                    if (primitiveProperty != null)
                    {
                        properties.Add(new ApiModelProperty
                        {
                            Name = getPropertyName(primitiveProperty.Name),
                            Type = primitiveProperty.Type,
                        });
                        continue;
                    }

                    var objectResponse = property as ObjectResponseProperty;
                    if (objectResponse != null)
                    {
                        var (current, others) = parseInnerModel(objectResponse, "");
                        models.Add(current);
                        models.AddRange(others);
                        properties.Add(new ApiModelProperty
                        {
                            Name = getPropertyName(objectResponse.Name),
                            Type = current.Name,
                        });
                    }
                }

                var top = new ApiModelMetadata
                {
                    Kind = "response",
                    Name = classNamePrefix,
                    Properties = properties,
                    Method = method,
                    StatusCode = statusCode,
                };

                models.Add(top);

                return models;
            }

            foreach (var verb in metadata.Verbs)
            {
                foreach (var response in verb.Responses)
                {
                    if (response.ContentType == "application/json")
                    {
                        response.Content.Switch(objectResponse =>
                        {
                            data.Models.AddRange(parseObjectResponseToModels(objectResponse, verb.Method, response.StatusCode));
                        },
                        arrayResponse =>
                        {
                            data.Models.AddRange(parseArrayResponseToModels(arrayResponse, verb.Method, response.StatusCode));
                        });
                    }
                }
            }

            data.Models = data.Models.Distinct(ApiModelCompararer.Default).ToList();

            return data;
        };

    }
}
