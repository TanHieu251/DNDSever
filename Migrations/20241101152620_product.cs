using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DNDServer.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImgProduct_Product_ProductId",
                table: "ImgProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_TypeProducts_TypeProductId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_TypeProject_TypeData",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeProject",
                table: "TypeProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImgProduct",
                table: "ImgProduct");

            migrationBuilder.RenameTable(
                name: "TypeProject",
                newName: "TypeProjects");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "ImgProduct",
                newName: "ImgProducts");

            migrationBuilder.RenameIndex(
                name: "IX_Product_TypeProductId",
                table: "Products",
                newName: "IX_Products_TypeProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ImgProduct_ProductId",
                table: "ImgProducts",
                newName: "IX_ImgProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeProjects",
                table: "TypeProjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImgProducts",
                table: "ImgProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImgProducts_Products_ProductId",
                table: "ImgProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_TypeProducts_TypeProductId",
                table: "Products",
                column: "TypeProductId",
                principalTable: "TypeProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_TypeProjects_TypeData",
                table: "Project",
                column: "TypeData",
                principalTable: "TypeProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImgProducts_Products_ProductId",
                table: "ImgProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_TypeProducts_TypeProductId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_TypeProjects_TypeData",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeProjects",
                table: "TypeProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImgProducts",
                table: "ImgProducts");

            migrationBuilder.RenameTable(
                name: "TypeProjects",
                newName: "TypeProject");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ImgProducts",
                newName: "ImgProduct");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TypeProductId",
                table: "Product",
                newName: "IX_Product_TypeProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ImgProducts_ProductId",
                table: "ImgProduct",
                newName: "IX_ImgProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeProject",
                table: "TypeProject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImgProduct",
                table: "ImgProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImgProduct_Product_ProductId",
                table: "ImgProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_TypeProducts_TypeProductId",
                table: "Product",
                column: "TypeProductId",
                principalTable: "TypeProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_TypeProject_TypeData",
                table: "Project",
                column: "TypeData",
                principalTable: "TypeProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
