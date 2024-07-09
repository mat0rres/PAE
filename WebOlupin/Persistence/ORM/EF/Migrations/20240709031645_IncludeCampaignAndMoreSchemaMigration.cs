using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class IncludeCampaignAndMoreSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "campaigns",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    url = table.Column<string>(type: "text", nullable: true),
                    imageid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_campaigns", x => x.id);
                    table.ForeignKey(
                        name: "fk_campaigns_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "guamaframework_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_campaigns_imageid",
                table: "campaigns",
                column: "imageid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campaigns");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "products");
        }
    }
}
