using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class PageHeaderInclusionSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sectionviews");

            migrationBuilder.CreateTable(
                name: "pageheaders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    imageid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pageheaders", x => x.id);
                    table.ForeignKey(
                        name: "fk_pageheaders_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "guamaframework_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pagevisits",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pageid = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    carrier = table.Column<string>(type: "text", nullable: false),
                    context = table.Column<string>(type: "text", nullable: true),
                    networkidentifier = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pagevisits", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pageheaders_imageid",
                table: "pageheaders",
                column: "imageid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pageheaders");

            migrationBuilder.DropTable(
                name: "pagevisits");

            migrationBuilder.CreateTable(
                name: "sectionviews",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    carrier = table.Column<string>(type: "text", nullable: false),
                    context = table.Column<string>(type: "text", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    networkidentifier = table.Column<string>(type: "text", nullable: true),
                    sectionkind = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sectionviews", x => x.id);
                });
        }
    }
}
