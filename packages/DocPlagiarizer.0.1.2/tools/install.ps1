param (
    $InstallPath,
    $ToolsPath,
    $Package,
    $Project
)

$TaskFile = "DocPlagiarizer.dll"
$TaskPath = $ToolsPath | Join-Path -ChildPath $TaskFile

Add-Type -AssemblyName "Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

$buildProject = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($Project.FullName) |
    Select-Object -First 1

$ProjectUri = New-Object -TypeName Uri -ArgumentList "file://$($Project.FullName)"
$TaskUri = New-Object -TypeName Uri -ArgumentList "file://$TaskPath"

$RelativePath = $ProjectUri.MakeRelativeUri($TaskUri) -replace '/','\'

$existingTasks = $buildProject.Xml.UsingTasks |
    Where-Object { $_.AssemblyFile -like "*\$TaskFile" }
if ($existingTasks) {
    $existingTasks | 
        ForEach-Object {
            $buildProject.Xml.RemoveChild($_) | Out-Null
        }
}
$buildProject.Xml.AddUsingTask("DocPlagiarizerTask", $RelativePath, $null) | Out-Null

$existingTagets = $buildProject.Xml.Targets |
    Where-Object { $_.Name -like "DocPlagiarizerTarget" }
if ($existingTagets) {
    $existingTagets |
        ForEach-Object {
            $buildProject.Xml.RemoveChild($_) | Out-Null
        }
}

$target = $buildProject.Xml.AddTarget("DocPlagiarizerTarget")
$target.BeforeTargets = "Build"
$target.AddTask("DocPlagiarizerTask")
$Project.Save()