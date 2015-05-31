## `HttpClient` and Octokit

If you want to understand more of the internals of how Octokit is using
`HttpClient`, then read on. Otherwise, you should be fine with using the
defaults.

### Redirects

A recent feature of the GitHub API is to handle redirects automatically.
This means the client needs to handle 30* responses and redirect correctly.

Due to the default behaviour of `HttpWebRequest`, we need to implement this
as a custom handler in `HttpClient`.

The rules we currently follow:

```
 - MUST follow redirect requests if HTTP status code is 301, 302, or 307.
 - MUST redirect a 30x status code if the HTTP method is HEAD, OPTIONS, GET, POST, PUT, PATCH, or DELETE.
 - MUST use the original request's HTTP method when following a redirect where the HTTP status code is 307.
 - SHOULD use the original request's HTTP method when following a redirect where the HTTP status code is 301 or 302.
 - MUST use the original request's authentication credentials when following a redirect where the original host matches the redirect host.
 - MUST NOT use the original request's authentication credentials when following a redirect where the original host does not match the redirect host.
 - SHOULD only follow 3 redirects.
 ```

### Proxy Support

There's some basic support for specify proxy credentials, but this is a work
in progress as there's a bunch of internals that need to be refactored
thoughtfully.

Here's how you can do it today:

```csharp
// set your proxy details here
var proxy = new WebProxy(); 

// this is the core connection
var connection = new Connection(new ProductHeaderValue("my-cool-app"),
    new HttpClientAdapter(() => HttpMessageHandlerFactory.CreateDefault(proxy)));

// and pass this connection to your client
var client = new GitHubClient(connection);
```

And if you're looking for GitHub Enterprise support with a custom proxy, it's
currently broken. I've opened an issue to track this, so please chime in if it's
something you need to support: https://github.com/octokit/octokit.net/issues/809

Another request has been for auto-wiring the proxy details at runtime. Please
leave a comment in this thread if you're interested in this:
https://github.com/octokit/octokit.net/issues/594