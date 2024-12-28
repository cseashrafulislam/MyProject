using MyProject.Domain;
using MyProject.Repository.Abstructions.Base;

namespace MyProject.Repository.Abstructions.PerformanceReviews
{
    public interface IPerformanceRepository : IRepository<PerformanceReview>
    {
        Task<IEnumerable<PerformanceReview>> GetAllWithAssociateEmployee(int pageSize, int pageNumber);
        Task<IEnumerable<PerformanceReview>> GetAllWithAssociateEmployee();
		Task<PerformanceReview> GetPerformanceReviewWithAssociateEmployee(int id);
	}
}
