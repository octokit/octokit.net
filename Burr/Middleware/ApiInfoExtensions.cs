using System;
using System.Text.RegularExpressions;

namespace Burr.Http
{
    public static class ApiInfoExtensions
    {
        public static int GetLastPage(this ApiInfo info)
        {
            if (!info.Links.ContainsKey("last")) return -1;

            var last = info.Links["last"].ToString();
            var pageParam = new Regex("[^_]page=[0-9]+").Match(last).Value;
            var lastPage = new Regex("[0-9]+").Match(pageParam).Value.ToInt32();

            return lastPage;
        }

        static Int32 ToInt32(this string s)
        {
            Int32 val;
            return Int32.TryParse(s, out val) ? val : 0;
        }
    }
}
