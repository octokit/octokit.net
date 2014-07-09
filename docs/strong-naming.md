# Why We're Strong Naming Octokit.net

A while ago we had an [epic discussion](https://github.com/octokit/octokit.net/issues/405) about whether or not to strong name Octokit.net - thanks for all the feedback, by the way - and we've finally settled on what we're going to do with Octokit.net.

At some point between now and the 1.0 release, we will organize strong naming to be added to the build process. Lots of people read this discussion as the team being opposed to strong naming - but that's not the case.

So why are we doing this?

## Visual Studio Extensibility

The initial discussions around strong naming indicated that many Visual Studio extensions require it, and Bad Things Happen when you don't have it. GAC or otherwise, you can easily hit upon situations where collisions between assemblies occurs - I've suffered this, and if we can prevent our users from suffering this fate, we should do it.

## What We're Stuck With

It's been excellent to talk with various people inside Microsoft about the current state of affairs with strong naming - the tradeoffs involved, possible future changes to the runtime and the platform, and to just kick around ideas to make things better. But this is not something that is going to change overnight, so we need to temper these grand plans with our current situation.

## Official Support

Octokit.net is a project which is maintained and supported by GitHub employees. While we love and encourage contributions to this project, the buck stops with us in terms of support. Our personal projects will probably have different approaches to strong naming, but we want to ensure everyone can use Octokit.net and build cool things with it.
