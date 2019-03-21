# Build the .NET part
FROM microsoft/dotnet:2.1-sdk AS dotnet-build
WORKDIR /app
VOLUME [ "/app/data" ]
COPY Build/MSBuild/* ./Build/MSBuild/
COPY modules/TimeSeriesSimulator/ ./modules/TimeSeriesSimulator/
WORKDIR /app/modules/TimeSeriesSimulator/Core
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build the static content
FROM node:11.6 AS node-build
WORKDIR /src

COPY ./modules/TimeSeriesSimulator/Web/package.json ./Web/package.json
WORKDIR /src/Web/
RUN yarn install

RUN mkdir /src/Core
RUN mkdir /src/Core/Web
COPY ./modules/TimeSeriesSimulator/Web/. ./
WORKDIR /src/Web
RUN yarn build

# Build runtime images
FROM microsoft/dotnet:2.1-runtime-stretch-slim
WORKDIR /app
COPY --from=dotnet-build /app/modules/TimeSeriesSimulator/Core/out ./
COPY --from=dotnet-build /app/modules/TimeSeriesSimulator/Core/data ./data
COPY --from=node-build /src/Core/wwwroot ./wwwroot
COPY modules/TimeSeriesSimulator/application.json modules/TimeSeriesSimulator/bounded-context.json ./

EXPOSE 80

#RUN useradd -r -ms /bin/bash moduleuser
#USER moduleuser

ENTRYPOINT ["dotnet", "Core.dll"]