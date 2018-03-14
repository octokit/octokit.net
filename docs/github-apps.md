# Working with GitHub Apps

## Overview
GitHub Apps are a new type of integration offering narrow, specific permissions, compared to OAuth apps or user authentication.

To learn more, head to the GitHub Apps section under the GitHub Developer [Getting started with Building Apps](https://developer.github.com/apps/getting-started-with-building-apps/#using-github-apps) documentation.

A GitHub App (known in Octokit as a `GitHubApp`) specifies permissions (read, write, none) it will be granted for various scopes and also registers for various webhook events.

A GitHub App is installed in an `Organization` or `User` account (known in Octokit as an `Installation`) where it is further limited to nominated (or all) repositories for that account.

The [GitHub Api Documentation](https://developer.github.com/v3/apps/) on GitHub Apps contains more detailed information.

## Authentication

The below walkthrough outlines how to authenticate as a `GitHubApp` and an `Installation` using Octokit.net.

Be sure to see the  [GitHub Api Documentation](https://developer.github.com/apps/building-github-apps/authentication-options-for-github-apps/#authentication-options-for-github-apps) on GitHub Apps authentication, if you require more details.

## GitHub App Walkthrough

Each GitHub App has a private certificate (PEM file) generated through the GitHub website and possessed by the owner of the GitHub App.  Authentication occurs using a time based JWT token, signed by the GitHub App's private certificate.

``` csharp
// A time based JWT token, signed by the GitHub App's private certificate
var jwtToken = @"eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE1MjA0Mjc3MTQsImV4cCI6MTUyMDQyODMxNCwiaXNzIjo5NzM1fQ.K-d3FKWKddMygFqvPZYWQusqhbF1LYfcIM0VbBq4uJsS9VkjhyXALlHmTJWjdblzx-U55lkZc_KWdJd6GlDxvoRb5w_9nrLcIFRbYVgi9XTYpCc3o5j7Qh3FvKxA1bzEs8XGrxjjE7-WJn_xi85ugFKTy9tlIRPa-PHeIOvNp4fz4ru8SFPoD4epiraeEyLfpU_ke-HYF7Ws7ar19zQkfJKRHSIFm1LxJ5MGKWT8pQBBUSGxGPgEG_tYI83aYw6cVx-DLV290bpr23LRUC684Wv_XabUDzXjPUYynAc01APZF6aN8B0LHdPbG8I6Yd74sQfmN-aHz5moz8ZNWLNm8Q
@";

var gitHubClient = new GitHubClient(new ProductHeaderValue("MyApp"))
{
    Credentials = new Credentials(jwtToken, AuthenticationType.Bearer)
};

var appsClient = gitHubClient.GitHubApps;
```

The authenticated app can query various top level information about itself

``` csharp
// Get the current authenticated GitHubApp
var app = await appClient.GetCurrent();

// Get a list of installations for the authenticated GitHubApp
var installations = await appClient.GetAllInstallationsForCurrent();

// Get a specific installation of the authenticated GitHubApp by it's installation Id
var installation = await appClient.GetInstallation(123);

```

In order to do much more, a GitHubApp needs to create a temporary installation token for a specific Installation Id, and use that as further authentication:

``` csharp
// Create an Installation token for Insallation Id 123
var response = await appClient.CreateInstallationToken(123);

// The token will expire!
response.ExpiresAt;

// Create a new GitHubClient using the installation token as authentication
var installationClient = new GitHubClient(new ProductHeaderValue("MyApp-Installation123"))
{
    Credentials = new Credentials(response.Token)
};
```

Once authenticated as an `Installation`, a [subset of regular API endpoints](https://developer.github.com/v3/apps/available-endpoints/) can be queried, using  the permissions of the `GitHubApp` and repository settings of the `Installation`:

``` csharp
// Create a Comment on an Issue
// - Assuming the GitHub App has read/write permission for issues scope
// - Assuming we are operating on a repository that the Installation has access to
var response = await installationClient.Issue.Comment.Create("owner", "repo", 1, "Hello from my GitHubApp Installation!");
```

## A Note on identifying Installation Id's
GitHub Apps can be registered for webhook events.

WebHook payloads sent to these registrations now include an extra field to indicate the Id of the GitHub App Installation that is associated with the received webhook.

Example webhook for an opened Pull Request:
``` json
{
    "action": "opened",
    "number": 1,
    "pull_request": {
        ...
    },
    "repository": {
        ...
    },
    "sender": {
        ...
    },
    "installation": {
        "id": 1234
    }
}
```

You can retrieve this `installation.id` from your webhook payload, and use it to create the Installation token as above, to further interact with the repository as that Installation of the GitHub App.

Although Octokit.net doesn't have explicit webhook support, the `ActivityPayload` response classes do generally align with webhook payloads (assuming the octokit.net custom deserializer is used), so we have added the field to these:

``` csharp
// json payload from the received webhook
var json = "...";

// Deserialize the pull_request event
var serializer = new Octokit.Internal.SimpleJsonSerializer();
var payload = serializer_.Deserialize<PullRequestEventPayload>(json);

// Create an Installation token for the associated Insallation Id
var response = await appClient.CreateInstallationToken(payload.Installation.Id);
```

## A Note on JWT Tokens
Octokit.net expects that you will pass in the appropriately signed JWT token required to authenticate the `GitHubApp`.

Luckily one of our contributors [@adriangodong](https://github.com/adriangodong) has created a library `GitHubJwt` ([GitHub](https://github.com/adriangodong/githubjwt) [NuGet.org](https://www.nuget.org/packages/githubjwt)) which you can use to help with this (as long as you are targetting `netstandard2.0` or above).

``` csharp
var generator = new GitHubJwt.GitHubJwtFactory(
    new GitHubJwt.FilePrivateKeySource("/path/to/pem_file"),
    new GitHubJwt.GitHubJwtFactoryOptions
    {
        AppIntegrationId = 123, // The GitHub App Id
        ExpirationSeconds = 600 // 10 minutes is the maximum time allowed
    }
);

var jwtToken = generator.CreateEncodedJwtToken();
```