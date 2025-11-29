using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restore.Infrastructure.Persistence.Migrations.Store
{
    /// <inheritdoc />
    public partial class UpdateProdExtIdInBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "product_xid",
                table: "basket_items",
                newName: "product_ext_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "product_ext_id",
                table: "basket_items",
                newName: "product_xid");
        }
    }
}
