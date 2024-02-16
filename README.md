# Background Workers

This project is a sample of background tasks with hosted services in .NET 8.

## Whats Including

### Workers
* ASP.NET Core Console Application in .NET 8
* **Hosted services** implementation
* **Repository Pattern** implementation
* **AWS S3** and **Slack** integration
* **Redis database** connection and containerization
* **Options Pattern** in ASP.NET Core for Slack, AWS, and worker options

### Docker Compose establishment with all services on docker;
- Containerization of worker
- Containerization of database
- Override Environment variables


At the root directory which include **docker-compose.yml** files, run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
