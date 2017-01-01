## Getting Started

The easiest way to get started with Octokit is to use a plain `GitHubClient`:

```
var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
```

This will let you access unauthenticated GitHub APIs, but you will be subject to rate limiting (you can read more about this [here](https://developer.github.com/v3/#rate-limiting)).

But why do you need this `ProductHeaderValue` value?

The API will reject you if you don't provide a `User-Agent` header (more details [here](https://developer.github.com/v3/#user-agent-required)). This is also to identify applications that are accessing the API and enable GitHub to contact the application author if there are problems. So pick a name that stands out!

### Authenticated Access

If you want to access private repositories or perform actions on behalf of a user, you need to pass credentials to the client.

There are two options supported by the API - basic and OAuth authentication.

```
var basicAuth = new Credentials("username", "password"); // NOTE: not real credentials
client.Credentials = basicAuth;
```

```
var tokenAuth = new Credentials("token"); // NOTE: not real token
client.Credentials = tokenAuth;
```

It is **strongly recommended** to use the [OAuth Flow](https://github.com/octokit/octokit.net/blob/master/docs/oauth-flow.md) for interactions on behalf of a user, as this gives two significant benefits:

 - the application owner never needs to store a user's password
 - the token can be revoked by the user at a later date

When authenticated, you have 5000 requests per hour available. So this is the recommended approach for interacting with the API.

### Connect to GitHub Enterprise

Octokit also supports connecting to GitHub Enterprise environments - just provide the URL to your GitHub Enterprise server when creating the client.

```csharp
var ghe = new Uri("https://github.myenterprise.com/");
var client = new GitHubClient(new ProductHeaderValue("my-cool-app"), ghe);
```

You can use the `EnterpriseProbe` class to test whether a URL points to a Github Enterprise instance.

```csharp
var probe = new EnterpriseProbe(new ProductHeaderValue("my-cool-app"));
var result = await probe.Probe(new Uri("http://ghe.example.com/"));
Assert.Equal(EnterpriseProbeResult.Ok, result); 
```

### Get some data

Once you have that setup, the simplest thing to experiment with is fetching details about a specific user:

```csharp
var user = await client.User.Get("shiftkey");
Console.WriteLine("{0} has {1} public repositories - go check out their profile at {2}",
	user.Name,
	user.PublicRepos,
	user.Url);
```

If you've authenticated as a given user, you can query their details directly:

```
var user = await client.User.Current();
```

### Too Much of a Good Thing: Dealing with API Rate Limits
Like any popular API, Github needs to throttle some requests. The OctoKit.NET client allows you to get some insight into how many requests you have left and when you can start making requests again. It does this via the `ApiInfo` object and the `GetLastApiInfo()` method.

Example usage:

```csharp
GithubClient client; 
//Create & initialize the client here

// Prior to first API call, this will be null, because it only deals with the last call.
var apiInfo = client.GetLastApiInfo();

// If the ApiInfo isn't null, there will be a property called RateLimit
var rateLimit = apiInfo?.RateLimit;

var howManyRequestsCanIMakePerHour = rateLimit?.Limit;
var howManyRequestsDoIHaveLeft = rateLimit?.Remaining;
var whenDoesTheLimitReset = rateLimit?.Reset;
```

An authenticated client will have a significantly higher limit than an anonymous client. 

For more information on the API and understanding rate limits, you may want to consult [the Github API docs on rate limits](https://developer.github.com/v3/#rate-limiting).