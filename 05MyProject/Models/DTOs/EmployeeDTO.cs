using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.DTOs
{
    public record EmployeeDTO(
        int Id,
        [Required(ErrorMessage = "Name is required.")] string Name
        ,string? Email
        , [Required, MinLength(11)] string PhoneNumber
        , [Required(ErrorMessage = "Department is required.")] int DepartmentId
        , string Position
        , DateTime JoiningDate
        , decimal? Salary
        , string Gender
        ,bool Deleted
        ,bool IsActive);
}
