using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

public static class TestFixtureLoader
{
    private static async Task<JsonDocument> LoadFixture(string filename)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var manifestResourceNames = assembly.GetManifestResourceNames();
        var stream = assembly.GetManifestResourceStream($"Octokit.CodeGen.Tests.fixtures.{filename}");
        return await JsonDocument.ParseAsync(stream);
    }

    public static async Task<JsonProperty> LoadPathWithGet()
    {
        var json = await LoadFixture("example-get-route.json");
        var paths = json.RootElement.GetProperty("paths");
        var properties = paths.EnumerateObject();
        var firstPath = properties.ElementAt(0);
        return firstPath;
    }

    public static async Task<JsonProperty> LoadPathWithGetAndPost()
    {
        var json = await LoadFixture("example-get-and-post-route.json");
        var paths = json.RootElement.GetProperty("paths");
        var properties = paths.EnumerateObject();
        var firstPath = properties.ElementAt(0);
        return firstPath;
    }

    public static async Task<JsonProperty> LoadPathWithGetPutAndDelete()
    {
        var json = await LoadFixture("example-get-put-delete-route.json");
        var paths = json.RootElement.GetProperty("paths");
        var properties = paths.EnumerateObject();
        var firstPath = properties.ElementAt(0);
        return firstPath;
    }

    public static async Task<JsonProperty> LoadUserReposEndpoint()
    {
        var json = await LoadFixture("user-repos.json");
        var paths = json.RootElement.GetProperty("paths");
        var properties = paths.EnumerateObject();
        var firstPath = properties.ElementAt(0);
        return firstPath;
    }

    public static async Task<JsonProperty> LoadTopicsRoute()
    {
        var json = await LoadFixture("topics-get-put-route.json");
        var paths = json.RootElement.GetProperty("paths");
        var properties = paths.EnumerateObject();
        var firstPath = properties.ElementAt(0);
        return firstPath;
    }
}
