using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [MaxLength(100, ErrorMessage = "Employee name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Position is required.")]
        [MaxLength(50, ErrorMessage = "Position cannot exceed 50 characters.")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Salary is required.")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 1000000.00, ErrorMessage = "Salary must be between 0.01 and 1,000,000.00.")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Department ID is required.")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}