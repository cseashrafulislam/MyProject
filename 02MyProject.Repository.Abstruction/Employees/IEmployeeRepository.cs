using MyProject.Domain;
using MyProject.Domain.DbQuery;
using MyProject.Repository.Abstructions.Base;

namespace MyProject.Repository.Abstructions.Employees
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllWithAssociateDepartment(int pageSize, int pageNumber);
        Task<IEnumerable<Employee>> GetAllWithAssociateDepartment();
        Task<Employee> GetEmployeeWithAssociateDepartment(int id);
        Task<IEnumerable<EmployeeQuery>> GetEmployeeBySearchingCriteria(int pageSize, int pageNumber, string searchName, string searchDepartment, string searchPosition, decimal searchScore);
    }
}
