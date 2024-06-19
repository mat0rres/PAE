using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class ExtendProductForContactRecordSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "colorid",
                table: "productforcontactrecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "productforcontactrecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_productforcontactrecords_colorid",
                table: "productforcontactrecords",
                column: "colorid");

            migrationBuilder.AddForeignKey(
                name: "fk_productforcontactrecords_productcolors_colorid",
                table: "productforcontactrecords",
                column: "colorid",
                principalTable: "productcolors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_productforcontactrecords_productcolors_colorid",
                table: "productforcontactrecords");

            migrationBuilder.DropIndex(
                name: "ix_productforcontactrecords_colorid",
                table: "productforcontactrecords");

            migrationBuilder.DropColumn(
                name: "colorid",
                table: "productforcontactrecords");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "productforcontactrecords");
        }
    }
}
