#r @"tools\FAKE.Core\tools\FakeLib.dll"
open Fake 

let version = "0.1.1" // TODO: Retrieve this from release notes or CI
let authors = ["GitHub"]

// project name and description
let projectName = "Octokit"
let projectDescription = "An async-based GitHub API client library for .NET"
let projectSummary = projectDescription // TODO: write a summary

let reactiveProjectName = "Octokit.Reactive"
let reactiveProjectDescription = "An IObservable based GitHub API client library for .NET using Reactive Extensions"
let reactiveProjectSummary = reactiveProjectDescription // TODO: write a summary

// directories
let buildDir = "./Octokit/bin"
let reactiveBuildDir = "./Octokit.Reactive/bin"
let testResultsDir = "./testresults"
let packagingRoot = "./packaging/"
let packagingDir = packagingRoot @@ "octokit"
let reactivePackagingDir = packagingRoot @@ "octokit.reactive"

Target "Clean" (fun _ ->
    CleanDirs [buildDir; reactiveBuildDir; testResultsDir; packagingRoot; packagingDir; reactivePackagingDir]
)

Target "BuildApp" (fun _ ->
    MSBuildWithDefaults "Build" ["./Octokit.sln"]
    |> Log "AppBuild-Output: "
)

Target "UnitTests" (fun _ ->
    !! "./Octokit.Tests/bin/**/Octokit.Tests.dll"
    |> xUnit (fun p -> 
            {p with 
                XmlOutput = true
                OutputDir = testResultsDir })
)

Target "IntegrationTests" (fun _ ->
    if hasBuildParam "OCTOKIT_GITHUBUSERNAME" && hasBuildParam "OCTOKIT_GITHUBPASSWORD" then
        !! "./Octokit.Tests.Integration/bin/**/Octokit.Tests.Integration.dll"
        |> xUnit (fun p -> 
                {p with 
                    XmlOutput = true
                    OutputDir = testResultsDir })
    else
        "The integration tests were skipped because the OCTOKIT_GITHUBUSERNAME and OCTOKIT_GITHUBUSERNAME environment variables are not set. " +
        "Please configure these environment variables for a GitHub test account (DO NOT USE A \"REAL\" ACCOUNT)."
        |> traceImportant 
)

Target "CreateOctokitPackage" (fun _ ->
    let net45Dir = packagingDir @@ "lib/net45/"
    let netcore45Dir = packagingDir @@ "lib/netcore45/"
    CleanDirs [net45Dir; netcore45Dir]

    CopyFile net45Dir (buildDir @@ "Release/Net45/Octokit.dll")
    CopyFile netcore45Dir (buildDir @@ "Release/NetCore45/Octokit.dll")
    CopyFiles packagingDir ["LICENSE.txt"; "README.md"]

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = projectName
            Description = projectDescription                               
            OutputPath = packagingRoot
            Summary = projectSummary
            WorkingDir = packagingDir
            Version = version
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "octokit.nuspec"
)

Target "CreateOctokitReactivePackage" (fun _ ->
    let net45Dir = reactivePackagingDir @@ "lib/net45/"
    CleanDirs [net45Dir]

    CopyFile net45Dir (reactiveBuildDir @@ "Release/Net45/Octokit.Reactive.dll")
    CopyFiles reactivePackagingDir ["LICENSE.txt"; "README.md"]

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = reactiveProjectName
            Description = reactiveProjectDescription                               
            OutputPath = packagingRoot
            Summary = reactiveProjectSummary
            WorkingDir = reactivePackagingDir
            Version = version
            Dependencies =
                ["Octokit", NormalizeVersion version
                 "Rx-Main", GetPackageVersion "./packages/" "Rx-Main"]
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "Octokit.Reactive.nuspec"
)

Target "Default" DoNothing

"Clean"
   ==> "BuildApp"
   ==> "UnitTests"
   ==> "IntegrationTests"
   ==> "CreateOctokitPackage"
   ==> "CreateOctokitReactivePackage"
   ==> "Default"

RunTargetOrDefault "Default"