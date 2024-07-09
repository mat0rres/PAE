using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class PropagatePriorityAnMoreSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "customattributes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "brandsupportoffers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "brands",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "brandpromotions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "enabled",
                table: "brandpromotions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "brandpromotions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priority",
                table: "customattributes");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "brandsupportoffers");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "enabled",
                table: "brandpromotions");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "brandpromotions");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "brandpromotions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
