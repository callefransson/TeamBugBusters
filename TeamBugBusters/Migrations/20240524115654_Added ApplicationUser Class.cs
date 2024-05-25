using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamBugBusters.Migrations
{
    /// <inheritdoc />
    public partial class AddedApplicationUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Customers_FkCustomerId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_FkCustomerId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "FkCustomerId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "FkUserId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_UserId",
                table: "CartItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_UserId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "FkUserId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "FkCustomerId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_FkCustomerId",
                table: "CartItems",
                column: "FkCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Customers_FkCustomerId",
                table: "CartItems",
                column: "FkCustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }
    }
}
