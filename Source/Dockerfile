FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as base

ARG CONFIGURATION=Release

RUN echo Configuration = $CONFIGURATION

RUN if [ "$CONFIGURATION" = "Debug" ] ; then apt-get update && \
    apt-get install -y --no-install-recommends unzip procps && \
    rm -rf /var/lib/apt/lists/* \
    ; fi

RUN useradd -ms /bin/bash moduleuser
USER moduleuser

# Build the .NET part
FROM microsoft/dotnet:2.2-sdk AS dotnet-build

ARG CONFIGURATION

WORKDIR /app
VOLUME [ "/app/data" ]
COPY Build/MSBuild/* ./Build/MSBuild/
COPY Source/ ./Source/
WORKDIR /app/Source/Core
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build the static content
FROM node:11.6 AS node-build
WORKDIR /src

COPY ./Source/Web/package.json ./Web/package.json
WORKDIR /src/Web/
RUN yarn install

RUN mkdir /src/Core
RUN mkdir /src/Core/Web
COPY ./Source/Web/. ./
WORKDIR /src/Web
RUN yarn build

# Build runtime images
FROM base
WORKDIR /app
COPY --from=dotnet-build /app/Source/Core/out ./
COPY --from=dotnet-build /app/Source/Core/data ./data
COPY --from=node-build /src/Core/wwwroot ./wwwroot
COPY Source/application.json Source/bounded-context.json ./

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080


ENTRYPOINT ["dotnet", "Core.dll"]