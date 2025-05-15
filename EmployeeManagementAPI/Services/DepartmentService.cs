using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(AppDbContext context, IMapper mapper, ILogger<DepartmentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            _logger.LogInformation("Fetching all departments");
            var departments = await _context.Departments.Include(d => d.Employees).ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            _logger.LogInformation("Fetching department with ID: {DepartmentId}", id);
            var department = await _context.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == id);
            if (department == null)
            {
                _logger.LogWarning("Department with ID: {DepartmentId} not found", id);
                return null;
            }
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto departmentDto)
        {
            _logger.LogInformation("Creating new department with name: {DepartmentName}", departmentDto.Name);
            var department = _mapper.Map<Department>(departmentDto);
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Department created with ID: {DepartmentId}", department.Id);
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<bool> UpdateDepartmentAsync(int id, UpdateDepartmentDto departmentDto)
        {
            _logger.LogInformation("Updating department with ID: {DepartmentId}", id);
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                _logger.LogWarning("Department with ID: {DepartmentId} not found for update", id);
                return false;
            }

            _mapper.Map(departmentDto, department);
            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Department with ID: {DepartmentId} updated successfully", id);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating department ID: {DepartmentId}", id);
                return false; // Or rethrow, depending on desired handling
            }
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            _logger.LogInformation("Attempting to delete department with ID: {DepartmentId}", id);
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                _logger.LogWarning("Department with ID: {DepartmentId} not found for deletion", id);
                return false;
            }

            // Optional: Check if department has employees before deleting
            var hasEmployees = await _context.Employees.AnyAsync(e => e.DepartmentId == id);
            if (hasEmployees)
            {
                _logger.LogWarning("Cannot delete department ID: {DepartmentId} as it has associated employees.", id);
                // Consider throwing a custom exception or returning a specific result
                // For now, we'll prevent deletion if employees exist.
                // You could also implement cascading deletes or set DepartmentId to null for employees.
                return false;
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Department with ID: {DepartmentId} deleted successfully", id);
            return true;
        }
    }
}