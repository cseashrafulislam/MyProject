using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Domain;
using MyProject.Domain.DbQuery;
using MyProject.Repositories.BaseRepository;
using MyProject.Repository.Abstructions.Employees;
using X.PagedList.Extensions;

namespace MyProject.Repositories.Employees
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        ApplicationDbContext db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
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

        public async Task<IEnumerable<Employee>> GetAllWithAssociateDepartment(int pageSize = 10, int pageNumber = 1)
        {
            return db.Employees
                .Where(e => !e.Deleted)
                .Include("Department")
                .OrderByDescending(e => e.Id)
                .ToPagedList(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Employee>> GetAllWithAssociateDepartment()
        {
            return await db.Employees
                .Where(e => !e.Deleted)
                .Include("Department")
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllWithAssociateDepartmentAsync()
        {
            return await db.Employees
                .Where(e => !e.Deleted)
                .Include(e => e.Department)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<EmployeeQuery>> GetEmployeeBySearchingCriteria(int pageSize, int pageNumber, string searchName, string searchDepartment, string searchPosition, decimal searchScore)
        {
            try
            {
                searchName = searchName ?? "NULL";
                searchDepartment = searchDepartment ?? "NULL";
                searchPosition = searchPosition ?? "NULL";
                searchScore = searchScore == 0 ? 0 : searchScore;

                string sql = $"EXEC EmpSpGetEmployeeBySearchingCriteria {searchName},{searchDepartment},{searchPosition},{searchScore},{pageSize},{pageNumber}";

                var employees = await db.EmployeeQuery
                                        .FromSqlRaw(sql)
                                        .AsNoTracking()
                                        .ToListAsync();

                var paginatedEmp =  employees.ToPagedList(pageSize, pageNumber).AsEnumerable();
                return paginatedEmp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Employee> GetEmployeeWithAssociateDepartment(int id)
        {
            var employee = await db.Employees
                .Where(e => !e.Deleted)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee is not null)
                return employee;
            return null!;
        }

    }
}
