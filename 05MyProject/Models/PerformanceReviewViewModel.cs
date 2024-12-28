using MyProject.Domain;

namespace MyProject.Models
{
    public class PerformanceReviewViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateOnly ReviewDate { get; set; }
        public decimal ReviewScore { get; set; }
        public string ReviewNotes { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
