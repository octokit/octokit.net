## Octokit Models

These either represent the body of a GitHub API request or response.

Request objects should be placed in the, you guessed it, _Request_ folder. Likewise Response objects should be placed
in the _Response_ folder.

Some models can be used for both requests and responses.

### Request models

The general design principle for request models are:

1. They represent the body of a request.
2. Properties that are _required_ by the API should be required by the model and passed in via the constructor.
3. Required porperties should not have a setter since they will be set by the constructor.
4. All other properties should have both a getter and setter.

Note that Octokit.net automatically converts property name to the Ruby casing required by the GitHub API. Thus a
property named `BreakingBad` would be passed as `breaking_bad`.

### Response models

The general design principle for response models are:

1. They should be immutable. As such, properties have a `public` getter and `protected` setter.
2. We want the properties to be read only, but also make it possible to mock the response from API methods.
3. They must be easily serialized and deserialized.
4. They need a default constructor as well as one that takes in every parameter.

We're in the process of reconsidering this design as it's created a few problems for us. Namely that creating these
objects in a unit test is a royal pain.

### Notes

There's a lot of confusion caused by the fact that the GitHub API returns GitHub resources as well as Git resources.
For example, you can use the [Git Data API](https://developer.github.com/v3/git/) to directly manipulate Git objects
such as a `commit`. At the same time, GitHub also has its own `commit` (represented by `GitHubCommit` in Octokit.net)
that contains the GitHub information around the commit such as comments.
