#r @"tools\FAKE.Core\tools\FakeLib.dll"
open Fake 

let projectName = "Octokit"
let authors = ["GitHub"]
let projectDescription = "An async-based GitHub API client library for .NET"
let projectSummary = projectDescription // TODO: write a summary

let reactiveProjectName = "Octokit.Reactive"
let reactiveProjectDescription = "An IObservable based GitHub API client library for .NET using Reactive Extensions"
let reactiveProjectSummary = reactiveProjectDescription // TODO: write a summary

let buildDir = "./Octokit/bin"
let reactiveBuildDir = "./Octokit.Reactive/bin"
let packagingRoot = "./packaging/"
let packagingDir = packagingRoot @@ "octokit"
let reactivePackagingDir = packagingRoot @@ "octokit.reactive"

let version = "0.1.1" // TODO: Retrieve this from release notes or CI

Target "Clean" (fun _ ->
    CleanDirs [buildDir; reactiveBuildDir; packagingRoot; packagingDir; reactivePackagingDir]
)

Target "BuildApp" (fun _ ->
    MSBuildWithDefaults "Build" ["./Octokit.sln"]
    |> Log "AppBuild-Output: "
)

Target "CreateOctokitPackage" (fun _ ->
    let net45Dir = packagingDir @@ "lib/net45/"
    let netcore45Dir = packagingDir @@ "lib/netcore45/"
    CleanDirs [net45Dir; netcore45Dir]

    CopyFile net45Dir (buildDir @@ "Release/Net40/Octokit.dll") // TODO: this a bug in the sln?!
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

    CopyFile net45Dir (reactiveBuildDir @@ "Release/Net40/Octokit.Reactive.dll") // TODO: this a bug in the sln?!    
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
                ["Octokit", RequireExactly (NormalizeVersion version)
                 "Rx-Main", RequireExactly "2.1.30214"] // TODO: Retrieve this from the referenced package
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "Octokit.Reactive.nuspec"
)

Target "Default" DoNothing

"Clean"
   ==> "BuildApp"
   ==> "CreateOctokitPackage"
   ==> "CreateOctokitReactivePackage"
   ==> "Default"

RunTargetOrDefault "Default"