@echo off

:Build
cls

dotnet restore .\build --verbosity error
dotnet run -p .\build -- %*

rem Bail if we're running a TeamCity build.
if defined TEAMCITY_PROJECT_NAME goto Quit

rem Bail if we're running a MyGet build.
if /i "%BuildRunner%"=="MyGet" goto Quit

:Quit
exit /b %errorlevel%
