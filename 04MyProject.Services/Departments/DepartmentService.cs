using MyProject.Domain;
using MyProject.Repository.Abstructions.Departments;
using MyProject.Services.Absructions.Departments;
using MyProject.Services.Base;

namespace MyProject.Services.Departments
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Fields
        protected readonly IDepartmentRepository _repository;
        #endregion

        public DepartmentService(IDepartmentRepository repository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Department>> GetAllWithAssociateManager(int pageSize, int pageNumber)
        {
            try
            {
                return await _repository.GetAllWithAssociateManager(pageSize, pageNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching departments with managers", ex);
            }
        }

        public async Task<Department> GetDepartmentWithAssociateManager(int id)
        {
            try
            {
                return await _repository.GetDepartmentWithAssociateManager(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching department with ID {id}", ex);
            }
        }

        public override async Task<bool> Add(Department entity)
        {
            await ValidateManagerAssignment(entity.ManagerId);
            if (entity.ManagerId == 0)
            {
                entity.ManagerId = null;
            }
            return await _repository.Add(entity);
        }

        public override async Task<bool> Update(Department entity)
        {
            await ValidateManagerAssignment(entity.ManagerId);
            if (entity.ManagerId == 0)
            {
                entity.ManagerId = null;
            }
            return await _repository.Update(entity);
        }

        private async Task ValidateManagerAssignment(int? managerId)
        {
            if (managerId != null && managerId != 0 && await IsManagerAssigned(managerId))
            {
                throw new Exception("This manager is already assigned to a department");
            }
        }

        private async Task<bool> IsManagerAssigned(int? managerId)
        {
            if (managerId == 0)
                return false;

            var managers = await _repository.GetAllWithAssociateManager();
            return managers.Any(item => item.ManagerId == managerId);
        }

        public async Task<IEnumerable<Department>> GetAllWithAssociateManager()
        {
            return await _repository.GetAllWithAssociateManager();
        }
    }
}