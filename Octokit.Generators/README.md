# Octokit.net Generators

## About

We've been discussing and thinking about code generators for a [while now](https://github.com/octokit/octokit.net/discussions/2527) and they are part of the [future vision](https://github.com/octokit/octokit.net/discussions/2495) for where we'd like to head with this SDK.

Consider this to be iteration 0, meaning while we want to potentially move to source generators, templates, and using the features that Rosyln offers we need to do some functional experiments and solve our current needs while iterating on the future.  Acknowledging that code generation is a solved problem (menaing there is existing work out there) we should be intentional with the direction we take.

----

## Getting started

From the Octokit .NET root run:

`dotnet run --project Octokit.Generators`

----

## Debugging

There is a launch config defined for this project named `Run Generator`

----

## CI/Actions

Currently no generation is automatically run as a build/release step.  Once the vision solidifies here a bit we'll begin introducing automation so that whatever is generated is always up to date.

----

## Notes and thoughts on code generation for Octokit.net

### Code generation, interpreters, and language interpolation

Hoisted from this [discussion](https://github.com/octokit/octokit.net/discussions/2495)

As you know there are loads of things that we can do here - especially given the power of .NET, reflection, and Rosyln. Just two thoughts on the cautious side of things here:

1. Just because we can generate things, it does not mean we should. As we roll through discovery on all of this we might find out some areas where generation would be trying to put a square peg in a round hole. We need to have the courage to stand down when needed.
2. Our long-term goal should be language and platform independence. Meaning, we might nail down incredible generative aspects for the .NET SDK but we should always be targeting things like versioned, package distributed, models that the core SDK can reference as well as a generative engine that could potentially generate the models and implementations based on language templates, RFCs, and the like for any reference language - so generating models and SDK methods should be doable for both .NET and Go (for instance) using the same "engine" if possible. It's lofty but I feel that it could be possible.

So what might the future look like for code generation given the above statements? Let's have a look a what might be a really naive/potential roadmap (again we need the community's input and involvement here - which is why I am grateful for the questions):

1. Make sure our current solution addresses the community needs: All SDKs have been synchronized with the current API surface
2. Standardize implementations - testing, models, API methods, etc... (this is a critical step to get us to generation)
3. As we go through the above we will learn more about what works and what doesn't. Armed with that knowledge, begin prototyping generative models for each SDK
4. Solve OpenAPI generation for at least 1 SDK and implement - again with our sights on other languages
5. Publish (but don't reference yet) SDK models to packaging platforms (in this case nuget)
6. Work on Generative API implementations - methods etc..
7. Model generation is unified in the SDK (i.e. we used the published / versioned models in the SDK) and published to package platforms.


After this point, things get really interesting with things like:

- Recipe and workflow generation - think of things like plugins for the SDKs to do things like instantiating project boards with teams and referenced repos all in one SDK call. SDKs shouldn't be reflective API implementations, but rather tools that simplify our approach to getting things done.
- We could start generating SDKs using language specifications - imagine pairing our OpenAPI specification with a language RFC to spit out a baseline SDK that could be extended.
- Generating language agnostic SDKs - what if we could maximize code reuse by implementing interpreted recipes or code
- Building workflow interpolation based on user patterns present in GitHub - this comes back to the community and GitHub joining up to make consistent workflows no matter where the users are - in community apps or GitHub proper.
