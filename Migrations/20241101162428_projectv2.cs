using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DNDServer.Migrations
{
    /// <inheritdoc />
    public partial class projectv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImgProject_Project_ProjectId",
                table: "ImgProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImgProject",
                table: "ImgProject");

            migrationBuilder.RenameTable(
                name: "ImgProject",
                newName: "ImgProjects");

            migrationBuilder.RenameIndex(
                name: "IX_ImgProject_ProjectId",
                table: "ImgProjects",
                newName: "IX_ImgProjects_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImgProjects",
                table: "ImgProjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImgProjects_Project_ProjectId",
                table: "ImgProjects",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImgProjects_Project_ProjectId",
                table: "ImgProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImgProjects",
                table: "ImgProjects");

            migrationBuilder.RenameTable(
                name: "ImgProjects",
                newName: "ImgProject");

            migrationBuilder.RenameIndex(
                name: "IX_ImgProjects_ProjectId",
                table: "ImgProject",
                newName: "IX_ImgProject_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImgProject",
                table: "ImgProject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImgProject_Project_ProjectId",
                table: "ImgProject",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
