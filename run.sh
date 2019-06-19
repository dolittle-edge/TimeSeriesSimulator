#!/bin/bash
docker build -f ./Source/Dockerfile -t dolittle/timeseries-simulator . --build-arg CONFIGURATION="Debug"
iotedgehubdev start -d deployment.json -v