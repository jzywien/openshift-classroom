# Running Db Server in a Docker Container

MS SQL Server
```
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=Passw0rd!' -p 1433:1433 --name mssql-server -d mcr.microsoft.com/mssql/server:2019-latest
```

Postgres
```
docker run --name postgres-db -p 5432:5432 -e POSTGRES_PASSWORD="Passw0rd" -d postgres
```
# Dotnet EF tools

In order to run `dotnet ef` commands from the CLI, you need to install the Dotnet EF command line tools.
They were previously bundled with the dotnet cli, but were removed in ef core 3.0+

```
dotnet tool install --global dotnet-ef
```

## Create Initial Migrations

The Initial db migrations will take a snapshot of the db when the application is created.

```
dotnet ef --startup-project ../Classroom.WebApi/Classroom.WebApi.csproj  migrations add initial
```

## Add Additional Migrations

When the internal data model changes, you can create DB migrations which will mirror the code changes to the DB.

```
dotnet ef --startup-project ../Classroom.WebApi/Classroom.WebApi.csproj  migrations add <NAME OF MIGRATION>
```