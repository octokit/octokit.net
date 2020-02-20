using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace Octokit.CodeGen
{
    public class PathProcessor
    {
        private static bool TryParse(string verb, out HttpMethod method)
        {
            if (string.Equals(verb, "get", StringComparison.OrdinalIgnoreCase))
            {
                method = HttpMethod.Get;
                return true;
            }

            method = null;
            return false;
        }

        public static PathResult Process(JsonProperty jsonProperty)
        {
            var verbs = new List<VerbResult>();

            foreach (var verbElement in jsonProperty.Value.EnumerateObject())
            {
                var verbName = verbElement.Name;
                HttpMethod method;

                if (!TryParse(verbName, out method))
                {
                    Console.WriteLine($"PathProcessor.TryParse does not handle input {verbName}.");
                    continue;
                }

                var verb = new VerbResult
                {
                    Method = method
                };

                JsonElement parameters;
                if (verbElement.Value.TryGetProperty("parameters", out parameters))
                {
                    foreach (var parameter in parameters.EnumerateArray())
                    {
                        JsonElement nameProp;
                        JsonElement inProp;
                        JsonElement schemaProp;

                        var hasName = parameter.TryGetProperty("name", out nameProp);
                        var hasIn = parameter.TryGetProperty("in", out inProp);
                        var hasSchema = parameter.TryGetProperty("schema", out schemaProp);

                        if (!hasName || !hasIn)
                        {
                            continue;
                        }

                        if (hasSchema)
                        {
                            JsonElement requiredProp;

                            var isRequired = false;
                            if (parameter.TryGetProperty("required", out requiredProp))
                            {
                                isRequired = requiredProp.GetBoolean();
                            }

                            var inString = inProp.GetString();
                            var nameString = nameProp.GetString();

                            if (inString == "header" && nameString == "accept")
                            {
                                JsonElement defaultProp;

                                if (schemaProp.TryGetProperty("default", out defaultProp))
                                {
                                    verb.AcceptHeader = defaultProp.GetString();
                                    continue;
                                }
                            }

                            JsonElement typeProp;
                            if (schemaProp.TryGetProperty("type", out typeProp))
                            {
                                var typeString = typeProp.GetString();
                                verb.Parameters.Add(new Parameter
                                {
                                    Name = nameString,
                                    In = inString,
                                    Required = isRequired,
                                    Type = typeString
                                });

                                continue;
                            }
                        }
                    }
                }

                verbs.Add(verb);
            }

            return new PathResult()
            {
                Path = jsonProperty.Name,
                Verbs = verbs,
            };
        }
    }

    public class PathResult
    {
        public PathResult()
        {
            Verbs = new List<VerbResult>();
        }

        public string Path { get; set; }
        public List<VerbResult> Verbs { get; set; }
    }

    public class VerbResult
    {
        public VerbResult()
        {
            Parameters = new List<Parameter>();
        }

        public HttpMethod Method { get; set; }
        public string AcceptHeader { get; set; }
        public List<Parameter> Parameters { get; set; }
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string In { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
    }
}
