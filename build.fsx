#r @"tools\FAKE.Core\tools\FakeLib.dll"
open Fake 
open System

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

let releaseNotes = 
    ReadFile "ReleaseNotes.md"
    |> ReleaseNotesHelper.parseReleaseNotes

let buildMode = getBuildParamOrDefault "buildMode" "Release"

Target "Clean" (fun _ ->
    CleanDirs [buildDir; reactiveBuildDir; testResultsDir; packagingRoot; packagingDir; reactivePackagingDir]
)

open Fake.AssemblyInfoFile

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "SolutionInfo.cs"
      [ Attribute.Product projectName
        Attribute.Version releaseNotes.AssemblyVersion
        Attribute.FileVersion releaseNotes.AssemblyVersion
        Attribute.ComVisible false ]
)

Target "CheckProjects" (fun _ ->
    !! "./Octokit/Octokit*.csproj"
    |> Fake.MSBuild.ProjectSystem.CompareProjectsTo "./Octokit/Octokit.csproj"

    !! "./Octokit.Reactive/Octokit.Reactive*.csproj"
    |> Fake.MSBuild.ProjectSystem.CompareProjectsTo "./Octokit.Reactive/Octokit.Reactive.csproj"
)

Target "FixProjects" (fun _ ->
    !! "./Octokit/Octokit*.csproj"
    |> Fake.MSBuild.ProjectSystem.FixProjectFiles "./Octokit/Octokit.csproj"

    !! "./Octokit.Reactive/Octokit.Reactive*.csproj"
    |> Fake.MSBuild.ProjectSystem.FixProjectFiles "./Octokit.Reactive/Octokit.Reactive.csproj"
)

Target "BuildApp" (fun _ ->
    MSBuild null "Build" ["Configuration", buildMode] ["./Octokit.sln"]
    |> Log "AppBuild-Output: "
)

Target "ConventionTests" (fun _ ->
    !! (sprintf "./Octokit.Tests.Conventions/bin/%s/**/Octokit.Tests.Conventions.dll" buildMode)
    |> xUnit (fun p -> 
            {p with 
                XmlOutput = true
                OutputDir = testResultsDir })
)

Target "UnitTests" (fun _ ->
    !! (sprintf "./Octokit.Tests/bin/%s/**/Octokit.Tests*.dll" buildMode)
    |> xUnit (fun p -> 
            {p with 
                XmlOutput = true
                Verbose = false
                OutputDir = testResultsDir })
)

Target "IntegrationTests" (fun _ ->
    if hasBuildParam "OCTOKIT_GITHUBUSERNAME" && hasBuildParam "OCTOKIT_GITHUBPASSWORD" then
        !! (sprintf "./Octokit.Tests.Integration/bin/%s/**/Octokit.Tests.Integration.dll" buildMode)
        |> xUnit (fun p -> 
                {p with 
                    XmlOutput = true
                    Verbose = false
                    OutputDir = testResultsDir
                    TimeOut = TimeSpan.FromMinutes 10.0  })
    else
        "The integration tests were skipped because the OCTOKIT_GITHUBUSERNAME and OCTOKIT_GITHUBPASSWORD environment variables are not set. " +
        "Please configure these environment variables for a GitHub test account (DO NOT USE A \"REAL\" ACCOUNT)."
        |> traceImportant 
)

Target "CreateOctokitPackage" (fun _ ->
    let net45Dir = packagingDir @@ "lib/net45/"
    let netcore45Dir = packagingDir @@ "lib/netcore45/"
    let portableDir = packagingDir @@ "lib/portable-net45+wp80+win/"
    CleanDirs [net45Dir; netcore45Dir; portableDir]

    CopyFile net45Dir (buildDir @@ "Release/Net45/Octokit.dll")
    CopyFile netcore45Dir (buildDir @@ "Release/NetCore45/Octokit.dll")
    CopyFile portableDir (buildDir @@ "Release/Portable/Octokit.dll")
    CopyFiles packagingDir ["LICENSE.txt"; "README.md"; "ReleaseNotes.md"]

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = projectName
            Description = projectDescription                               
            OutputPath = packagingRoot
            Summary = projectSummary
            WorkingDir = packagingDir
            Version = releaseNotes.AssemblyVersion
            ReleaseNotes = toLines releaseNotes.Notes
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "octokit.nuspec"
)

Target "CreateOctokitReactivePackage" (fun _ ->
    let net45Dir = reactivePackagingDir @@ "lib/net45/"
    CleanDirs [net45Dir]

    CopyFile net45Dir (reactiveBuildDir @@ "Release/Net45/Octokit.Reactive.dll")
    CopyFiles reactivePackagingDir ["LICENSE.txt"; "README.md"; "ReleaseNotes.md"]

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = reactiveProjectName
            Description = reactiveProjectDescription                               
            OutputPath = packagingRoot
            Summary = reactiveProjectSummary
            WorkingDir = reactivePackagingDir
            Version = releaseNotes.AssemblyVersion
            ReleaseNotes = toLines releaseNotes.Notes
            Dependencies =
                ["Octokit", NormalizeVersion releaseNotes.AssemblyVersion
                 "Rx-Main", GetPackageVersion "./packages/" "Rx-Main"]
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "Octokit.Reactive.nuspec"
)

Target "Default" DoNothing

Target "CreatePackages" DoNothing

"Clean"
   ==> "AssemblyInfo"
   ==> "CheckProjects"
       ==> "BuildApp"

"UnitTests"
   ==> "Default"

"ConventionTests"
   ==> "Default"

"IntegrationTests"
   ==> "Default"

"CreateOctokitPackage"
   ==> "CreatePackages"
"CreateOctokitReactivePackage"
   ==> "CreatePackages"

RunTargetOrDefault "Default"