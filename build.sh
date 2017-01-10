#!/bin/bash
dotnet restore .\build --verbodity=error
dotnet run -p .\build -- $@