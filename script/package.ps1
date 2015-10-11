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

$fake = Join-Path $rootDirectory ".\tools\FAKE.Core\tools\Fake.exe"
$script = Join-Path $rootDirectory "build.fsx"
$hacks = Join-Path $rootDirectory "script\hacks-patch-config.fsx"

if ((Test-Path $fake) -eq $false) {
    Write-Warning "FAKE executable not found in repository"
    Write-Output "Please run /script/bootstrap before running this again..."
    Pop-Location
    Die-WithOutput -1
}

# patching FAKE as an inline workaround for SourceLink patching issue
# see https://github.com/ctaggart/SourceLink/issues/106 for details
Write-Output ""
Write-Output "Patching FAKE app.config to workaround assembly binding issue..."
Write-Output ""
. $fake $hacks

Write-Output "Creating packages..."
Write-Output ""
. $fake $script "target=CreatePackages" "buildMode=Release"

Pop-Location