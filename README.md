# CaseManagement
Opsætning af DB

Hvis ef mangler, kan det installeres globalt:
dotnet tool install --global dotnet-ef

I dette projekt gør jeg brug af PostGres med DBeaver.
download postgres.
https://www.enterprisedb.com/downloads/postgres-postgresql-downloads
Hent Dbeaver

Opret en connection:

Host: localhost
Port: 5432
Database: postgres
Username: postgres
Password:

Opret Db:
CREATE DATABASE casemanagementdb;

Tjek connection string I Api/appsettings.

(cd til src)

📦 EF Core – Database Setup
1. Opret migration
dotnet ef migrations add InitialCreate --project CaseManagement.Infrastructure --startup-project CaseManagement.Api
2. Opdater database
dotnet ef database update --project CaseManagement.Infrastructure --startup-project CaseManagement.A
