using MyProject.Domain;
using MyProject.Domain.DbQuery;
using MyProject.Repository.Abstructions.Employees;
using MyProject.Services.Absructions.Employees;
using MyProject.Services.Base;

namespace MyProject.Services.Employees
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllWithAssociateDepartment(int pageSize, int pageNumber)
        {
            return await _employeeRepository.GetAllWithAssociateDepartment(pageSize, pageNumber);
        }
        public async Task<IEnumerable<Employee>> GetAllWithAssociateDepartment()
        {
            return await _employeeRepository.GetAllWithAssociateDepartment();
        }

        public async Task<Employee> GetEmployeeWithAssociateDepartment(int id)
        {
            return await _employeeRepository.GetEmployeeWithAssociateDepartment(id);
        }
        public async Task<IEnumerable<Employee>> GetEmployeeBySearchingCriteria(int pageSize, int pageNumber)
        {
            return await _employeeRepository.GetAllWithAssociateDepartment(pageSize, pageNumber);
        }
        public async Task<IEnumerable<EmployeeQuery>> GetEmployeeBySearchingCriteria(int pageSize, int pageNumber, string searchName, string searchDepartment, string searchPosition, decimal searchScore)
        {
            return await _employeeRepository.GetEmployeeBySearchingCriteria(pageSize, pageNumber,searchName,searchDepartment,searchPosition,searchScore);
        }
    }
}
