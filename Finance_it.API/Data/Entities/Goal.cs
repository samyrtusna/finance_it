using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_it.API.Data.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } 
        [Required(ErrorMessage ="Title is required.")]
        public string Title { get; set; }= null!;
        [Required(ErrorMessage ="Target Amount is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TargetAmount { get; set; }
        [Required(ErrorMessage ="Current Amount is required.")]
        [Column(TypeName = "decimal(10,2)")]
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
