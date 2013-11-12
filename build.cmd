@echo off

SET MinimalFAKEVersion=639
SET FAKEVersion=1
cls

if exist tools\FAKE.Core\tools\PatchVersion.txt ( 
    FOR /F "tokens=*" %%i in (tools\FAKE.Core\tools\PatchVersion.txt) DO (SET FAKEVersion=%%i)    
)

if %MinimalFAKEVersion% lss %FAKEVersion% goto Build
if %MinimalFAKEVersion%==%FAKEVersion% goto Build

"tools\nuget\nuget.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-Prerelease"

:Build
cls

SET TARGET="Default"

IF NOT [%1]==[] (set TARGET="%1")

SET BUILDMODE="Release"
IF NOT [%2]==[] (set BUILDMODE="%2")

"tools\FAKE.Core\tools\Fake.exe" "build.fsx" "target=%TARGET%" "buildMode=%BUILDMODE%"

rem Bail if we're running a TeamCity build.
if defined TEAMCITY_PROJECT_NAME goto Quit

rem Bail if we're running a MyGet build.
if /i "%BuildRunner%"=="MyGet" goto Quit

rem Loop the build script.
set CHOICE=nothing
echo (Q)uit, (Enter) runs the build again
set /P CHOICE= 
if /i "%CHOICE%"=="Q" goto :Quit

GOTO Build

:Quit
exit /b %errorlevel%
