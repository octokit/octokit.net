namespace Octopi.Tests
{
    public static class Fixtures
    {
        public static EmbeddedResource AuthorizationsJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octopi.Tests.Fixtures.authorizations.json");

        public static EmbeddedResource AuthorizationJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octopi.Tests.Fixtures.authorization.json");

        public static EmbeddedResource UserJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octopi.Tests.Fixtures.user.json");

        public static EmbeddedResource UserFullJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octopi.Tests.Fixtures.user_full.json");

        public static EmbeddedResource RepositoryJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octopi.Tests.Fixtures.repository.json");

        public static EmbeddedResource RepositoriesJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octopi.Tests.Fixtures.repositories.json");
    }
}
