using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class BrandSupportRedefinitionSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "brandsupportitems");

            migrationBuilder.DropTable(
                name: "brandsupportpackages");

            migrationBuilder.CreateTable(
                name: "brandsupportoffers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    brandid = table.Column<Guid>(type: "uuid", nullable: false),
                    featureimageid = table.Column<Guid>(type: "uuid", nullable: true),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brandsupportoffers", x => x.id);
                    table.ForeignKey(
                        name: "fk_brandsupportoffers_brands_brandid",
                        column: x => x.brandid,
                        principalTable: "brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_brandsupportoffers_images_featureimageid",
                        column: x => x.featureimageid,
                        principalSchema: "guamaframework_media",
                        principalTable: "images",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_brandsupportoffers_brandid",
                table: "brandsupportoffers",
                column: "brandid");

            migrationBuilder.CreateIndex(
                name: "ix_brandsupportoffers_featureimageid",
                table: "brandsupportoffers",
                column: "featureimageid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "brandsupportoffers");

            migrationBuilder.CreateTable(
                name: "brandsupportpackages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    brandid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brandsupportpackages", x => x.id);
                    table.ForeignKey(
                        name: "fk_brandsupportpackages_brands_brandid",
                        column: x => x.brandid,
                        principalTable: "brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "brandsupportitems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    featureimageid = table.Column<Guid>(type: "uuid", nullable: false),
                    packageid = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brandsupportitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_brandsupportitems_brandsupportpackages_packageid",
                        column: x => x.packageid,
                        principalTable: "brandsupportpackages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_brandsupportitems_images_featureimageid",
                        column: x => x.featureimageid,
                        principalSchema: "guamaframework_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_brandsupportitems_featureimageid",
                table: "brandsupportitems",
                column: "featureimageid");

            migrationBuilder.CreateIndex(
                name: "ix_brandsupportitems_packageid",
                table: "brandsupportitems",
                column: "packageid");

            migrationBuilder.CreateIndex(
                name: "ix_brandsupportpackages_brandid",
                table: "brandsupportpackages",
                column: "brandid");
        }
    }
}
