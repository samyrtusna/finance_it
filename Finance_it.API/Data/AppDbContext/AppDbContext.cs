using Finance_it.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Data.AppDbContext 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Income", Type = FinancialType.Income },
                new Category { Id = 2, Name = "Expense", Type = FinancialType.Expense },
                new Category { Id = 3, Name = "Salary", Type = FinancialType.Income, ParentCategoryId = 1 },
                new Category { Id = 4, Name = "Debt", Type = FinancialType.Income, ParentCategoryId = 1 },
                new Category { Id = 5, Name = "Interest", Type = FinancialType.Income, ParentCategoryId = 1 },
                new Category { Id = 6, Name = "Pension", Type = FinancialType.Income, ParentCategoryId = 1 },

                new Category { Id = 7, Name = "Housing", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 8, Name = "Home Rent", Type = FinancialType.Expense, ParentCategoryId = 7, ExpenseType = ExpenseType.Fixed },
                new Category { Id = 9, Name = "Mortgage", Type = FinancialType.Expense, ParentCategoryId = 7, ExpenseType = ExpenseType.Fixed}, 
                new Category { Id = 10, Name = "Utilities", Type = FinancialType.Expense, ParentCategoryId = 7, ExpenseType = ExpenseType.Variable },
                new Category { Id = 11, Name = "Internet&Telephone", Type = FinancialType.Expense, ParentCategoryId = 7, ExpenseType = ExpenseType.Variable},
                new Category { Id = 12, Name = "Home Insurance", Type = FinancialType.Expense, ParentCategoryId = 7, ExpenseType = ExpenseType.Variable },
                new Category { Id = 13, Name = "Home Maintenance", Type = FinancialType.Expense, ParentCategoryId = 7, ExpenseType = ExpenseType.Variable },

                new Category { Id = 14, Name = "Transport", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 15, Name = "Car Loan", Type = FinancialType.Expense, ParentCategoryId = 14, ExpenseType = ExpenseType.Fixed },
                new Category { Id = 16, Name = "Fuel", Type = FinancialType.Expense, ParentCategoryId = 14, ExpenseType = ExpenseType.Variable },
                new Category { Id = 17, Name = "Public Transport", Type = FinancialType.Expense, ParentCategoryId = 14, ExpenseType = ExpenseType.Variable },
                new Category { Id = 18, Name = "Car Insurance", Type = FinancialType.Expense, ParentCategoryId = 14, ExpenseType = ExpenseType.Variable },
                new Category { Id = 19, Name = "Car Maintenance", Type = FinancialType.Expense, ParentCategoryId = 14, ExpenseType = ExpenseType.Variable },
                new Category { Id = 20, Name = "Parking", Type = FinancialType.Expense, ParentCategoryId = 14, ExpenseType = ExpenseType.Variable },
                new Category { Id = 21, Name = "Food", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 22, Name = "Groceries", Type = FinancialType.Expense, ParentCategoryId = 21, ExpenseType = ExpenseType.Variable },
                new Category { Id = 23, Name = "Restaurant&Cafés", Type = FinancialType.Expense, ParentCategoryId = 21, ExpenseType = ExpenseType.Variable },
                new Category { Id = 24, Name = "Healthcare", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 25, Name = "Health Insurance", Type = FinancialType.Expense, ParentCategoryId = 24, ExpenseType = ExpenseType.Fixed },
                new Category { Id = 26, Name = "Medical Bills", Type = FinancialType.Expense, ParentCategoryId = 24, ExpenseType = ExpenseType.Variable },
                new Category { Id = 27, Name = "Medications", Type = FinancialType.Expense, ParentCategoryId = 24, ExpenseType = ExpenseType.Variable },
                new Category { Id = 28, Name = "Education", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 29, Name = "Tuition Fees", Type = FinancialType.Expense, ParentCategoryId = 28, ExpenseType = ExpenseType.Fixed },
                new Category { Id = 30, Name = "Books&Supplies", Type = FinancialType.Expense, ParentCategoryId = 28, ExpenseType = ExpenseType.Variable },
                new Category { Id = 31, Name = "Courses&Trainings", Type = FinancialType.Expense, ParentCategoryId = 28, ExpenseType = ExpenseType.Variable },
                new Category { Id = 32, Name = "Entertainment", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 33, Name = "Subscriptions", Type = FinancialType.Expense, ParentCategoryId = 31, ExpenseType = ExpenseType.Variable },
                new Category { Id = 34, Name = "Cinema&Events", Type = FinancialType.Expense, ParentCategoryId = 31, ExpenseType = ExpenseType.Variable },
                new Category { Id = 35, Name = "Hobbies", Type = FinancialType.Expense, ParentCategoryId = 31, ExpenseType = ExpenseType.Variable },
                new Category { Id = 36, Name = "Travel&Vacation", Type = FinancialType.Expense, ParentCategoryId = 31, ExpenseType = ExpenseType.Variable },
                new Category { Id = 37, Name = "Insurance", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 38, Name = "Life Insurance", Type = FinancialType.Expense, ParentCategoryId = 34, ExpenseType = ExpenseType.Fixed },
                new Category { Id = 39, Name = "Other Insurance", Type = FinancialType.Expense, ParentCategoryId = 34, ExpenseType = ExpenseType.Variable },
                new Category { Id = 40, Name = "Family&Children", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 41, Name = "Childcare", Type = FinancialType.Expense, ParentCategoryId = 37, ExpenseType = ExpenseType.Variable },
                new Category { Id = 42, Name = "School Fees", Type = FinancialType.Expense, ParentCategoryId = 37, ExpenseType = ExpenseType.Fixed },
                new Category { Id = 43, Name = "Activities", Type = FinancialType.Expense, ParentCategoryId = 37, ExpenseType = ExpenseType.Variable },
                new Category { Id = 44, Name = "Clothes", Type = FinancialType.Expense, ParentCategoryId = 37, ExpenseType = ExpenseType.Variable },
                new Category { Id = 45, Name = "Other/Miscellaneous", Type = FinancialType.Expense, ParentCategoryId = 2, ExpenseType = ExpenseType.Mixed },
                new Category { Id = 46, Name = "Taxes&Fees", Type = FinancialType.Expense, ParentCategoryId = 42, ExpenseType = ExpenseType.Fixed },
                new Category { Id = 47, Name = "Unexpected Expenses", Type = FinancialType.Expense, ParentCategoryId = 42, ExpenseType = ExpenseType.Variable }
                );
        }


        public DbSet<User> Users { get; set; }
        public DbSet<FinancialEntry> FinancialEntries { get; set; }
        public DbSet<FinancialScore> FinancialScores { get; set; }
        public DbSet<ScoreDetail> ScoreDetails { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WeeklyAgregate> WeeklyAgregates { get; set; }
        public DbSet<MonthlyAgregate> MonthlyAgregates { get; set; }
        public DbSet<YearlyAgregate> YearlyAgregates { get; set; }
        }
}
