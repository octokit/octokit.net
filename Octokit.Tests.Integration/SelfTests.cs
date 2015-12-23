using Xunit;
using System.Threading.Tasks;
using System.Linq;
using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Octokit.Tests.Integration
{
    /// <summary>
    /// Tests to make sure our tests are ok.
    /// </summary>
    public class SelfTests
    {
        [Fact]
        public void NoTestsUseAsyncVoid()
        {
            var errors = typeof(SelfTests).Assembly.GetAsyncVoidMethodsList();
            Assert.Equal("", errors);
        }

        [Fact]
        public async Task DocumentedApiMatchesImplementation()
        {
            var keywords = new[] { "GET", "DELETE", "PATCH", "POST" };

            var document = new HtmlDocument();

            var first = new HtmlWeb()
                .Load("https://developer.github.com/v3/")
                .DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[2]/div[1]").ChildNodes
                .Select(cn => cn.InnerHtml)
                .First(cn => !string.IsNullOrEmpty(cn.Trim()));

            document.LoadHtml(first);

            var dictionary = document.DocumentNode.SelectNodes("//a")
                .Select(p => new
                {
                    Key = p.GetAttributeValue("href", "not found").Replace("/v3", ""),
                    Value = "https://developer.github.com" + p.GetAttributeValue("href", "not found")
                })
                .Where(p => p.Key.Trim() != "#" & p.Key.Trim() != "/")
                .Where(p => !p.Key.Contains("#"))
                .ToDictionary(p => p.Key, p => p.Value)
                .OrderBy(p => p.Key)
                .Select(kvp => new Section(kvp.Key, kvp.Value));

            foreach (var value in dictionary)
            {
                if (!value.Endpoints.Any())
                {
                    Console.WriteLine(value.key);
                    Console.WriteLine(" # empty");
                }
                else
                {
                    Console.WriteLine(value.key);
                    foreach (var v in value.Endpoints)
                    {
                        var label = v.str.Trim('\n', '\r');
                        Console.WriteLine(" - " + label);
                    }
                }
            }
        }

        public class Section
        {
            readonly string[] keywords = new[] { "GET", "DELETE", "PATCH", "POST" };

            public string key;
            public string url;

            readonly IEnumerable<Endpoint> endpoints;

            public IEnumerable<Endpoint> Endpoints { get { return endpoints; } }

            public Section(string key, string url)
            {
                this.key = key;
                this.url = url;
                
                try
                {
                    endpoints = new HtmlWeb()
                        .Load(url)
                        .DocumentNode.SelectSingleNode("//*[@id=\"wrapper\"]/div[1]").SelectNodes("//pre")
                        .Select(dn => dn.InnerText)
                        .Where(cn => keywords.Contains(Regex.Split(cn, "[^a-zA-Z]+").First() /*split first  word*/))
                        .Select(str => new Endpoint(str))
                        .ToList();
                }
                catch (Exception)
                {
                    endpoints = Enumerable.Empty<Endpoint>();
                }
            }
        }

        public class Endpoint
        {
            public string str;

            public Endpoint(string str)
            {
                this.str = str;
            }
        }
    }
}
