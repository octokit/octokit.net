#r @"tools\FAKE.Core\tools\FakeLib.dll"
open Fake 

let packageDir = "./packaging/octokit/"

Target "Clean" (fun _ ->
    CleanDir packageDir
)

Target "CopyAdditionalFiles" (fun _ ->
    CopyFiles packageDir ["LICENSE.txt"; "README.md"]
)

Target "Default" DoNothing

"Clean"
   ==> "CopyAdditionalFiles"
   ==> "Default"

RunTargetOrDefault "Default"