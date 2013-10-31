#r @"tools\FAKE.Core\tools\FakeLib.dll"
open Fake 

let buildDir = "./Octokit/bin"
let packageDir = "./packaging/octokit/"

Target "Clean" (fun _ ->
    CleanDirs [buildDir; packageDir]
)

Target "BuildApp" (fun _ ->
    MSBuildWithDefaults "Build" ["./Octokit.sln"]
    |> Log "AppBuild-Output: "
)

Target "CreateNuget" (fun _ ->
    let net45Dir = packageDir @@ "lib/net45/"
    let netcore45Dir = packageDir @@ "lib/netcore45/"
    CleanDirs [net45Dir; netcore45Dir]

    CopyFile net45Dir (buildDir @@ "Release/Net40/Octokit.dll") // TODO: this a bug in the sln?!
    CopyFile netcore45Dir (buildDir @@ "Release/NetCore45/Octokit.dll")
    CopyFiles packageDir ["LICENSE.txt"; "README.md"]
)

Target "Default" DoNothing

"Clean"
   ==> "BuildApp"
   ==> "CreateNuget"
   ==> "Default"

RunTargetOrDefault "Default"