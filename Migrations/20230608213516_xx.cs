using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class xx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "HoursBudget",
                table: "ProjectPhases",
                newName: "HrBudget");

            migrationBuilder.RenameColumn(
                name: "TotalHoursInProject",
                table: "EmployeeProjects",
                newName: "ProjectPhaseId");

            migrationBuilder.AddColumn<string>(
                name: "Milestone",
                table: "ProjectPhases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OverTime",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryPerHour",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "HoursSpent",
                table: "EmployeeProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectPhaseId",
                table: "EmployeeProjects",
                column: "ProjectPhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_ProjectPhases_ProjectPhaseId",
                table: "EmployeeProjects",
                column: "ProjectPhaseId",
                principalTable: "ProjectPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_ProjectPhases_ProjectPhaseId",
                table: "EmployeeProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProjects_ProjectPhaseId",
                table: "EmployeeProjects");

            migrationBuilder.DropColumn(
                name: "Milestone",
                table: "ProjectPhases");

            migrationBuilder.DropColumn(
                name: "OverTime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalaryPerHour",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HoursSpent",
                table: "EmployeeProjects");

            migrationBuilder.RenameColumn(
                name: "HrBudget",
                table: "ProjectPhases",
                newName: "HoursBudget");

            migrationBuilder.RenameColumn(
                name: "ProjectPhaseId",
                table: "EmployeeProjects",
                newName: "TotalHoursInProject");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
