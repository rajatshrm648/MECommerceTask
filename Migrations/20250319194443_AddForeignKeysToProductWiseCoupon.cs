using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MECommerceTask.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysToProductWiseCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductWiseCoupons_ProductId",
                table: "ProductWiseCoupons",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWiseCoupons_Products_ProductId",
                table: "ProductWiseCoupons",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWiseCoupons_Products_ProductId",
                table: "ProductWiseCoupons");

            migrationBuilder.DropIndex(
                name: "IX_ProductWiseCoupons_ProductId",
                table: "ProductWiseCoupons");
        }
    }
}
