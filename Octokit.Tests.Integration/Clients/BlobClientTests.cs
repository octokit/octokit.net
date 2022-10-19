using System;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class BlobClientTests : IDisposable
{
    private readonly IBlobsClient _fixture;
    private readonly RepositoryContext _context;

    public BlobClientTests()
    {
        var github = Helper.GetAuthenticatedClient();
        _fixture = github.Git.Blob;

        _context = github.CreateRepositoryContextWithAutoInit("public-repo").Result;
    }

    [IntegrationTest]
    public async Task CanCreateABlob()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var result = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

        Assert.False(string.IsNullOrWhiteSpace(result.Sha));
    }

    [IntegrationTest]
    public async Task CanCreateABlobWithRepositoryId()
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var result = await _fixture.Create(_context.Repository.Id, blob);

        Assert.False(string.IsNullOrWhiteSpace(result.Sha));
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

        var result = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

        Assert.False(string.IsNullOrWhiteSpace(result.Sha));
    }

    [IntegrationTest]
    public async Task CanCreateABlobWithBase64ContentsAndWithRepositoryId()
    {
        var utf8Bytes = Encoding.UTF8.GetBytes("Hello World!");
        var base64String = Convert.ToBase64String(utf8Bytes);

        var blob = new NewBlob
        {
            Content = base64String,
            Encoding = EncodingType.Base64
        };

        var result = await _fixture.Create(_context.Repository.Id, blob);

        Assert.False(string.IsNullOrWhiteSpace(result.Sha));
    }

    [IntegrationTest]
    public async Task CanGetABlob()
    {
        var newBlob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var result = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newBlob);
        var blob = await _fixture.Get(_context.RepositoryOwner, _context.RepositoryName, result.Sha);

        Assert.Equal(result.Sha, blob.Sha);
        Assert.Equal(EncodingType.Base64, blob.Encoding);

        var contents = Encoding.UTF8.GetString(Convert.FromBase64String(blob.Content));

        Assert.Equal("Hello World!", contents);
    }

    [IntegrationTest]
    public async Task CanGetABlobWithRepositoryId()
    {
        var newBlob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };

        var result = await _fixture.Create(_context.Repository.Id, newBlob);
        var blob = await _fixture.Get(_context.Repository.Id, result.Sha);

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

        var result = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newBlob);
        var blob = await _fixture.Get(_context.RepositoryOwner, _context.RepositoryName, result.Sha);

        Assert.Equal(result.Sha, blob.Sha);
        Assert.Equal(EncodingType.Base64, blob.Encoding);

        // NOTE: it looks like the blobs you get back from the GitHub API
        // will have an additional \n on the end. :cool:!
        var expectedOutput = base64String + "\n";
        Assert.Equal(expectedOutput, blob.Content);
    }

    [IntegrationTest]
    public async Task CanGetABlobWithBase64TextWithRepositoryId()
    {
        var utf8Bytes = Encoding.UTF8.GetBytes("Hello World!");
        var base64String = Convert.ToBase64String(utf8Bytes);

        var newBlob = new NewBlob
        {
            Content = base64String,
            Encoding = EncodingType.Base64
        };

        var result = await _fixture.Create(_context.Repository.Id, newBlob);
        var blob = await _fixture.Get(_context.Repository.Id, result.Sha);

        Assert.Equal(result.Sha, blob.Sha);
        Assert.Equal(EncodingType.Base64, blob.Encoding);

        // NOTE: it looks like the blobs you get back from the GitHub API
        // will have an additional \n on the end. :cool:!
        var expectedOutput = base64String + "\n";
        Assert.Equal(expectedOutput, blob.Content);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
