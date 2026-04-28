# CaseManagement
Opsætning af DB

Hvis ef mangler, kan det installeres globalt:
dotnet tool install --global dotnet-ef

(cd til src)

📦 EF Core – Database Setup
1. Opret migration
dotnet ef migrations add InitialCreate --project CaseManagement.Infrastructure --startup-project CaseManagement.Api
2. Opdater database
dotnet ef database update --project CaseManagement.Infrastructure --startup-project CaseManagement.A