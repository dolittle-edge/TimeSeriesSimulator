# Build the .NET part
FROM microsoft/dotnet:2.1-sdk AS dotnet-build
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
FROM microsoft/dotnet:2.1-runtime-stretch-slim
WORKDIR /app
COPY --from=dotnet-build /app/Source/Core/out ./
COPY --from=dotnet-build /app/Source/Core/data ./data
COPY --from=node-build /src/Core/wwwroot ./wwwroot
COPY Source/application.json Source/bounded-context.json ./

EXPOSE 80

#RUN useradd -r -ms /bin/bash moduleuser
#USER moduleuser

ENTRYPOINT ["dotnet", "Core.dll"]