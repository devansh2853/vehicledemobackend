# Vehicle Demo Backend

## Deployment Steps

1. Create a Resource Group
2. Create an Azure SQL Server
3. Create an Azure SQL Database on the created server
4. Create an Azure App Service and add the GitHub repository in the Deployment Center
5. Set the database connection string in:
   - Azure App Service (Connection Strings)
   - GitHub repository secrets

---

## Database CI/CD Setup (Choose ONE)

### Migrating on Application Startup

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VehicleProjectContext>();
    db.Database.Migrate();
}
```

---

### Creating an EF Core Migration Bundle

```bash
- name: Create EF Migration bundle
  run: |
    dotnet tool install --global dotnet-ef
    dotnet ef migrations bundle \
      --project VehicleProject/VehicleProject.csproj \
      --startup-project VehicleProject/VehicleProject.csproj \
      --configuration Release \
      --output efbundle

- name: Run EF Migrations
  run: ./efbundle --connection '${{ secrets.DB_CONNECTION_STRING }}'
```

---

### Creating SQL Migration Scripts

```bash
dotnet ef migrations script \
  --idempotent \
  --project VehicleProject/VehicleProject.csproj \
  --startup-project VehicleProject/VehicleProject.csproj \
  --configuration Release \
  --output migrations.sql
```

---

## Frontend Deployment

7. Create an Azure Static Web App
8. Add the frontend GitHub repository in the Deployment Center
9. Set required environment variables in:
   - GitHub secrets
   - Azure Static Web App configuration

---

## Backend Configuration

10. Configure CORS in the backend to allow frontend access

---
