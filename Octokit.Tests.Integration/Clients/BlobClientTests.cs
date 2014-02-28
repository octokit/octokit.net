using System;
using Octokit;
using Octokit.Tests.Integration;
using System.Threading.Tasks;
using Xunit;
using System.Text;

public class BlobClientTests : IDisposable
{
    readonly IBlobsClient _fixture;
    readonly Repository _repository;
    readonly string _owner;

    public BlobClientTests()
    {
        var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _fixture = client.GitDatabase.Blob;

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _repository = client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest]
    public async Task CanCreateABlob()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var result = await _fixture.Create(_owner, _repository.Name, blob);

        Assert.False(String.IsNullOrWhiteSpace(result.Sha));
    }

    [IntegrationTest]
    public async Task CanCreateABlobWithBase64Contents()
    {
        var utf8Bytes = Encoding.UTF8.GetBytes("Hello World!");
        var base64String = Convert.ToBase64String(utf8Bytes);

        var blob = new NewBlob
        {
            Content = base64String,
            Encoding = EncodingType.Base64
        };

        var result = await _fixture.Create(_owner, _repository.Name, blob);

        Assert.False(String.IsNullOrWhiteSpace(result.Sha));
    }

    [IntegrationTest]
    public async Task CanGetABlob()
    {
        var newBlob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var result = await _fixture.Create(_owner, _repository.Name, newBlob);
        var blob = await _fixture.Get(_owner, _repository.Name, result.Sha);

        Assert.Equal(result.Sha, blob.Sha);
        Assert.Equal(EncodingType.Base64, blob.Encoding);

        var contents = Encoding.UTF8.GetString(Convert.FromBase64String(blob.Content));

        Assert.Equal("Hello World!", contents);
    }

    [IntegrationTest]
    public async Task CanGetABlobWithBase64Text()
    {
        var utf8Bytes = Encoding.UTF8.GetBytes("Hello World!");
        var base64String = Convert.ToBase64String(utf8Bytes);

        var newBlob = new NewBlob
        {
            Content = base64String,
            Encoding = EncodingType.Base64
        };

        var result = await _fixture.Create(_owner, _repository.Name, newBlob);
        var blob = await _fixture.Get(_owner, _repository.Name, result.Sha);

        Assert.Equal(result.Sha, blob.Sha);
        Assert.Equal(EncodingType.Base64, blob.Encoding);

        // NOTE: it looks like the blobs you get back from the GitHub API
        // will have an additional \n on the end. :cool:!
        var expectedOutput = base64String + "\n";
        Assert.Equal(expectedOutput, blob.Content);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
