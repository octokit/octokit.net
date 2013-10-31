#r @"tools\FAKE.Core\tools\FakeLib.dll"
open Fake 


Target "Test" (fun _ ->
    trace "Testing stuff..."
)

Target "Deploy" (fun _ ->
    trace "Heavy deploy action"
)

"Test"
   ==> "Deploy"

Run "Deploy"