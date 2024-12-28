using System.ComponentModel.DataAnnotations;
using MyProject.Domain;

namespace MyProject.Models
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Position { get; set; } //Designation
        public DateTime JoiningDate { get; set; }
        public decimal? Salary { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public List<Department> Departments { get; set; }
    }
}
