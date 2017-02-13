#r @"tools/FAKE.Core/tools/FakeLib.dll"
#r @"tools/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "System.Xml.Linq"
#load "tools/SourceLink.Fake/tools/SourceLink.fsx"
open Fake
open System
open System.IO
open SourceLink
open FSharp.Data

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
let samplesDir = "./samples"
let packagingDir = packagingRoot @@ "octokit"
let reactivePackagingDir = packagingRoot @@ "octokit.reactive"
let linqPadDir = "./tools/LINQPad"

let releaseNotes =
    ReadFile "ReleaseNotes.md"
    |> ReleaseNotesHelper.parseReleaseNotes

let buildMode = getBuildParamOrDefault "buildMode" "Release"

MSBuildDefaults <- {
    MSBuildDefaults with
        ToolsVersion = Some "14.0"
        Verbosity = Some MSBuildVerbosity.Minimal }

Target "Clean" (fun _ ->
    CleanDirs [buildDir; reactiveBuildDir; testResultsDir; packagingRoot; packagingDir; reactivePackagingDir]
)

open Fake.AssemblyInfoFile
open Fake.Testing

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./SolutionInfo.cs"
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

let codeFormatter = @"tools\Octokit.CodeFormatter\tools\CodeFormatter.exe"

Target "FormatCode" (fun _ ->
    [   "Octokit"
        "Octokit.Reactive"
        "Octokit.Tests"
        "Octokit.Tests.Conventions"
        "Octokit.Tests.Integration"]
    |> Seq.iter (fun pf ->
        let project = pf + "/" + pf + ".csproj"
        ExecProcess (fun info ->
        info.FileName <- codeFormatter
        info.Arguments <- project + " /nocopyright /nounicode") (TimeSpan.FromMinutes 1.0)
            |> ignore
    )
)

Target "FixProjects" (fun _ ->
    !! "./Octokit/Octokit*.csproj"
    |> Fake.MSBuild.ProjectSystem.FixProjectFiles "./Octokit/Octokit.csproj"

    !! "./Octokit.Reactive/Octokit.Reactive*.csproj"
    |> Fake.MSBuild.ProjectSystem.FixProjectFiles "./Octokit.Reactive/Octokit.Reactive.csproj"
)

let setParams defaults = {
    defaults with
        ToolsVersion = Some("14.0")
        Targets = ["Build"]
        Properties =
            [
                "Configuration", buildMode
            ]
    }

let Exec command args =
    let result = Shell.Exec(command, args)
    if result <> 0 then failwithf "%s exited with error %d" command result

Target "BuildApp" (fun _ ->
    build setParams "./Octokit.sln"
        |> DoNothing
)

Target "BuildMono" (fun _ ->
    // xbuild does not support msbuild  tools version 14.0 and that is the reason
    // for using the xbuild command directly instead of using msbuild
    Exec "xbuild" "./Octokit-Mono.sln /t:Build /tv:12.0 /v:m  /p:RestorePackages='False' /p:Configuration='Release' /logger:Fake.MsBuildLogger+ErrorLogger,'../octokit.net/tools/FAKE.Core/tools/FakeLib.dll'"
)

Target "RestoreDotNetCore" (fun _ ->
    [ "./Octokit.Next"
      "./Octokit.Next.Tests" ]
    |> Seq.iter (fun d ->
        Fake.DotNetCli.Restore (fun p ->
            { p with
                WorkingDir = d })
    )
)

Target "BuildDotNetCore" (fun _ ->
    !! "./**/project.json"
    |> Fake.DotNetCli.Build id
)

Target "UnitTestsDotNetCore" (fun _ ->
    !! "./Octokit.Next.Tests"
    |> Fake.DotNetCli.Test id
)

Target "ConventionTests" (fun _ ->
    !! (sprintf "./Octokit.Tests.Conventions/bin/%s/**/Octokit.Tests.Conventions.dll" buildMode)
    |> xUnit2 (fun p ->
            {p with
                HtmlOutputPath = Some (testResultsDir @@ "xunit.html") })
)

Target "UnitTests" (fun _ ->
    !! (sprintf "./Octokit.Tests/bin/%s/**/Octokit.Tests*.dll" buildMode)
    |> xUnit2 (fun p ->
            {p with
                HtmlOutputPath = Some (testResultsDir @@ "xunit.html") })
)

Target "IntegrationTests" (fun _ ->
    if hasBuildParam "OCTOKIT_GITHUBUSERNAME" && hasBuildParam "OCTOKIT_GITHUBPASSWORD" then
        !! (sprintf "./Octokit.Tests.Integration/bin/%s/**/Octokit.Tests.Integration.dll" buildMode)
        |> xUnit2 (fun p ->
                {p with
                    HtmlOutputPath = Some (testResultsDir @@ "xunit.html")
                    TimeOut = TimeSpan.FromMinutes 10.0  })
    else
        "The integration tests were skipped because the OCTOKIT_GITHUBUSERNAME and OCTOKIT_GITHUBPASSWORD environment variables are not set. " +
        "Please configure these environment variables for a GitHub test account (DO NOT USE A \"REAL\" ACCOUNT)."
        |> traceImportant
)

Target "SourceLink" (fun _ ->
    [   "Octokit/Octokit.csproj"
        "Octokit/Octokit-netcore45.csproj"
        "Octokit/Octokit-Portable.csproj"
        "Octokit.Reactive/Octokit.Reactive.csproj" ]
    |> Seq.iter (fun pf ->
        let proj = VsProj.LoadRelease pf
        let url = "https://raw.githubusercontent.com/octokit/octokit.net/{0}/%var2%"
        SourceLink.Index proj.Compiles proj.OutputFilePdb __SOURCE_DIRECTORY__ url
    )
)

type LinqPadSampleMetadata = XmlProvider<"""
    <Query Kind="Program">
        <NuGetReference>Octokit</NuGetReference>
        <NuGetReference>Octokit.Reactive</NuGetReference>
        <NuGetReference>Rx-Main</NuGetReference>
        <Namespace>Octokit</Namespace>
        <Namespace>System.Reactive.Linq</Namespace>
        <Namespace>System.Threading.Tasks</Namespace>
    </Query>
""">

Target "ValidateLINQPadSamples" (fun _ ->

    let splitFileContents = fun (file: FileInfo) ->
        let content = File.ReadAllText(file.FullName)
        let closeTag = "</Query>"
        let openTagIndex = content.IndexOf("<Query Kind=\"Program\">")
        let closeTagIndex = content.IndexOf(closeTag)
        let endOfXml = closeTagIndex + closeTag.Length
        let xmlPart = content.Substring(openTagIndex, endOfXml - openTagIndex)
        let rest = content.Substring(endOfXml)

        file.Name, xmlPart, rest

    let createTempFile = fun (fileName: string, metadataString: string, rest: string) ->
        let metadata = LinqPadSampleMetadata.Parse(metadataString)
        let assembliesDir = buildDir @@ "Release/Net45"
        let reactiveAssembliesDir = reactiveBuildDir @@ "Release/Net45"
        let tempFileName = Path.GetTempFileName()
        use stream = File.OpenWrite(tempFileName)
        use writer = new StreamWriter(stream)

        writer.WriteLine("ref {0}\\System.Reactive.Core.dll;", reactiveAssembliesDir);
        writer.WriteLine("ref {0}\\System.Reactive.Interfaces.dll;", reactiveAssembliesDir);
        writer.WriteLine("ref {0}\\System.Reactive.Linq.dll;", reactiveAssembliesDir);
        writer.WriteLine("ref {0}\\Octokit.dll;", assembliesDir);
        writer.WriteLine("ref {0}\\Octokit.Reactive.dll;", reactiveAssembliesDir);
        writer.WriteLine("ref C:\\Program Files (x86)\\Reference Assemblies\\Microsoft\\Framework\\.NETFramework\\v4.5\\System.Net.Http.dll;");

        for metadataNamespace in metadata.Namespaces do
            writer.WriteLine("using {0};", metadataNamespace)

        writer.WriteLine()
        writer.WriteLine(rest)

        writer.Flush()

        fileName, tempFileName

    directoryInfo(samplesDir @@ "linqpad-samples")
    |> filesInDir
    |> Array.map (splitFileContents >> createTempFile)
    |> Seq.iter (fun (fileName, sample) ->
        printfn "Executing sample %s" fileName
        let result = ExecProcess (fun info ->
            info.FileName <- linqPadDir @@ "lprun.exe"
            info.Arguments <- "-compileonly -lang=Program " + sample) (TimeSpan.FromMinutes 5.0)

        if result <> 0 then failwithf "lprun.exe returned with a non-zero exit code for %s" sample
    )
)

Target "CreateOctokitPackage" (fun _ ->
    let net45Dir = packagingDir @@ "lib/net45/"
    let netcore45Dir = packagingDir @@ "lib/netcore451/"
    let portableDir = packagingDir @@ "lib/portable-net45+wp80+win+wpa81/"
    let linqpadSamplesDir = packagingDir @@ "linqpad-samples"
    CleanDirs [net45Dir; netcore45Dir; portableDir;linqpadSamplesDir]

    CopyFile net45Dir (buildDir @@ "Release/Net45/Octokit.dll")
    CopyFile net45Dir (buildDir @@ "Release/Net45/Octokit.XML")
    CopyFile net45Dir (buildDir @@ "Release/Net45/Octokit.pdb")
    CopyFile netcore45Dir (buildDir @@ "Release/NetCore45/Octokit.dll")
    CopyFile netcore45Dir (buildDir @@ "Release/NetCore45/Octokit.XML")
    CopyFile netcore45Dir (buildDir @@ "Release/NetCore45/Octokit.pdb")
    CopyFile portableDir (buildDir @@ "Release/Portable/Octokit.dll")
    CopyFile portableDir (buildDir @@ "Release/Portable/Octokit.XML")
    CopyFile portableDir (buildDir @@ "Release/Portable/Octokit.pdb")
    CopyDir packagingDir "./samples" allFiles
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
    let linqpadSamplesDir = reactivePackagingDir @@ "linqpad-samples"
    CleanDirs [net45Dir;linqpadSamplesDir]

    CopyFile net45Dir (reactiveBuildDir @@ "Release/Net45/Octokit.Reactive.dll")
    CopyFile net45Dir (reactiveBuildDir @@ "Release/Net45/Octokit.Reactive.XML")
    CopyFile net45Dir (reactiveBuildDir @@ "Release/Net45/Octokit.Reactive.pdb")
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

"Clean"
   ==> "AssemblyInfo"
   ==> "CheckProjects"
   ==> "BuildMono"

"RestoreDotNetCore"
   ==> "BuildDotNetCore"
   ==> "UnitTestsDotNetCore"

"UnitTests"
   ==> "Default"

"ConventionTests"
   ==> "Default"

"IntegrationTests"
   ==> "Default"

"SourceLink"
   ==> "CreatePackages"

"CreateOctokitPackage"
   ==> "CreatePackages"

"CreateOctokitReactivePackage"
   ==> "CreatePackages"

"ValidateLINQPadSamples"
   ==> "CreatePackages"

RunTargetOrDefault "Default"
