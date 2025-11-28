using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restore.Infrastructure.Persistence.Migrations.Store
{
    /// <inheritdoc />
    public partial class AddUniqueIndexVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_vouchers_code",
                table: "vouchers");

            migrationBuilder.CreateIndex(
                name: "ix_vouchers_code",
                table: "vouchers",
                column: "code",
                unique: true,
                filter: "\"is_deleted\" = FALSE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_vouchers_code",
                table: "vouchers");

            migrationBuilder.CreateIndex(
                name: "ix_vouchers_code",
                table: "vouchers",
                column: "code",
                unique: true);
        }
    }
}
