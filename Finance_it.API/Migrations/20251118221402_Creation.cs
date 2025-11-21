using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finance_it.API.Migrations
{
    /// <inheritdoc />
    public partial class Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ExpenseType = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyAggregates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AggregateName = table.Column<int>(type: "int", nullable: false),
                    AggregateValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyAggregates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyAggregates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyAggregates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WeekStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeekEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AggregateName = table.Column<int>(type: "int", nullable: false),
                    AggregateValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyAggregates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyAggregates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YearlyAggregates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AggregateName = table.Column<int>(type: "int", nullable: false),
                    AggregateValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearlyAggregates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearlyAggregates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialEntries_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ExpenseType", "Name", "ParentCategoryId", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, null, "Income", null, 1, null },
                    { 2, null, "Expense", null, 2, null },
                    { 3, null, "Salary", 1, 1, null },
                    { 4, null, "Debt", 1, 1, null },
                    { 5, null, "Interest", 1, 1, null },
                    { 6, null, "Pension", 1, 1, null },
                    { 7, 3, "Housing", 2, 2, null },
                    { 14, 3, "Transport", 2, 2, null },
                    { 21, 3, "Food", 2, 2, null },
                    { 24, 3, "Healthcare", 2, 2, null },
                    { 28, 3, "Education", 2, 2, null },
                    { 32, 3, "Entertainment", 2, 2, null },
                    { 37, 3, "Insurance", 2, 2, null },
                    { 40, 3, "Family&Children", 2, 2, null },
                    { 45, 3, "Other/Miscellaneous", 2, 2, null },
                    { 8, 1, "Home Rent", 7, 2, null },
                    { 9, 1, "Mortgage", 7, 2, null },
                    { 10, 2, "Utilities", 7, 2, null },
                    { 11, 2, "Internet&Telephone", 7, 2, null },
                    { 12, 2, "Home Insurance", 7, 2, null },
                    { 13, 2, "Home Maintenance", 7, 2, null },
                    { 15, 1, "Car Loan", 14, 2, null },
                    { 16, 2, "Fuel", 14, 2, null },
                    { 17, 2, "Public Transport", 14, 2, null },
                    { 18, 2, "Car Insurance", 14, 2, null },
                    { 19, 2, "Car Maintenance", 14, 2, null },
                    { 20, 2, "Parking", 14, 2, null },
                    { 22, 2, "Groceries", 21, 2, null },
                    { 23, 2, "Restaurant&Cafés", 21, 2, null },
                    { 25, 1, "Health Insurance", 24, 2, null },
                    { 26, 2, "Medical Bills", 24, 2, null },
                    { 27, 2, "Medications", 24, 2, null },
                    { 29, 1, "Tuition Fees", 28, 2, null },
                    { 30, 2, "Books&Supplies", 28, 2, null },
                    { 31, 2, "Courses&Trainings", 28, 2, null },
                    { 41, 2, "Childcare", 37, 2, null },
                    { 42, 1, "School Fees", 37, 2, null },
                    { 43, 2, "Activities", 37, 2, null },
                    { 44, 2, "Clothes", 37, 2, null },
                    { 33, 2, "Subscriptions", 31, 2, null },
                    { 34, 2, "Cinema&Events", 31, 2, null },
                    { 35, 2, "Hobbies", 31, 2, null },
                    { 36, 2, "Travel&Vacation", 31, 2, null },
                    { 46, 1, "Taxes&Fees", 42, 2, null },
                    { 47, 2, "Unexpected Expenses", 42, 2, null },
                    { 38, 1, "Life Insurance", 34, 2, null },
                    { 39, 2, "Other Insurance", 34, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialEntries_CategoryId",
                table: "FinancialEntries",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialEntries_UserId",
                table: "FinancialEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId",
                table: "Goals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyAggregates_UserId",
                table: "MonthlyAggregates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_UserId",
                table: "Recommendations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_Unique",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyAggregates_UserId",
                table: "WeeklyAggregates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_YearlyAggregates_UserId",
                table: "YearlyAggregates",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialEntries");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "MonthlyAggregates");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "WeeklyAggregates");

            migrationBuilder.DropTable(
                name: "YearlyAggregates");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
