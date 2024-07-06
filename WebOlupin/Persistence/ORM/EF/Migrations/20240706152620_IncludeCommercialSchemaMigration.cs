using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class IncludeCommercialSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "commercials",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullname = table.Column<string>(type: "text", nullable: false),
                    emailaddress = table.Column<string>(type: "text", nullable: false),
                    notifyproductforcontact = table.Column<bool>(type: "boolean", nullable: false),
                    notifycommoncontact = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commercials", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commercials");
        }
    }
}
