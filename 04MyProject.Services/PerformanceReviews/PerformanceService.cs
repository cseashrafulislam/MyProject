using MyProject.Domain;
using MyProject.Repository.Abstructions.PerformanceReviews;
using MyProject.Services.Absructions.PerformanceReviews;
using MyProject.Services.Base;

namespace MyProject.Services.PerformanceReviews
{
    public class PerformanceService : BaseService<PerformanceReview>, IPerformanceService
	{
		#region Fields
		protected IPerformanceRepository _repository;
		#endregion
		public PerformanceService(IPerformanceRepository repository) : base(repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<PerformanceReview>> GetAllWithAssociateEmployee()
		{
			var performances = await _repository.GetAllWithAssociateEmployee();
			if (performances.Any())
				return performances;
			return Enumerable.Empty<PerformanceReview>();
		}

		public async Task<PerformanceReview> GetPerformanceReviewWithAssociateEmployee(int id)
		{
			return await _repository.GetPerformanceReviewWithAssociateEmployee(id);
		}
        public override ICollection<PerformanceReview> GetAll()
        {
            var performances = _repository.GetAll();

            if (performances.Any())
            {
				performances = performances
					.OrderBy(pr => pr.EmployeeId)
					.ThenBy(pr => pr.ReviewDate)
					.ToList();
            }
            return performances;
        }

        public async Task<IEnumerable<PerformanceReview>> GetAllWithAssociateEmployee(int pageSize, int pageNumber)
        {
            return await _repository.GetAllWithAssociateEmployee(pageSize, pageNumber);
        }
    }
}
