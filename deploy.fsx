#r @"tools/FAKE.Core/tools/FakeLib.dll"
#load "Octokit.fsx"
open Fake 
open Fake.Git
open System
open Octokit

let authors = ["GitHub"]

// project name 
let gitOwner = getBuildParam "gitOwner"
let gitHome = "https://github.com/" + gitOwner
let projectName = "Octokit"

// The name of the project on GitHub
let gitName = "octokit.net"

// directories
let packagingRoot = "./packaging/"

let releaseNotes = 
    ReadFile "ReleaseNotes.md"
    |> ReleaseNotesHelper.parseReleaseNotes


Target "ReleaseGitHub" (fun _ ->
    let user = getBuildParam "gitOwner" 
    traceFAKE  "The git owner is  %s" user
    let pw =
        match getBuildParam "github-pw" with
        | s when not (String.IsNullOrWhiteSpace s) -> s
        | _ -> getUserPassword "Password: "
    let remote =
        Git.CommandHelper.getGitResult "" "remote -v"
        |> Seq.filter (fun (s: string) -> s.EndsWith("(push)"))
        |> Seq.tryFind (fun (s: string) -> s.Contains(gitOwner + "/" + gitName))
        |> function None -> gitHome + "/" + gitName | Some (s: string) -> s.Split().[0]

    StageAll ""
    Git.Commit.Commit "" (sprintf "Bump version to %s" releaseNotes.NugetVersion)
    Branches.pushBranch "" remote (Information.getBranchName "")

    Branches.tag "" releaseNotes.NugetVersion
    Branches.pushTag "" remote releaseNotes.NugetVersion
    
    // release on github
    createClient user pw
    |> createDraft gitOwner gitName releaseNotes.NugetVersion (releaseNotes.SemVer.PreRelease <> None) releaseNotes.Notes 
    |> uploadFile (sprintf "./packaging/Octokit.%s.nupkg" releaseNotes.AssemblyVersion)
    |> uploadFile (sprintf "./packaging/Octokit.Reactive.%s.nupkg" releaseNotes.AssemblyVersion)
    |> releaseDraft
    |> Async.RunSynchronously
)

RunTargetOrDefault "ReleaseGitHub"
