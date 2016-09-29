# Start Cake
Push-Location
cd build
Write-Host "Cake.Frosting: Restoring packages..."
Invoke-Expression "dotnet restore ./project.json --verbosity error"
if($LASTEXITCODE -ne 0) {
    Pop-Location;
    exit $LASTEXITCODE;
}
Write-Host "Cake.Frosting: Running build..."
Invoke-Expression "dotnet run -- $Arguments"
if($LASTEXITCODE -ne 0) {
    Pop-Location;
    exit $LASTEXITCODE;
}
Pop-Location