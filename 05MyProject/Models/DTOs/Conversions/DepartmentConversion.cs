using MyProject.Domain;

namespace MyProject.Models.DTOs.Conversions
{
    public class DepartmentConversion
    {
        public static Department ToEntity(DepartmentDTO departmentDTO) => new Department()
        {
            Id = departmentDTO.Id,
            Name = departmentDTO.Name,
            Budget = departmentDTO.Budget,
            ManagerId = departmentDTO.ManagerId
        };

        public static (DepartmentDTO?, IEnumerable<DepartmentDTO>?) FromEntity(Department? department, IEnumerable<Department>? departments)
        {
            //return single employee
            if (department is not null || departments is null)
            {
                var singleDepartment = new DepartmentDTO(department!.Id, department.Name, department.Budget, department.ManagerId);
                return (singleDepartment, null);
            }
            //return list
            if (departments is not null || department is null)
            {
                var dptList = departments!.Select(p => new DepartmentDTO(department!.Id, department.Name, department.Budget, department.ManagerId)).ToList();
                return (null, dptList);
            }

            return (null, null);

        }
    }
}
