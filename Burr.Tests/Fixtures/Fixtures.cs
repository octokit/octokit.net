
namespace Burr.Tests
{
    public static class Fixtures
    {
        public static EmbeddedResource AuthorizationsJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Burr.Tests.Fixtures.authorizations.json");

        public static EmbeddedResource AuthorizationJson =
           new EmbeddedResource(typeof(Fixtures).Assembly, "Burr.Tests.Fixtures.authorization.json");

        public static EmbeddedResource UserJson = 
            new EmbeddedResource(typeof(Fixtures).Assembly, "Burr.Tests.Fixtures.user.json");

        public static EmbeddedResource UserFullJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Burr.Tests.Fixtures.user_full.json");
    }
}
