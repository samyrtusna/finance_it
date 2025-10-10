using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finance_it.API.Migrations
{
    /// <inheritdoc />
    public partial class Mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "FinancialEntries");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "FinancialEntries",
                newName: "CategoryId");

            migrationBuilder.AlterColumn<decimal>(
                name: "CriterionValue",
                table: "ScoreDetails",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetAmount",
                table: "Goals",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAmount",
                table: "Goals",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "FinancialScores",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "FinancialEntries",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "IX_FinancialEntries_CategoryId",
                table: "FinancialEntries",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialEntries_Categories_CategoryId",
                table: "FinancialEntries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialEntries_Categories_CategoryId",
                table: "FinancialEntries");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_FinancialEntries_CategoryId",
                table: "FinancialEntries");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "FinancialEntries",
                newName: "Type");

            migrationBuilder.AlterColumn<decimal>(
                name: "CriterionValue",
                table: "ScoreDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetAmount",
                table: "Goals",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentAmount",
                table: "Goals",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "FinancialScores",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "FinancialEntries",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "FinancialEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
