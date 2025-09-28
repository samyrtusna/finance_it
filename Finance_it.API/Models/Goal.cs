using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } 
        [Required(ErrorMessage ="Title is required.")]
        public string Title { get; set; }= null!;
        [Required(ErrorMessage ="Target Amount is required.")]
        public decimal TargetAmount { get; set; }
        [Required(ErrorMessage ="Current Amount is required.")]
        public decimal CurrentAmount { get; set; }
        [Required(ErrorMessage ="Start date is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage ="Due date is required.")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage ="Goal Status is required.")]
        [EnumDataType(typeof(GoalStatus), ErrorMessage = "Invalid Goal Status.")]
        public GoalStatus Status { get; set; }
    }
}
