using System;
using System.Collections.Generic;
using Burr.Helpers;
using SimpleJSON;

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
                AvatarUrl = (string)jObj["avatar_url"]
            };
        }
    }
}
