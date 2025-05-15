using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Profiles;
using EmployeeManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Services (Dependency Injection)
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Add Services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Employee Management API",
        Version = "v1",
        Description = "An API to manage employees and departments.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Abu Hassan", 
            Email = "abu.t.hassan@gmail.com",
            Url = new Uri("https://github.com/aaabbbuuu/EmployeeManagementAPI")
        }
    });
});


var app = builder.Build();

// Middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>(); // Add global exception handler

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at app's root
    });
}
else
{
    // For production, you might want to add HSTS, HTTPS redirection, etc.
    app.UseExceptionHandler("/error"); // A generic error handler page/endpoint for production
    app.UseHsts();
}

app.UseHttpsRedirection(); // Added for good practice

app.MapControllers();
app.Run();