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
let packageDir = "./packaging/octokit/"
let reactivePackageDir = "./packaging/octokit/reactive"

let version = "0.1.1" // TODO: Retrieve this from release notes or CI

Target "Clean" (fun _ ->
    CleanDirs [buildDir; reactiveBuildDir; packageDir; reactivePackageDir]
)

Target "BuildApp" (fun _ ->
    MSBuildWithDefaults "Build" ["./Octokit.sln"]
    |> Log "AppBuild-Output: "
)

Target "CreateOctokitPackage" (fun _ ->
    let net45Dir = packageDir @@ "lib/net45/"
    let netcore45Dir = packageDir @@ "lib/netcore45/"
    CleanDirs [net45Dir; netcore45Dir]

    CopyFile net45Dir (buildDir @@ "Release/Net40/Octokit.dll") // TODO: this a bug in the sln?!
    CopyFile netcore45Dir (buildDir @@ "Release/NetCore45/Octokit.dll")
    CopyFiles packageDir ["LICENSE.txt"; "README.md"]

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = projectName
            Description = projectDescription                               
            OutputPath = "./packaging/"
            Summary = projectSummary
            WorkingDir = packageDir
            Version = version
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "octokit.nuspec"
)

Target "CreateOctokitReactivePackage" (fun _ ->
    let net45Dir = reactivePackageDir @@ "lib/net45/"
    CleanDirs [net45Dir]

    CopyFile net45Dir (reactiveBuildDir @@ "Release/Net40/Octokit.Reactive.dll") // TODO: this a bug in the sln?!    
    CopyFiles packageDir ["LICENSE.txt"; "README.md"]

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = reactiveProjectName
            Description = reactiveProjectDescription                               
            OutputPath = "./packaging/"
            Summary = reactiveProjectSummary
            WorkingDir = reactivePackageDir
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