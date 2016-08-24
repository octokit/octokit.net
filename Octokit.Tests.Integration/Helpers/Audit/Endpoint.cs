namespace Octokit.Tests.Integration
{
    public class Endpoint
    {
        public string Value { get; private set; }
        public bool IsDeprecated { get; private set; }

        public Endpoint(string value, bool isDeprecated)
        {
            Value = value;
            IsDeprecated = isDeprecated;
        }
    }
}
