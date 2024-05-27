using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamBugBusters.Migrations
{
    /// <inheritdoc />
    public partial class ChangedmoreClassesAndRemovedUnnecessaryCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDiscount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CartItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductDiscount",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "CartItems",
                type: "int",
                nullable: true);
        }
    }
}
