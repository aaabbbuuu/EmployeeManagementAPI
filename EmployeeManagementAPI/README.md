# Employee Management API

[![.NET Build and Test](https://github.com/aaabbbuuu/EmployeeManagementAPI/actions/workflows/dotnet.yml/badge.svg)](https://github.com/aaabbbuuu/EmployeeManagementAPI/actions/workflows/dotnet.yml)
An enhanced Web API built with **ASP.NET Core**, **Entity Framework Core**, and **SQL Server** to manage employees and departments. This project has been refactored from a simple tutorial to incorporate best practices like a service layer, Data Transfer Objects (DTOs), and comprehensive CRUD operations.

## ✨ Key Enhancements

- **Service Layer:** Business logic is now encapsulated in services, promoting separation of concerns and testability.
- **Data Transfer Objects (DTOs):** Dedicated models for API requests and responses, improving API contract stability and preventing over-posting.
- **Full CRUD Operations:** Comprehensive Create, Read, Update, and Delete operations for both `Employees` and `Departments`.
- **AutoMapper:** Streamlined mapping between DTOs and Entity Framework Core entities.
- **Global Error Handling:** Centralized exception handling for consistent error responses.
- **Improved Validation:** Enhanced input validation using Data Annotations.
- **Asynchronous Operations:** Consistent use of `async/await` for non-blocking database interactions.
- **Structured Logging:** Utilizes `ILogger` for better application monitoring.

## 🚀 Features

- **Department Management:**
  - Create, View (all & by ID), Update, and Delete departments.
  - View employees associated with each department.
- **Employee Management:**
  - Create, View (all & by ID), Update, and Delete employees.
  - Link employees to existing departments.
- **Relational Data:** Employees are linked to departments via foreign keys.
- **Database:** Uses SQL Server with EF Core Code-First migrations.
- **API Documentation:** Swagger UI for easy API exploration and testing.

## 🛠️ Technologies

- ASP.NET Core Web API (.NET 7)
- Entity Framework Core
- SQL Server Express (or LocalDB)
- AutoMapper
- Swagger / Swashbuckle
- xUnit & Moq (for planned unit tests)

## 🏗️ Project Structure Highlights

- `Controllers/`: API controllers, lean and focused on request/response handling.
- `Services/`: Contains business logic (e.g., `DepartmentService`, `EmployeeService`).
- `DTOs/`: Data Transfer Objects for API communication (e.g., `EmployeeDto`, `CreateDepartmentDto`).
- `Models/`: Entity Framework Core entity classes (e.g., `Employee`, `Department`).
- `Data/`: `AppDbContext` for database interaction.
- `Profiles/`: AutoMapper mapping configurations.
- `Middleware/`: Custom middleware, such as `GlobalExceptionHandlerMiddleware`.

## 🔧 Prerequisites

- [.NET 7 SDK or later](https://dotnet.microsoft.com/en-us/download)
- [SQL Server Express or LocalDB](https://learn.microsoft.com/en-us/sql/sql-server/editions-and-components-of-sql-server)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) with ASP.NET workload (or any preferred .NET IDE)

## 📦 Setup & Running

1. **Clone the repo**
    ```bash
    git clone https://github.com/your-username/EmployeeManagementAPI.git  # Replace your-username
    cd EmployeeManagementAPI
    ```

2. **Configure Connection String**
    Ensure your `appsettings.json` (or `appsettings.Development.json`) has the correct SQL Server connection string:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```

3. **Apply Migrations**
    ```bash
    dotnet ef database update
    ```

4. **Run the Application**
    ```bash
    dotnet run
    ```

5. **Access the API & Swagger**
    Once running, the API will typically be available at:
    - `https://localhost:7XXX`
    - `http://localhost:5XXX`

    Swagger UI will be available at the application root:
    - e.g., `https://localhost:7123/` (check console output for actual port)

## 📝 API Endpoints

**Departments:**
- `GET /api/Department`
- `GET /api/Department/{id}`
- `POST /api/Department`
- `PUT /api/Department/{id}`
- `DELETE /api/Department/{id}`

**Employees:**
- `GET /api/Employee`
- `GET /api/Employee/{id}`
- `POST /api/Employee`
- `PUT /api/Employee/{id}`
- `DELETE /api/Employee/{id}`

### 📤 Sample Payloads

**Create Department**
```json
{
  "name": "Marketing",
  "description": "Handles all marketing and advertising efforts."
}
```

**Create Employee**
```json
{
  "name": "Jane Smith",
  "position": "Marketing Specialist",
  "salary": 65000.00,
  "departmentId": 2 
}
```

> Ensure `departmentId` refers to an existing department.

## 💡 Future Enhancements (Planned)

- Comprehensive Unit and Integration Tests (xUnit, MSTest, Moq)
- Authentication & Authorization with JWT
- API Versioning
- Advanced Logging (Serilog, Application Insights)
- Containerization with Docker
- CI/CD with GitHub Actions
- Pagination and Filtering

## 🤝 Contributing

Contributions are welcome!

1. Fork the repository.
2. Create a new branch: `git checkout -b feature/AmazingFeature`
3. Make your changes.
4. Commit: `git commit -m 'Add some AmazingFeature'`
5. Push: `git push origin feature/AmazingFeature`
6. Open a Pull Request.

Please ensure your code follows the style and includes documentation/tests as appropriate.

---
