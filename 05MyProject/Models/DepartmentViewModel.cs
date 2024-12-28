using System.ComponentModel.DataAnnotations;
using MyProject.Domain;

namespace MyProject.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public int? ManagerId { get; set; }

        public Employee Manager { get; set; }

        public decimal? Budget { get; set; }

        public List<Employee> Managers { get; set; } 
    }
}
