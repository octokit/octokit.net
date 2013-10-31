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

Target "CopyAdditionalFiles" (fun _ ->
    CopyFiles packageDir ["LICENSE.txt"; "README.md"]
)

Target "Default" DoNothing

"Clean"
   ==> "BuildApp"
   ==> "CopyAdditionalFiles"
   ==> "Default"

RunTargetOrDefault "Default"