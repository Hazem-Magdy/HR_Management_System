using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class remove_employeeProjects_coloumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_ProjectPhases_ProjectPhaseId",
                table: "EmployeeProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProjects_EmployeeId",
                table: "EmployeeProjects");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProjects_ProjectPhaseId",
                table: "EmployeeProjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeProjects");

            migrationBuilder.DropColumn(
                name: "HoursSpent",
                table: "EmployeeProjects");

            migrationBuilder.DropColumn(
                name: "ProjectPhaseId",
                table: "EmployeeProjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects",
                columns: new[] { "EmployeeId", "ProjectId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeeProjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "HoursSpent",
                table: "EmployeeProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectPhaseId",
                table: "EmployeeProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_EmployeeId",
                table: "EmployeeProjects",
                column: "EmployeeId");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
