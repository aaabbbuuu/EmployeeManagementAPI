using EmployeeManagementAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto departmentDto);
        Task<bool> UpdateDepartmentAsync(int id, UpdateDepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}