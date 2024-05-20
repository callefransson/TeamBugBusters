using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamBugBusters.Migrations
{
    /// <inheritdoc />
    public partial class addedNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_FkCartId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "FkCartId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_FkCartId",
                table: "CartItems",
                column: "FkCartId",
                principalTable: "Carts",
                principalColumn: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_FkCartId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "FkCartId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_FkCartId",
                table: "CartItems",
                column: "FkCartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
