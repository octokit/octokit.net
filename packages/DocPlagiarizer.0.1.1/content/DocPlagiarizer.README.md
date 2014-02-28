#DocPlagiarizer

##What is it?

A build task for MSBuild that will copy xml documentation comments from interfaces to implementations.

##How do I install it?

Install using NuGet

    Install-Package DocPlagiarizer

This will add the build task to your project, create a build target that calls the task and set that target to run before `Build`.

##How do I use it?

Build your project and let your editor reload any changed files.