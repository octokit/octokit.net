﻿using System;
using System.Linq;

namespace Octokit
{
    public class NewReference
    {
        const string _refsPrefix = "refs";

        public NewReference(string reference, string sha)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "ref");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            Ref = GetReference(reference);
            Sha = sha;
        }

        public string Ref { get; private set; }
        public string Sha { get; private set; }

        static string GetReference(string reference)
        {
            var parts = reference.Split('/').ToList();

            var refsPart = parts.FirstOrDefault();
            if (refsPart != null && refsPart != _refsPrefix)
            {
                parts.Insert(0, _refsPrefix);
            }

            if (parts.Count < 3)
            {
                throw new FormatException("Reference must start with 'refs' and have at least two slashes.");
            }

            return string.Join("/", parts);
        }
    }
}