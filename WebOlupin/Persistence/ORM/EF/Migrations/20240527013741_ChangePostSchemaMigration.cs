using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangePostSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "summary",
                table: "posts");

            migrationBuilder.AddColumn<string>(
                name: "context",
                table: "posts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "context",
                table: "posts");

            migrationBuilder.AddColumn<string>(
                name: "summary",
                table: "posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
