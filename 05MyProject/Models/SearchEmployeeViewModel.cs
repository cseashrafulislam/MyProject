using MyProject.Domain;

namespace MyProject.Models
{
    public class SearchEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; } // FK for One-to-Many
        public string DepartmentName { get; set; }
        public string Position { get; set; } //Designation
        public DateTime JoiningDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? ReviewScore { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; } // Is Employee Active or Not
    }
}
