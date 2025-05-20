Hello.

To make a migration, run 
`dotnet ef migrations add NAME_HERE --project src/DataSource --startup-project src/WebHost`
from the repository root.

This should automatically create a migration to fix the schema to match any changes you've made to the model objects. No need to do anything manually. 

... In theory. I think. 

Then just use the makefile to update the DB. 

Note for development: once a migration has been run it will not be run again EVEN IF YOU MANUALLY CHANGE THE MIGRATION. Modifying these files manually has consequences, make sure you understand them. 