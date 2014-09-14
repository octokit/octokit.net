param (
    $InstallPath,
    $ToolsPath,
    $Package,
    $Project
)

$TaskFile = "DocPlagiarizer.dll"

Add-Type -AssemblyName "Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

$buildProject = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($Project.FullName) |
    Select-Object -First 1

$existingTasks = $buildProject.Xml.UsingTasks |
    Where-Object { $_.AssemblyFile -like "*\$TaskFile" }
if ($existingTasks) {
    $existingTasks | 
        ForEach-Object {
            $buildProject.Xml.RemoveChild($_) | Out-Null
        }
}

$existingTagets = $buildProject.Xml.Targets |
    Where-Object { $_.Name -like "DocPlagiarizerTarget" }
if ($existingTagets) {
    $existingTagets |
        ForEach-Object {
            $buildProject.Xml.RemoveChild($_) | Out-Null
        }
}

$Project.Save()