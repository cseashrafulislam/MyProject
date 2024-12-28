using MyProject.Domain;
using MyProject.Domain.DbQuery;
using MyProject.Services.Absructions.Base;

namespace MyProject.Services.Absructions.Employees
{
    public interface IEmployeeService : IService<Employee>
    {
        Task<IEnumerable<Employee>> GetAllWithAssociateDepartment(int pageSize, int pageNumber);
        Task<IEnumerable<Employee>> GetAllWithAssociateDepartment();
        Task<Employee> GetEmployeeWithAssociateDepartment(int id);
        Task<IEnumerable<EmployeeQuery>> GetEmployeeBySearchingCriteria(int pageSize, int pageNumber, string searchName, string searchDepartment, string searchPosition, decimal searchScore);
    }
}
