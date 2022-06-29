# See here for image contents: https://github.com/microsoft/vscode-dev-containers/tree/v0.234.0/containers/dotnet/.devcontainer/base.Dockerfile

# [Choice] .NET version: 6.0, 5.0, 3.1, 6.0-bullseye, 5.0-bullseye, 3.1-bullseye, 6.0-focal, 5.0-focal, 3.1-focal
ARG VARIANT="6.0-bullseye"
FROM mcr.microsoft.com/vscode/devcontainers/dotnet

# "install" the dotnet 3.1 & 5.0 runtime for tests
COPY --from=mcr.microsoft.com/dotnet/sdk:3.1 /usr/share/dotnet/shared /usr/share/dotnet/shared
COPY --from=mcr.microsoft.com/dotnet/sdk:5.0 /usr/share/dotnet/shared /usr/share/dotnet/shared

# # Add mkdocs for doc generation
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive && apt-get -y install --no-install-recommends python3-pip
RUN pip3 install mkdocs

RUN apt-get install -y mono-complete
