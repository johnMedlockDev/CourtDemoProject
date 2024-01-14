

*For DB*
Run/deploy this image:
- https://hub.docker.com/_/microsoft-mssql-server/

run this command from CourtDemoProject.CaseManagementSystem.Api to create the tables:
- dotnet ef database update -- --connection-string "Server=localhost;Database=CaseManagementSystem;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=true;"