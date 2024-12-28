using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Domain;
using MyProject.Repositories.BaseRepository;
using MyProject.Repository.Abstructions.PerformanceReviews;
using X.PagedList.Extensions;

namespace MyProject.Repositories.PerformanceReviews
{
    public class PerformanceRepository : BaseRepository<PerformanceReview>, IPerformanceRepository
    {
        ApplicationDbContext db;
        public PerformanceRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
        public ApplicationDbContext Db
        {
            get
            {
                return db;
            }
            set
            {
                db = value;
            }
        }

        public async Task<IEnumerable<PerformanceReview>> GetAllWithAssociateEmployee(int pageSize = 10, int pageNumber = 1)
        {
            return db.PerformanceReviews
                .Where(e => !e.Deleted)
                .Include(e => e.Employee)
                .OrderByDescending(e => e.Id)
                .ToPagedList(pageNumber, pageSize);
        }

        public async Task<IEnumerable<PerformanceReview>> GetAllWithAssociateEmployee()
		{
            return await db.PerformanceReviews
                .Where(x => x.Deleted == false)
                .Include(e => e.Employee)
                .OrderBy(o=> o.EmployeeId)
                .ThenBy(o=> o.ReviewDate)
                .ToListAsync();
         }

		public async Task<PerformanceReview> GetPerformanceReviewWithAssociateEmployee(int id)
		{
			var performance =  await db.PerformanceReviews
				.Where(x => x.Deleted == false && x.Id == id)
				.Include(e => e.Employee)
				.FirstOrDefaultAsync();
            return performance!;
		}
	}
}
