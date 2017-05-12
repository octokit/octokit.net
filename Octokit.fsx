#I __SOURCE_DIRECTORY__
#r "./Octokit/bin/Release/Net45/Octokit.dll"

open Octokit
open System
open System.IO

type Draft =
    { Client : GitHubClient
      Owner : string
      Project : string
      DraftRelease : Release }

let private isRunningOnMono = System.Type.GetType ("Mono.Runtime") <> null

let rec private retry count asyncF =
    // This retry logic causes an exception on Mono:
    // https://github.com/fsharp/fsharp/issues/440
    if isRunningOnMono then
        asyncF
    else
        async {
            try
                return! asyncF
            with _ when count > 0 -> return! retry (count - 1) asyncF
        }

let createClient user password =
    async {
        let github = new GitHubClient(new ProductHeaderValue("Octokit"))
        github.Credentials <- Credentials(user, password)
        return github
    }

let createClientWithToken token =
    async {
        let github = new GitHubClient(new ProductHeaderValue("Octokit"))
        github.Credentials <- Credentials(token)
        return github
    }

let private makeRelease draft owner project version prerelease (notes:seq<string>) (client : Async<GitHubClient>) =
    async {
        let data = new NewRelease(version)
        data.Name <- version
        data.Body <- String.Join(Environment.NewLine, notes)
        data.Draft <- draft
        data.Prerelease <- prerelease
        let! client' = client
        let! draft = Async.AwaitTask <| client'.Release.Create(owner, project, data)
        let draftWord = if data.Draft then " draft" else ""
        printfn "Created%s release id %d" draftWord draft.Id
        return { Client = client'
                 Owner = owner
                 Project = project
                 DraftRelease = draft }
    } |> retry 5

let createDraft owner project version prerelease notes client = makeRelease true owner project version prerelease notes client
let createRelease owner project version prerelease notes client = makeRelease false owner project version prerelease notes client

let uploadFile fileName (draft : Async<Draft>) =
    async {
        let fi = FileInfo(fileName)
        let archiveContents = File.OpenRead(fi.FullName)
        let assetUpload = new ReleaseAssetUpload(fi.Name,"application/octet-stream",archiveContents,Nullable<TimeSpan>())
        let! draft' = draft
        let! asset = Async.AwaitTask <| draft'.Client.Release.UploadAsset(draft'.DraftRelease, assetUpload)
        printfn "Uploaded %s" asset.Name
        return draft'
    } |> retry 5

let releaseDraft (draft : Async<Draft>) =
    async {
        let! draft' = draft
        let update = draft'.DraftRelease.ToUpdate()
        update.Draft <- Nullable<bool>(false)
        let! released = Async.AwaitTask <| draft'.Client.Release.Edit(draft'.Owner, draft'.Project, draft'.DraftRelease.Id, update)
        printfn "Released %d on github" released.Id
    } |> retry 5
