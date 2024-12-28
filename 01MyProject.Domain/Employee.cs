using MyProject.Domain.Base;

namespace MyProject.Domain
{
    public class Employee : BaseDomain
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; } // FK for One-to-Many
        public Department Department { get; set; }
        public string Position { get; set; } //Designation
        public DateTime JoiningDate { get; set; } 
        public decimal? Salary { get; set; } 
        public string Gender { get; set; }
        public bool IsActive { get; set; } // Is Employee Active or Not
    }
}
