#MeChat-backend

## Migration
0. Preprare
	Go to source forlder and open cmd

1. Add Migration
dotnet ef migrations add {migration's name} --project MeChat.Infrastructure.Persistence\MeChat.Infrastructure.Persistence.csproj --startup-project MeChat.API\MeChat.API.csproj

2. Apply Migration
dotnet ef database update --project MeChat.Infrastructure.Persistence\MeChat.Infrastructure.Persistence.csproj --startup-project MeChat.API\MeChat.API.csproj

## Run project
	Make projects MeChat.API, MeChat.Infrastructure.MessageBroker.Consumer is startup project 
	Run project