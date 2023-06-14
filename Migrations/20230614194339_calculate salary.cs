using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class calculatesalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OvertimeRate",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RegularHoursPerDay",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "WorkingDaysPerWeek",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OvertimeRate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RegularHoursPerDay",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WorkingDaysPerWeek",
                table: "Employees");
        }
    }
}
