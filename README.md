# MyStarterProject
C# starter project using enterprise-friendly tooling and patterns.

This project is almost entirely about tooling, not code. Any functional code in the project only exists to show how the tooling works. The entire purpose of this project is to provide a functional ready-to-use enterprise-friendly dotnet project with everything you'd need to just drop it into a hackathon or even a startup setting and immediately start coding. 

No rights are reserved on this project. You are free to use it however you want. Note that you are still subject to whatever licenses govern the underlying tooling. 

# Running the application
1) If on Windows: use WSL Ubuntu. Run VS Code in WSL mode. 
2) Install all the dependencies. You should be able to figure that out. Install Docker, Docker-compose, cmake, dotnet 9, etc etc. 
3) Go to the project root and run `make`. This is the entrypoint to the application. The makefile is mainly just aliases for other commands so you don't need to remember the exact syntax of the docker-compose commands. 

The most important commands in the makefile are the debug commands. They are configured to allow you to run only part of the app in VS code (and the rest in docker) so you can work on specific projects rather than the entire application at once. 

There should be documentation on everything meaningful. 

# Testing
TODO

# Tooling
TODO

## Dockerizing
To briefly explain how the application is dockerized:

The entire application is based on a single image in Dockerfile.Apps because one image is faster to build and deploy than multiple images. Every project is on that single image. It's a multi-stage image using an SDK builder then copying over to a runtime image; very standard stuff. Despite everything being on that single image though, the image should only be running a single application at a time. This system uses different shell scripts for entrypoints for each use case of that image. 

The Dockerfiles/Scripts folder is copied to the runtime image. Refer to the docker-compose file for proper Entrypoint syntax. 

## Docker-Compose
There are comments in the Compose file but to summarize:
The default entrypoing is just `exit 0` so the `app-image` service just runs and exits. Since that service has both Image and Build then it will build the image then save it as that image name. The other apps based off that image (currently Web, WebHost, Migrator) have Image but *not* Build, so they will use the image created by the `app-image` service instead. 

The standard `docker compose up --build` aka `make docker-build-and-run` will perform the following steps based on its depends_on chain:
1) Run the Postgres DB using a persistant volume `postgres_data` (and also other tooling)
2) Run app-image, which builds the base image once and only once
3) Run dbmigrator service, which runs the entity framework migrations against the Postgres DB
4) Run Web and WebHost

## Swagger
TODO

## Logging
TODO

## Coverage
TODO
