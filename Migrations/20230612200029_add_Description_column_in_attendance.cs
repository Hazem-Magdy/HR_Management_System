﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class add_Description_column_in_attendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Attendances");
        }
    }
}
