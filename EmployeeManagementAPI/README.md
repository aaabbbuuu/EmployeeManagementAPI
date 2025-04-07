# Employee Management API

A simple Web API built with **ASP.NET Core**, **Entity Framework Core**, and **SQL Server** to manage employees and departments.

## Features

- 🔍 View all departments and employees
- ➕ Add new departments or employees
- 🔗 Employees are linked to departments
- 🧱 Uses SQL Server and EF Core Code-First migrations
- 📦 Swagger UI for easy testing

## Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Express
- Swagger / Swashbuckle

### 🔧 Prerequisites

- [.NET 7 SDK or later](https://dotnet.microsoft.com/en-us/download)
- [SQL Server Express or LocalDB](https://learn.microsoft.com/en-us/sql/sql-server/editions-and-components-of-sql-server)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) with ASP.NET workload

### 📦 Setup

1. **Clone the repo**
   ```bash
   git clone https://github.com/your-username/EmployeeManagementAPI.git
   cd EmployeeManagementAPI
   ```
2.  **Configure connection string**
	In *appsettings.json*:
	```
	"ConnectionStrings": {
		"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;"
	}
	```
3.  **Run Migrations**
	dotnet ef database update
4.  **Run the app**
	dotnet run
5.  **Test via Swagger**
	Visit: https://localhost:5001/swagger

### Sample Payloads
**Create Department**
```json
{
  "name": "Engineering",
  "description": "Software Development"
}

```

**Create Employee**
```json
{
  "name": "John Doe",
  "position": "Backend Developer",
  "salary": 85000.00,
  "departmentId": 1
}

```