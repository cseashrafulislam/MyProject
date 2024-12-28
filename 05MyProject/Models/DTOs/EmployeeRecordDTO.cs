using MyProject.Domain;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.DTOs
{
    public record EmployeeRecordDTO(
        int Id,
        [Required(ErrorMessage = "Name is required.")] string Name
        , [Required, EmailAddress] string Email
        , [Required, MinLength(11)] string PhoneNumber
        , [Required(ErrorMessage = "Department is required.")] int DepartmentId
        , List<Department> Departments
        , string Position
        , DateTime JoiningDate
        , decimal? Salary
        , string Gender
        , bool Deleted
        ,bool IsActive);
}
