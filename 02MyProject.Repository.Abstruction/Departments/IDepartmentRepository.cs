using MyProject.Domain;
using MyProject.Repository.Abstructions.Base;

namespace MyProject.Repository.Abstructions.Departments
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllWithAssociateManager(int pageSize, int pageNumber);
        Task<IEnumerable<Department>> GetAllWithAssociateManager();
        Task<Department> GetDepartmentWithAssociateManager(int id);
    }
}
