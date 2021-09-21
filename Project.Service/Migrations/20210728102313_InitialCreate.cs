using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Service.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Assignment_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Assignment_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                columns: table => new
                {
                    ProjectType_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectType_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.ProjectType_Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Project_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectType_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Project_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Project_Id);
                    table.ForeignKey(
                        name: "FK_Project_ProjectType",
                        column: x => x.ProjectType_Id,
                        principalTable: "ProjectTypes",
                        principalColumn: "ProjectType_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentProject",
                columns: table => new
                {
                    AssignmentsAssignment_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectsProject_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentProject", x => new { x.AssignmentsAssignment_Id, x.ProjectsProject_Id });
                    table.ForeignKey(
                        name: "FK_AssignmentProject_Assignment_AssignmentsAssignment_Id",
                        column: x => x.AssignmentsAssignment_Id,
                        principalTable: "Assignment",
                        principalColumn: "Assignment_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentProject_Projects_ProjectsProject_Id",
                        column: x => x.ProjectsProject_Id,
                        principalTable: "Projects",
                        principalColumn: "Project_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentProject_ProjectsProject_Id",
                table: "AssignmentProject",
                column: "ProjectsProject_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectType_Id",
                table: "Projects",
                column: "ProjectType_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentProject");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectTypes");
        }
    }
}
