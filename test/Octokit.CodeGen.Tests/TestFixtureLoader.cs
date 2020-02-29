using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

public static class TestFixtureLoader
{
    public static Stream LoadStream(string filename)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var manifestResourceNames = assembly.GetManifestResourceNames();
        return assembly.GetManifestResourceStream($"Octokit.CodeGen.Tests.fixtures.{filename}");
    }

    private static async Task<JsonDocument> LoadFixture(string filename)
    {
        var stream = LoadStream(filename);
        return await JsonDocument.ParseAsync(stream);
    }

    public static Stream LoadPathWithGet()
    {
        return LoadStream("example-get-route.json");
    }

    public static Stream LoadPathWithGetAndPost()
    {
        return LoadStream("example-get-and-post-route.json");
    }

    public static Stream LoadPathWithGetPutAndDelete()
    {
        return LoadStream("example-get-put-delete-route.json");
    }

    public static Stream LoadUserReposEndpoint()
    {
        return LoadStream("user-repos.json");
    }

    public static Stream LoadTopicsRoute()
    {
        return LoadStream("topics-get-put-route.json");
    }
}
