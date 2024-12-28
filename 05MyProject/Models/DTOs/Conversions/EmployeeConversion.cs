using MyProject.Domain;

namespace MyProject.Models.DTOs.Conversions
{
    public static class EmployeeConversion
    {
        public static Employee ToEntity(EmployeeDTO employeeDTO) => new Employee()
        {
            Id = employeeDTO.Id,
            Name = employeeDTO.Name,
            PhoneNumber = employeeDTO.PhoneNumber,
            Email = employeeDTO.Email,
            DepartmentId = employeeDTO.DepartmentId,
            Deleted = employeeDTO.Deleted,
            JoiningDate = employeeDTO.JoiningDate,
            Position = employeeDTO.Position,
            Salary = employeeDTO.Salary,
            Gender = employeeDTO.Gender,
            IsActive = employeeDTO.IsActive

        };

        public static (EmployeeDTO?, IEnumerable<EmployeeDTO>?) FromEntity(Employee? employee, IEnumerable<Employee>? employees)
        {
            //return single employee
            if(employee is not  null || employees is  null)
            {
                var singleEmployee = new EmployeeDTO(employee!.Id,employee!.Name, employee.Email, employee.PhoneNumber, employee.DepartmentId, employee.Position, employee.JoiningDate, employee.Salary, employee.Gender, employee.Deleted, employee.IsActive);
                return (singleEmployee, null);
            }
            //return list
            if(employees is not null || employee is null)
            {
                var employeeList = employees!.Select(p => new EmployeeDTO(employee!.Id, employee!.Name, employee.Email, employee.PhoneNumber, employee.DepartmentId, employee.Position, employee.JoiningDate, employee.Salary, employee.Gender, employee.Deleted, employee.IsActive)).ToList();
                return (null, employeeList);
            }

            return (null, null);
            
        }
        
    }
}
