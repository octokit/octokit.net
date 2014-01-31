using System;

namespace Octokit
{
    public class Emoji
    {
        public Emoji(string name, Uri url)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(url, "url");

            Name = name;
            Url = url;
        }

        public string Name { get; private set; }
        public Uri Url { get; private set; }
    }
}
