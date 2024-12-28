using MyProject.Domain;
using MyProject.Services.Absructions.Base;

namespace MyProject.Services.Absructions.PerformanceReviews
{
    public interface IPerformanceService : IService<PerformanceReview>
    {
        Task<IEnumerable<PerformanceReview>> GetAllWithAssociateEmployee(int pageSize, int pageNumber);
        Task<IEnumerable<PerformanceReview>> GetAllWithAssociateEmployee();
        Task<PerformanceReview> GetPerformanceReviewWithAssociateEmployee(int id);
    }
}
