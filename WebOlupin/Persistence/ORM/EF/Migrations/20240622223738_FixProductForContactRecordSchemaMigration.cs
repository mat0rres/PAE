using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixProductForContactRecordSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_productforcontactrecords_productcolors_productcolorid",
                table: "productforcontactrecords");

            migrationBuilder.DropColumn(
                name: "color",
                table: "productforcontactrecords");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "productforcontactrecords");

            migrationBuilder.RenameColumn(
                name: "productcolorid",
                table: "productforcontactrecords",
                newName: "productid");

            migrationBuilder.RenameIndex(
                name: "ix_productforcontactrecords_productcolorid",
                table: "productforcontactrecords",
                newName: "ix_productforcontactrecords_productid");

            migrationBuilder.AddForeignKey(
                name: "fk_productforcontactrecords_products_productid",
                table: "productforcontactrecords",
                column: "productid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_productforcontactrecords_products_productid",
                table: "productforcontactrecords");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "productforcontactrecords",
                newName: "productcolorid");

            migrationBuilder.RenameIndex(
                name: "ix_productforcontactrecords_productid",
                table: "productforcontactrecords",
                newName: "ix_productforcontactrecords_productcolorid");

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "productforcontactrecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "productforcontactrecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_productforcontactrecords_productcolors_productcolorid",
                table: "productforcontactrecords",
                column: "productcolorid",
                principalTable: "productcolors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
