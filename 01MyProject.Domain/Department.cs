using MyProject.Domain.Base;

namespace MyProject.Domain
{
    public class Department : BaseDomain
    {
        public string Name { get; set; }
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; } // One-to-one relationship
        public decimal? Budget { get; set; }
    }
}
