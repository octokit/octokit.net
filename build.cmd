@echo off

"tools\nuget\nuget.exe" "install" "xunit.runner.console" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "2.1.0"
"tools\nuget\nuget.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.4.2"
"tools\nuget\nuget.exe" "install" "SourceLink.Fake" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "1.1.0"
"tools\nuget\nuget.exe" "install" "Octokit.CodeFormatter" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "1.0.0-preview" -Pre

:Build
cls

SET TARGET="Default"

IF NOT [%1]==[] (set TARGET="%1")

SET BUILDMODE="Release"
IF NOT [%2]==[] (set BUILDMODE="%2")

:: because we want to run specific steps inline on qed
:: we need to break the dependency chain
:: this ensures we do a build before running any tests

if %TARGET%=="Default" (SET RunBuild=1)
if %TARGET%=="RunUnitTests" (SET RunBuild=1)
if %TARGET%=="RunIntegrationTests" (SET RunBuild=1)
if %TARGET%=="CreatePackages" (SET RunBuild=1)

if NOT "%RunBuild%"=="" (
"tools\FAKE.Core\tools\Fake.exe" "build.fsx" "target=BuildApp" "buildMode=%BUILDMODE%"
)

"tools\FAKE.Core\tools\Fake.exe" "build.fsx" "target=%TARGET%" "buildMode=%BUILDMODE%"

rem Bail if we're running a TeamCity build.
if defined TEAMCITY_PROJECT_NAME goto Quit

rem Bail if we're running a MyGet build.
if /i "%BuildRunner%"=="MyGet" goto Quit

:Quit
exit /b %errorlevel%
