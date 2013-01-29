namespace Octokit.Tests
{
    public static class Fixtures
    {
        public static EmbeddedResource AuthorizationsJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octokit.Tests.Fixtures.authorizations.json");

        public static EmbeddedResource AuthorizationJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octokit.Tests.Fixtures.authorization.json");

        public static EmbeddedResource UserJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octokit.Tests.Fixtures.user.json");

        public static EmbeddedResource UserFullJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octokit.Tests.Fixtures.user_full.json");

        public static EmbeddedResource RepositoryJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octokit.Tests.Fixtures.repository.json");

        public static EmbeddedResource RepositoriesJson =
            new EmbeddedResource(typeof(Fixtures).Assembly, "Octokit.Tests.Fixtures.repositories.json");
    }
}
