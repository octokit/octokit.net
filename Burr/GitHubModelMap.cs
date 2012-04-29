using System;
using System.Collections.Generic;
using Burr.Helpers;
using Burr.SimpleJSON;

namespace Burr
{
    public class GitHubModelMap : IGitHubModelMap
    {
        static Dictionary<Type, Func<JObject, object>> toObjects = new Dictionary<Type, Func<JObject, object>>();
        static Dictionary<Type, Func<object, JObject>> fromObjects = new Dictionary<Type, Func<object, JObject>>();

        static GitHubModelMap()
        {
            toObjects.Add(typeof(User), JObjectToUser);

            fromObjects.Add(typeof(User), x => UserToJObject((User)x));
        }

        public T For<T>(JObject obj)
        {
            Ensure.ArgumentNotNull(obj, "obj");

            return (T)toObjects[typeof(T)](obj);
        }

        public JObject For(object obj)
        {
            Ensure.ArgumentNotNull(obj, "obj");

            return fromObjects[obj.GetType()](obj);
        }

        public static JObject UserToJObject(User user)
        {
            var dict = new Dictionary<string, JObject>();

            if (user.Name.IsNotBlank())
                dict.Add("name", JObject.CreateString(user.Name));

            //{
            //    {"Name", JObject.CreateString(user.Name) },
            //    {"Email", JObject.CreateString(user.Email) },
            //    {"Blog", JObject.CreateString(user.Blog) },
            //    {"Company", JObject.CreateString(user.Company) },
            //    {"Location", JObject.CreateString(user.Location) },
            //    {"Hireable", JObject.CreateBoolean(user.Hireable) },
            //    {"Bio", JObject.CreateString(user.Bio) },
            //}
            return JObject.CreateObject(dict);
        }

        public static User JObjectToUser(JObject jObj)
        {
            var user = new User
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
                Login = (string)jObj["login"],
            };

            if (jObj.ObjectValue.ContainsKey("plan"))
            {
                user.Plan = new Plan
                {
                    Collaborators = (int)jObj["plan"]["collaborators"],
                    Name = (string)jObj["plan"]["name"],
                    Space = (int)jObj["plan"]["space"],
                    PrivateRepos = (int)jObj["plan"]["private_repos"],
                };
            }

            return user;
        }
    }
}
