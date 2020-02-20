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
                var verb = verbElement.Name;
                HttpMethod method;

                if (TryParse(verb, out method))
                {
                    verbs.Add(new VerbResult
                    {
                        Method = method
                    });
                }
                else
                {
                    Console.WriteLine($"PathProcessor.TryParse does not handle input {verb}.");
                }
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
        public HttpMethod Method { get; set; }
    }
}
