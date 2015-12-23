using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Octokit.Tests.Integration
{
    public static class WebsiteScraper
    {
        public static IEnumerable<Section> GetListOfDocumentedApis()
        {
            var keywords = new[] { "GET", "DELETE", "PATCH", "POST", "PUT" };

            var document = new HtmlDocument();

            var first = new HtmlWeb()
                .Load("https://developer.github.com/v3/")
                .DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[2]/div[1]").ChildNodes
                .Select(cn => cn.InnerHtml)
                .First(cn => !string.IsNullOrEmpty(cn.Trim()));

            document.LoadHtml(first);

            return document.DocumentNode.SelectNodes("//a")
                .Select(p => new
                {
                    Key = p.GetAttributeValue("href", "not found").Replace("/v3", ""),
                    Value = "https://developer.github.com" + p.GetAttributeValue("href", "not found")
                })
                .Where(p => p.Key.Trim() != "#" & p.Key.Trim() != "/")
                .Where(p => !p.Key.Contains("#"))
                .Where(p => !p.Key.Contains("/legacy"))
                .ToDictionary(p => p.Key, p => p.Value)
                .OrderBy(p => p.Key)
                .Select(kvp => new Section(kvp.Key, kvp.Value));
        }
    }
}
