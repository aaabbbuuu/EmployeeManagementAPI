using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(AppDbContext context, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            _logger.LogInformation("Fetching all employees");
            var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            _logger.LogInformation("Fetching employee with ID: {EmployeeId}", id);
            var employee = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                _logger.LogWarning("Employee with ID: {EmployeeId} not found", id);
                return null;
            }
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto?> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            _logger.LogInformation("Creating new employee with name: {EmployeeName}", employeeDto.Name);
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == employeeDto.DepartmentId);
            if (!departmentExists)
            {
                _logger.LogWarning("Department with ID: {DepartmentId} not found for employee creation", employeeDto.DepartmentId);
                return null; // Indicate department not found
            }

            var employee = _mapper.Map<Employee>(employeeDto);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Employee created with ID: {EmployeeId}", employee.Id);

            // Fetch the created employee with department info for the DTO
            var createdEmployeeWithDept = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == employee.Id);
            return _mapper.Map<EmployeeDto>(createdEmployeeWithDept);
        }

        public async Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto)
        {
            _logger.LogInformation("Updating employee with ID: {EmployeeId}", id);
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                _logger.LogWarning("Employee with ID: {EmployeeId} not found for update", id);
                return false;
            }

            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == employeeDto.DepartmentId);
            if (!departmentExists)
            {
                _logger.LogWarning("Department with ID: {DepartmentId} not found for employee update", employeeDto.DepartmentId);
                return false; // Or handle as a bad request if department change is invalid
            }

            _mapper.Map(employeeDto, employee);
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Employee with ID: {EmployeeId} updated successfully", id);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating employee ID: {EmployeeId}", id);
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation("Attempting to delete employee with ID: {EmployeeId}", id);
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                _logger.LogWarning("Employee with ID: {EmployeeId} not found for deletion", id);
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Employee with ID: {EmployeeId} deleted successfully", id);
            return true;
        }
    }
}