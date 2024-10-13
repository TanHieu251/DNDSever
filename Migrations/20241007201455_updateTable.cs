using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DNDServer.Migrations
{
    /// <inheritdoc />
    public partial class updateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "TypeProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEnd",
                table: "Project",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateStart",
                table: "Project",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Feature",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feature",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specfication",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeProduct_CategoryID",
                table: "TypeProduct",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeProduct_Category_CategoryID",
                table: "TypeProduct",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeProduct_Category_CategoryID",
                table: "TypeProduct");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_TypeProduct_CategoryID",
                table: "TypeProduct");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "TypeProduct");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Feature",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Feature",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Specfication",
                table: "Product");
        }
    }
}
