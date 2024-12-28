namespace MyProject.Models.DTOs
{
    public record SearchCriteriaDTO
        (
        string searchName,
        string searchDepartment,
        string searchPosition,
        decimal reviewScore,
        int pageSize=10,
        int pageNumber = 1
        );
}
