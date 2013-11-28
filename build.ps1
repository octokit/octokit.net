 param (
    [string]$Target = "Default",
    [string]$BuildMode = "Release"
 )

& "tools\nuget\nuget.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "2.2.0"

# because we want to run specific steps inline on qed
# we need to break the dependency chain
# this ensures we do a build before running any tests

$postBuildTasks = "Default", "UnitTests", "IntegrationTests", "CreatePackages"

if ($postBuildTasks -contains $Target) {
& "tools\FAKE.Core\tools\Fake.exe" "build.fsx" "target=BuildApp" "buildMode=$BuildMode"
}

& "tools\FAKE.Core\tools\Fake.exe" "build.fsx" "target=$Target" "buildMode=$BuildMode"