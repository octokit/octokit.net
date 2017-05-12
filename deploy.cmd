@echo off

"tools\nuget\nuget.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.4.2"

:Build
cls

SET TARGET="ReleaseGitHub"
SET GITOWNER="octokit"
IF NOT [%1]==[] (set GITOWNER="%1")

"tools\FAKE.Core\tools\Fake.exe" "deploy.fsx" "target=%TARGET%" "gitOwner=%GITOWNER%"

:Quit
exit /b %errorlevel%
