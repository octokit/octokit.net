Set-StrictMode -Version Latest

try {
    # Make the output width reeeeeaaaaaly wide so our output doesn't get hard-wrapped.
    # <http://stackoverflow.com/questions/978777/powershell-output-column-width>
    $Host.UI.RawUI.BufferSize = New-Object Management.Automation.Host.Size -ArgumentList 5000, 25
} catch {
    # Most likely we were run in a cmd.exe terminal which won't allow setting
    # the BufferSize to such a strange size.
}

$rootDirectory = Split-Path (Split-Path $MyInvocation.MyCommand.Path)

Push-Location $rootDirectory

Import-Module (Join-Path $rootDirectory "script\modules\BuildUtils.psm1") 3>$null # Ignore warnings

$output = & git config --local core.autocrlf

if ($output -ne "input") {
    Write-Error "core.autocrlf not configured correctly for repository"
    Write-Error "Run ./script/bootstrap to set correct state for packaging"
    exit -1
}

Write-Output "Creating packages..."
Write-Output ""
.\tools\FAKE.Core\tools\Fake.exe "build.fsx" "target=CreatePackages" "buildMode=Release"