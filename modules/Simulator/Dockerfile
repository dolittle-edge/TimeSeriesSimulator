# Build the .NET part
FROM microsoft/dotnet:2.1-sdk AS dotnet-build
WORKDIR /app
VOLUME [ "/app/data" ]
COPY Build/MSBuild/* ./Build/MSBuild/
COPY modules/Simulator/ ./modules/Simulator/
WORKDIR /app/modules/Simulator/Core
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build the static content
FROM node:11.6 AS node-build
WORKDIR /src

COPY ./modules/Simulator/Web/package.json ./Web/package.json
WORKDIR /src/Web/
RUN yarn install

RUN mkdir /src/Core
RUN mkdir /src/Core/Web
COPY ./modules/Simulator/Web/. ./
WORKDIR /src/Web
RUN yarn build

# Build runtime images
FROM microsoft/dotnet:2.1-runtime-stretch-slim
WORKDIR /app
COPY --from=dotnet-build /app/modules/Simulator/Core/out ./
COPY --from=dotnet-build /app/modules/Simulator/Core/data ./data
COPY --from=node-build /src/Core/wwwroot ./wwwroot
COPY modules/Simulator/application.json modules/Simulator/bounded-context.json ./

EXPOSE 80

#RUN useradd -r -ms /bin/bash moduleuser
#USER moduleuser

ENTRYPOINT ["dotnet", "Core.dll"]