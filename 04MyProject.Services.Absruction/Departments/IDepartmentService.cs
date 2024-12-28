using MyProject.Domain;
using MyProject.Services.Absructions.Base;

namespace MyProject.Services.Absructions.Departments
{
    public interface IDepartmentService : IService<Department>
    {
        Task<IEnumerable<Department>> GetAllWithAssociateManager(int pageSize = 10, int pageNumber = 1);
        Task<IEnumerable<Department>> GetAllWithAssociateManager();
        Task<Department> GetDepartmentWithAssociateManager(int id);
    }
}
