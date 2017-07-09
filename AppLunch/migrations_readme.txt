Use Package Manager Console for commands. 

[Initial Setup]
-Initially ran "Enable-Migrations". This created the \Migrations.Configuration.cs (for the identity context)

-Since DB was already created, ran "Add-Migration InitialCreate -IgnoreChanges" then "Update-Database". This basically "synced" the context with the already created db.

-Next added first name and last name properties to the identity db context. Ran "Add-Migration IdentityNameFields" then "Update-Database". This added those new properties to the table.

[Migrations]
- "Add-Migration [MigrationName]" (where [MigrationName is just a quick description of what changed]) to add the migration.
- "Update-Database" to push the changes in the context to the database.