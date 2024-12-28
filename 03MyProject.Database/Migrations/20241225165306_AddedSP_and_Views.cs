using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedSP_and_Views : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "EmployeeQuery");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "EmployeeQuery",
            //    columns: table => new
            //    {
            //        DepartmentId = table.Column<int>(type: "int", nullable: false),
            //        DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Id = table.Column<int>(type: "int", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ReviewScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }
    }
}
