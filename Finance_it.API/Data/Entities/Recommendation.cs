using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Data.Entities
{
    public class Recommendation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage ="Text is required.")]
        public string Text { get; set; } = null!;
        [Required(ErrorMessage ="Recommendation Category is required.")]
        [EnumDataType(typeof(RecommendationCategory), ErrorMessage =" Invalid Recommendation Category")]
        public RecommendationCategory Category { get; set; }
        [Required(ErrorMessage ="Recommendation Status is required.")]
        [EnumDataType(typeof(RecommendationStatus), ErrorMessage ="Invalid Recommendation Status.")]
        public RecommendationStatus Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }= DateTime.UtcNow; 
    }
}
