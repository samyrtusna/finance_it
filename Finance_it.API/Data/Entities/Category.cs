using System.ComponentModel.DataAnnotations;

namespace Finance_it.API.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Category name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 50 characters.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Financial type is required.")]
        [EnumDataType(typeof(FinancialType), ErrorMessage = "Invalid Financial type.")]
        public FinancialType Type { get; set; }
        [EnumDataType(typeof(ExpenseType), ErrorMessage = "Invalid Expense type.")]
        public ExpenseType? ExpenseType { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; } = [];
    }
}
