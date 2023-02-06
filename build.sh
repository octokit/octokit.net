#!/bin/bash
if [[ "$TRAVIS_OS_NAME" == "osx" ]]; then
  # This is due to: https://github.com/NuGet/Home/issues/2163#issue-135917905
  echo "current ulimit is: `ulimit -n`..."
  ulimit -n 1024
  echo "new limit: `ulimit -n`"
fi

echo "Restoring global tools"
dotnet tool restore

if [[ ! "$OSTYPE" == "linux-gnu"* ]]; then
  dotnet dev-certs https --clear
  dotnet dev-certs https --trust
fi
cd build
echo "Preparing Cake.Frosting build runner..."
dotnet restore

echo "Executing Cake.Frosting build runner..."
echo  "dotnet run -- $@"
dotnet run -- "$@"
