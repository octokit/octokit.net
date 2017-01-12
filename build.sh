#!/bin/bash
dotnet restore ./build --verbosity=error
dotnet run -p ./build -- "$@"
