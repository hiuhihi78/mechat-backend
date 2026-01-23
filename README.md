# MeChat Backend

## 🚀 Getting Started

This repository contains the backend services for **MeChat**, including API, persistence, and message broker consumers.

---

## 🗄️ Database Migration (Entity Framework Core)

### 1. Preparation

* Navigate to the `src` directory of the project
* Open **Command Prompt** or **Terminal** in that folder

### 2. Add a Migration

```bash
dotnet ef migrations add <MigrationName> \
  --project MeChat.Infrastructure.Persistence/MeChat.Infrastructure.Persistence.csproj \
  --startup-project MeChat.API/MeChat.API.csproj
```

> 🔹 Replace `<MigrationName>` with a meaningful name describing your change.

### 3. Apply Migration to Database

```bash
dotnet ef database update \
  --project MeChat.Infrastructure.Persistence/MeChat.Infrastructure.Persistence.csproj \
  --startup-project MeChat.API/MeChat.API.csproj
```

---

## ▶️ Run the Project

Set the following projects as **Startup Projects**:

* `MeChat.API`
* `MeChat.Infrastructure.MessageBroker.Consumer`

### Run via .NET CLI

```bash
dotnet run --project MeChat.API
```

```bash
dotnet run --project MeChat.Infrastructure.MessageBroker.Consumer
```

Or run both projects directly from **Visual Studio** using *Multiple Startup Projects*.

---

## 🐳 Docker – External Services

Docker is used to run external dependencies such as message brokers, databases, etc.

### Development Environment

```bash
docker compose -f docker-compose.develop.yml --verbose up
```

### Production Environment

```bash
docker compose -f docker-compose.production.yml --verbose up
```

---

## 🧰 Common Commands

### 🐳 Docker Commands

```bash
# List running containers
docker ps

# List all containers (including stopped ones)
docker ps -a

# List Docker images
docker images

# Stop a container
docker stop <container_name>

# Remove a container
docker rm <container_name>

# Remove an image
docker rmi <image_name>

# View container logs
docker logs -f <container_name>
```

---

### 🟥 Redis Commands

```bash
# Access Redis CLI
docker exec -it mechat-redis redis-cli

# Set a key
SET my_key "hello"

# Get a key
GET my_key

# Check if a key exists
EXISTS my_key

# Delete a key
DEL my_key

# List all keys (use carefully in production)
KEYS *

# Set key with expiration (seconds)
SETEX my_key 60 "hello"

# Check remaining TTL
TTL my_key
```

---

## 📌 Notes

* Docker commands are generic and can be used for any container
* Redis commands assume Redis is running in Docker with container name `mechat-redis`
* Avoid using `KEYS *` in production environments

Happy coding! 🚀
