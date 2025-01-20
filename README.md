# AuditEvent.Worker application

## About working logic

![](.\working_logic.png)

This application connect to AMQP broker specific topic (`auditevent`) and try to process subscribed messages and saves to database.
If you would like to use other topic you need to add a new topic name to `MessageProcessorFactory`.
## Settings

You can set implemented MongoDB connection string and database name and you can set AMQP broker connection settings
(`appsettings.json` example)

``` 
{
  "Queue": {
    "HostName": "localhost"
  },
  "Database": {
    "Connection": "mongodb://localhost:27017",
    "DatabaseName": "test"
  }
}
```

## Usage

### Restore

`dotnet restore .\AuditEvent.Worker\AuditEvent.Worker.csproj`

### Build

`dotnet build .\AuditEvent.Worker\AuditEvent.Worker.csproj`

### Run

`dotnet run --project .\AuditEvent.Worker\AuditEvent.Worker.csproj`

# AuditEvent.API application

This application serves two endpoints which helps to get datas from database and check data integrity.

## Endpoints:

- `/api/Message/IntegrityCheck` - Checks data integrity in database, returns boolean
- `/api/Message/GetAll` - Returns all processed AuditEventMessage object from database

## Settings

You can set implemented MongoDB connection string and database name.
(`appsettings.json` example)

``` 
{ 
    "Database": {
        "Connection": "mongodb://localhost:27017",
        "DatabaseName": "test"
    }
}
```

## Usage

### Restore

`dotnet restore .\AuditEvent.Api\AuditEvent.Api.csproj`

### Build

`dotnet build .\AuditEvent.Api\AuditEvent.Api.csproj`

### Run

`dotnet run --project .\AuditEvent.Api\AuditEvent.Api.csproj`