using MyProject.Domain;

namespace MyProject.Models.DTOs.Conversions
{
    public static class PerformanceReviewConversion
    {
        public static PerformanceReview ToEntity(PerformanceReviewDTO performanceReviewDTO) => new PerformanceReview()
        {
            Id = performanceReviewDTO.Id,
            EmployeeId = performanceReviewDTO.EmployeeId,
            ReviewDate = performanceReviewDTO.ReviewDate,
            ReviewNotes = performanceReviewDTO.ReviewNotes,
            ReviewScore = performanceReviewDTO.ReviewScore
        };

        public static (PerformanceReviewDTO?, IEnumerable<PerformanceReviewDTO>?) FromEntity(PerformanceReview? performanceReview, IEnumerable<PerformanceReview>? performanceReviews)
        {
            //return single
            if (performanceReview is not null || performanceReviews is null)
            {
                var single = new PerformanceReviewDTO(performanceReview!.Id,performanceReview.EmployeeId,performanceReview.ReviewDate,performanceReview.ReviewScore,performanceReview.ReviewNotes);
                return (single, null);
            }
            //return list
            if (performanceReviews is not null || performanceReview is null)
            {
                var pList = performanceReviews!.Select(p => new PerformanceReviewDTO(performanceReview!.Id, performanceReview.EmployeeId, performanceReview.ReviewDate, performanceReview.ReviewScore, performanceReview.ReviewNotes)).ToList();
                return (null, pList);
            }

            return (null, null);

        }
    }
}
