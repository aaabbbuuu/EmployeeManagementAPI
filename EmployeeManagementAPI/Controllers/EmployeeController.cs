using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Services;

namespace EmployeeManagementAPI.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                _logger.LogWarning("Employee with ID {EmployeeId} not found.", id);
                return NotFound(new { Message = $"Employee with ID {id} not found." });
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newEmployee = await _employeeService.CreateEmployeeAsync(createEmployeeDto);
            if (newEmployee == null)
            {
                _logger.LogWarning("Failed to create employee. Department ID {DepartmentId} might not exist.", createEmployeeDto.DepartmentId);
                return BadRequest(new { Message = $"Department with ID {createEmployeeDto.DepartmentId} not found." });
            }
            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var success = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto);
            if (!success)
            {
                _logger.LogWarning("Failed to update employee with ID {EmployeeId}. It might not exist or target department is invalid.", id);
                return NotFound(new { Message = $"Employee with ID {id} not found or associated department ID {updateEmployeeDto.DepartmentId} is invalid." });
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            if (!success)
            {
                _logger.LogWarning("Failed to delete employee with ID {EmployeeId}. It might not exist.", id);
                return NotFound(new { Message = $"Employee with ID {id} not found." });
            }
            return NoContent();
        }
    }
}