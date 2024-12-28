using MyProject.Domain.Base;

namespace MyProject.Domain
{
    public class PerformanceReview : BaseDomain
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateOnly ReviewDate { get; set; }
        public decimal ReviewScore { get; set; }
        public string ReviewNotes { get; set; }
    }
}
