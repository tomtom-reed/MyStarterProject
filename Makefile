.DEFAULT_GOAL := help

current_dir = $(shell pwd)

help: ## Get a description of all available commands
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | 	awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'

docker-build: ## Build containers for local usage
	docker compose build

docker-run: docker-build ## Run local API's
	docker compose up

docker-build-and-run: ## alias for docker compose up --build
	docker compose up --build

docker-debug-web: ## Run everything except the web API
	(Web_Hostname=https://web.debug.local.starterproject.tomtomreed:5001 docker compose up postgres webhost)

docker-debug-webhost: ## Run everything except the webhost API
	(WebHost_Hostname=https://webhost.debug.local.starterproject.tomtomreed:5002 docker compose up postgres web)

### Utils
refresh-db: ## Drops and recreates the local database to the latest version
	docker compose -f docker-compose.yml -f docker-compose/docker-compose.refresh-db.yml up --build refresh-db
	docker compose restart web webhost

docker-purge-local-db: ## Destroys the local volume used for the DB. ALL DATA WILL BE LOST.
	docker compose down
	docker volume rm postgres_data

docker-clean: ## shuts down all containers and removes everything
	docker compose down -v
	docker system prune --all --force

docker-clean-rebuild: ## clean everything and then recreate and host all containers
	make docker-clean
	docker compose up --build

### apidocs website helpers
run-unit-tests: ## Run all unit tests across a custom test db
	docker compose -f docker-compose.yml -f docker-compose/docker-compose.apidocs.yml up --build unit-tests-db-setup unit-tests apidocs-web --remove-orphans

### Dotnet test helpers
test-db-refresh: ## Refresh the .Test db used for unit test runs
	docker compose up postgres -d
	(Database__Database=TomTomReed.StarterProject.Test dotnet ./src/Apps/Apps.DatabaseMigrator/bin/*/net8.0/Apps.DatabaseMigrator.dll --replace)

test-all: ## Refresh the .Test db and run all unit tests
	make test-db-refresh
	dotnet build
	(Database__Database=TomTomReed.StarterProject.Test dotnet dotnet test -v q --no-build);
