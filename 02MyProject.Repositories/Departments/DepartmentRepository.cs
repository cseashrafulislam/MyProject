using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Domain;
using MyProject.Repositories.BaseRepository;
using MyProject.Repository.Abstructions.Departments;
using X.PagedList;
using X.PagedList.Extensions;

namespace MyProject.Repositories.Departments
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        ApplicationDbContext db;
        public DepartmentRepository(ApplicationDbContext db) : base(db)
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

        public async Task<IEnumerable<Department>> GetAllWithAssociateManager(int pageSize = 10, int pageNumber = 1)
        {
            var departments = db.Departments
                .Where(x => x.Deleted == false)
                .Include(d => d.Manager)
                .OrderByDescending(d => d.Id)
                .ToPagedList(pageNumber, pageSize);
            return departments;
        }
        public async Task<IEnumerable<Department>> GetAllWithAssociateManager()
        {
            return await db.Departments
                .Where(x => x.Deleted == false)
                .Include(e => e.Manager)
                .OrderByDescending(d => d.Id)
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentWithAssociateManager(int id)
        {
            var employee = await db.Departments
                                .Where(e => e.Deleted == false)
                                .Include(e => e.Manager)
                                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee is not null)
                return employee;
            return null!;
        }
    }
}
