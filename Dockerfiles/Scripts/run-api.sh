#!/bin/sh
set -e

#####################################################
## Setup environment variables and run server api
#####################################################

## Reminder: Do not log unsafe env variables (certs, passwords, etc).
echo ""
echo "##################################################"
echo "######### Runtime Environment Variables ##########"
echo "##################################################"

echo "DOTNET_ENVIRONMENT: $DOTNET_ENVIRONMENT"
echo "API_CONTAINER: $API_CONTAINER"
echo "APP_TO_RUN: $1"

# switch to the app folder to ensure the configuration is loaded from there
cd ./src/$1/bin/Release/net9.0/publish/
dotnet $1.dll