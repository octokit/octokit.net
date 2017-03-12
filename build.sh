#!/bin/bash
cd build
dotnet restore --verbosity=error
dotnet run -- "$@"
