
namespace Burr.Tests
{
    public static class Fixtures
    {
        public static EmbeddedResource UserJson = 
            new EmbeddedResource(typeof(Fixtures).Assembly, "Burr.Tests.Fixtures.user.json", "user.json");
    }
}
