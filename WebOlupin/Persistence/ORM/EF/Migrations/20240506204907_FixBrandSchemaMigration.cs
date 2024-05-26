using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixBrandSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_brands_identityid",
                table: "brands");

            migrationBuilder.CreateIndex(
                name: "ix_brands_identityid",
                table: "brands",
                column: "identityid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_brands_identityid",
                table: "brands");

            migrationBuilder.CreateIndex(
                name: "ix_brands_identityid",
                table: "brands",
                column: "identityid");
        }
    }
}
