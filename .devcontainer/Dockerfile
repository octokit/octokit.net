# See here for image contents: https://github.com/microsoft/vscode-dev-containers/tree/v0.234.0/containers/dotnet/.devcontainer/base.Dockerfile

# [Choice] .NET version: 6.0, 6.0-bullseye, 6.0-focal
ARG VARIANT="6.0-bullseye"
FROM mcr.microsoft.com/vscode/devcontainers/dotnet

# "install" the dotnet 6.0 runtime for tests
COPY --from=mcr.microsoft.com/dotnet/sdk:6.0 /usr/share/dotnet/shared /usr/share/dotnet/shared

# # Add mkdocs for doc generation
# RUN apt-get update && export DEBIAN_FRONTEND=noninteractive && apt-get -y install --no-install-recommends python3-pip
# RUN pip3 install mkdocs

RUN apt-get install -y mono-complete
