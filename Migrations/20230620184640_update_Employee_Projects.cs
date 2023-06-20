using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class update_Employee_Projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeeProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeProjects");
        }
    }
}
