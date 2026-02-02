# ========== BUILD ==========
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY MeChat.sln ./
COPY src ./src
COPY test ./test

WORKDIR /app
RUN dotnet restore MeChat.sln

RUN dotnet publish ./src/MeChat.API/MeChat.API.csproj -c Release -o /app/publish --no-restore

# ========== RUNTIME ==========
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "MeChat.API.dll"]
