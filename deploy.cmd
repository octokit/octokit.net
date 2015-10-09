@echo off

"tools\nuget\nuget.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.4.2"

:Build
cls

SET TARGET="ReleaseGitHub"

"tools\FAKE.Core\tools\Fake.exe" "deploy.fsx" "target=%TARGET%" "buildMode=%BUILDMODE%"

:Quit
exit /b %errorlevel%
