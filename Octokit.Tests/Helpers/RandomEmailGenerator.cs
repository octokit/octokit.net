using System;

namespace Octokit.Tests.Helpers
{
    public static class RandomEmailGenerator
    {
        public static string GenerateRandomEmail()
        {
            var randomUsername = Guid.NewGuid().ToString();
            var randomDomain = Guid.NewGuid().ToString();
            
            return $"{randomUsername}@{randomDomain}.com";
        }
    }
}