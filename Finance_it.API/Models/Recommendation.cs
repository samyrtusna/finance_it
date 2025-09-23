using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class Recommendation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }= new();
        [Required(ErrorMessage ="A Text is required.")]
        public string Text { get; set; } = string.Empty;
        [Required(ErrorMessage ="A Recommendation Category is required.")]
        [EnumDataType(typeof(RecommendationCategory), ErrorMessage =" Invalid Recommendation Category")]
        public RecommendationCategory Category { get; set; }
        [Required(ErrorMessage ="A Recommendation Status is required.")]
        [EnumDataType(typeof(RecommendationStatus), ErrorMessage ="Invalid Recommendation Status.")]
        public RecommendationStatus Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
