using System;
using System.Collections.Generic;
using Burr.Helpers;
using Burr.SimpleJSON;

namespace Burr
{
    public class ApiObjectMap : IApiObjectMap
    {
        static Dictionary<Type, Func<JObject, object>> maps = new Dictionary<Type, Func<JObject, object>>();

        static ApiObjectMap()
        {
            maps.Add(typeof(User), ForUser);
        }

        public T For<T>(JObject obj)
        {
            Ensure.ArgumentNotNull(obj, "obj");

            return (T)maps[typeof(T)](obj);
        }

        public static User ForUser(JObject jObj)
        {
            return new User
            {
                Followers = (int)jObj["followers"],
                Type = (string)jObj["type"],
                Hireable = (bool)jObj["hireable"],
                AvatarUrl = (string)jObj["avatar_url"],
                Bio = (string)jObj["bio"],
                HtmlUrl = (string)jObj["html_url"],
                CreatedAt = DateTimeOffset.Parse((string)jObj["created_at"]),
                PublicRepos = (int)jObj["public_repos"],
                Blog = (string)jObj["blog"],
                Url = (string)jObj["url"],
                PublicGists = (int)jObj["public_gists"],
                Following = (int)jObj["following"],
                Company = (string)jObj["company"],
                Name = (string)jObj["name"],
                Location = (string)jObj["location"],
                Id = (int)jObj["id"],
                Email = (string)jObj["email"],
                Login = (string)jObj["login"]
            };
        }
    }
}
