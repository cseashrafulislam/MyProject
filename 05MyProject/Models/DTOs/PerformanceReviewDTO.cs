using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.DTOs
{
    public record PerformanceReviewDTO( 
        int Id,
        [Required(ErrorMessage ="Employee is required.")] int EmployeeId,
        [Required] DateOnly ReviewDate,
        [Required, Range(1,10)] decimal ReviewScore,
        string ReviewNotes);
}
