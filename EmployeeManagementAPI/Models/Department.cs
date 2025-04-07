using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
