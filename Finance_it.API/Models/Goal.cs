using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = new();
        [Required(ErrorMessage ="A Title is required.")]
        public string Title { get; set; }= string.Empty;
        [Required(ErrorMessage ="A Target Amount is required.")]
        public decimal TargetAmount { get; set; }
        [Required(ErrorMessage ="A Current Amount is required.")]
        public decimal CurrentAmount { get; set; }
        [Required(ErrorMessage ="A Start date is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage ="A Due date is required.")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage ="A Goal Status is required.")]
        [EnumDataType(typeof(GoalStatus), ErrorMessage = "Invalid Goal Status.")]
        public GoalStatus Status { get; set; }
    }
}
