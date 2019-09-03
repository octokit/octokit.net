# Working with GitHub Apps

## Introduction
GitHub Apps are a new type of integration offering narrow, specific permissions, compared to OAuth apps or user authentication.

:warning: Please ensure to follow the links to the official GitHub Developer documentation where they are referenced, as we do not want to include all of the detail in this walkthrough (and run the risk of it becoming out of date or incorrect)

To learn more about GitHub Apps, head to the GitHub Apps section under the GitHub Developer [Getting started with Building Apps](https://developer.github.com/apps/getting-started-with-building-apps/#using-github-apps) documentation.

## Overview

A GitHub App (known in Octokit as a `GitHubApp`) is a global entity on GitHub, that specifies permissions (read, write, none) it will be granted for various scopes and additionally defines a list of webhook events the app will be interested in.

An "instance" of a GitHub App is then installed in an `Organization` or `User` account (known in Octokit as an `Installation`) where it is further limited to nominated (or all) repositories for that account.

An `Installation` of a `GitHubApp`, thus operates at the "intersection" between the globally defined permissions/scopes/webhooks of the GitHub App itself PLUS the Organization/User repositories that were nominated.

The [GitHub Api Documentation](https://developer.github.com/v3/apps/) on GitHub Apps contains more detailed information.

## Authentication

Authentication for GitHub Apps is reasonably complicated, as there are a few moving parts to take into account.

The below walkthrough outlines how to use Octokit.net to
- Authenticate as the `GitHubApp` itself using a temporary JWT token
- Query top level endpoints as the `GitHubApp`
- Generate a temporary Installation Token as the `GitHubApp`, to allow further authentication as a specific `Installation` of the `GitHubApp`
- Access specific endpoints as the `Installation`

Be sure to read the  [GitHub Api Documentation](https://developer.github.com/apps/building-github-apps/authenticating-with-github-apps) on GitHub Apps authentication, before proceeding!

## GitHub App Walkthrough

Each GitHub App has a private certificate (PEM file) which is [generated via the GitHub website](https://developer.github.com/apps/building-github-apps/authenticating-with-github-apps/#generating-a-private-key) by the owner of the GitHub App.  Wherever the owner decides to host the GitHub App, it would need access to this private certificate, as it is the entry point to authentication with GitHub.

The first step in the authentication process, is to generate a temporary JWT token, signed by the GitHub App's private certificate.  It also needs to include the GitHub App's unique Id, which is obtainable from the GitHub website.

:bulb: There are several ways to generate JWT tokens in .NET and this library aims to have minimal dependencies on other libraries.  Therefore the expectation is that your app will create the JWT token however you see fit, and pass it in to Octokit.net.  The example below contains a hardcoded JWT token string as an example.  See the Additional Notes section for one recommended library, to generate the JWT token.

:warning: GitHub enforces that the JWT token used can only be valid for a maximum of 10 minutes - a new token will be required after this time.  In the future, Octokit.net may provide hooks/helpers to help you take care of this, but for now your application will need to handle this itself.

``` csharp
// A time based JWT token, signed by the GitHub App's private certificate
var jwtToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE1MjA0Mjc3MTQsImV4cCI6MTUyMDQyODMxNCwiaXNzIjo5NzM1fQ.K-d3FKWKddMygFqvPZYWQusqhbF1LYfcIM0VbBq4uJsS9VkjhyXALlHmTJWjdblzx-U55lkZc_KWdJd6GlDxvoRb5w_9nrLcIFRbYVgi9XTYpCc3o5j7Qh3FvKxA1bzEs8XGrxjjE7-WJn_xi85ugFKTy9tlIRPa-PHeIOvNp4fz4ru8SFPoD4epiraeEyLfpU_ke-HYF7Ws7ar19zQkfJKRHSIFm1LxJ5MGKWT8pQBBUSGxGPgEG_tYI83aYw6cVx-DLV290bpr23LRUC684Wv_XabUDzXjPUYynAc01APZF6aN8B0LHdPbG8I6Yd74sQfmN-aHz5moz8ZNWLNm8Q";

// Use the JWT as a Bearer token
var appClient = new GitHubClient(new ProductHeaderValue("MyApp"))
{
    Credentials = new Credentials(jwtToken, AuthenticationType.Bearer)
};
```

Now we have an authenticated `GitHubApp` (`appClient`), we can query various top level information about the `GitHubApp` itself:

``` csharp
// Get the current authenticated GitHubApp
var app = await appClient.GitHubApps.GetCurrent();

// Get a list of installations for the authenticated GitHubApp
var installations = await appClient.GitHubApps.GetAllInstallationsForCurrent();

// Get a specific installation of the authenticated GitHubApp by it's installation Id
var installation = await appClient.GitHubApps.GetInstallation(123);

```

In order to do more than top level calls, a `GitHubApp` needs to authenticate as a specific `Installation` by creating a temporary Installation Token (currently these expire after 1 hour), and using that for authentication. 

:bulb: The example below includes a hardcoded Installation Id, but this would typically come from a webhook payload (so a GitHub App knows which Installation it needs to authenticate as, to deal with the received webhook).  See the Additional Notes section for more details on Installation Id's in webhooks.

:warning: These temporary Installation Tokens are only valid for 1 hour, and a new Installation Token will be required after this time.  In the future, Octokit.net may provide hooks/helpers to help you take care of this, but for now your application will need to handle this itself.

``` csharp
// Create an Installation token for Insallation Id 123
var response = await appClient.GitHubApps.CreateInstallationToken(123);

// NOTE - the token will expire in 1 hour!
response.ExpiresAt;

// Create a new GitHubClient using the installation token as authentication
var installationClient = new GitHubClient(new ProductHeaderValue("MyApp-Installation123"))
{
    Credentials = new Credentials(response.Token)
};
```

Once authenticated as an `Installation`, a [subset of regular API endpoints](https://developer.github.com/v3/apps/available-endpoints/) can be accessed, using the intersection of the permissions/scopes of the `GitHubApp` and the repositories nominated by the User/Organization as part of the `Installation`:

``` csharp
// Create a Comment on an Issue
// - Assuming the GitHub App has read/write permission for issues scope
// - Assuming we are operating on a repository that the Installation has access to
var response = await installationClient.Issue.Comment.Create("owner", "repo", 1, "Hello from my GitHubApp Installation!");
```

That concludes the walkthrough!

## Additional Notes

### A Note on JWT Tokens
Octokit.net aims to have no external dependencies, therefore we do not currently have the ability to generate/sign JWT tokens for you, and instead expect that you will pass in the appropriately signed JWT token required to authenticate the `GitHubApp`.

Luckily one of our contributors [@adriangodong](https://github.com/adriangodong) has created a library `GitHubJwt` ( [GitHub](https://github.com/adriangodong/githubjwt) | [NuGet](https://www.nuget.org/packages/githubjwt) ) which you can use as per the following example.

``` csharp
// Use GitHubJwt library to create the GitHubApp Jwt Token using our private certificate PEM file
var generator = new GitHubJwt.GitHubJwtFactory(
    new GitHubJwt.FilePrivateKeySource("/path/to/pem_file"),
    new GitHubJwt.GitHubJwtFactoryOptions
    {
        AppIntegrationId = 1, // The GitHub App Id
        ExpirationSeconds = 600 // 10 minutes is the maximum time allowed
    }
);

var jwtToken = generator.CreateEncodedJwtToken();

// Pass the JWT as a Bearer token to Octokit.net
var appClient = new GitHubClient(new ProductHeaderValue("MyApp"))
{
    Credentials = new Credentials(jwtToken, AuthenticationType.Bearer)
};
```

### A Note on identifying Installation Id's
GitHub Apps specify which webhook events they are interested in, and when installed in a User/Organization account and restricted to some/all repositories, these webhooks will then be sent to the GitHub App's URL.

WebHook payloads now include an extra field to indicate the Id of the GitHub App Installation that is associated with the received webhook.

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
