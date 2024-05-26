using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPostSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_images_featureimageid",
                table: "posts");

            migrationBuilder.AlterColumn<Guid>(
                name: "featureimageid",
                table: "posts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<bool>(
                name: "enabled",
                table: "posts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_posts_images_featureimageid",
                table: "posts",
                column: "featureimageid",
                principalSchema: "guamaframework_media",
                principalTable: "images",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_images_featureimageid",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "enabled",
                table: "posts");

            migrationBuilder.AlterColumn<Guid>(
                name: "featureimageid",
                table: "posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_posts_images_featureimageid",
                table: "posts",
                column: "featureimageid",
                principalSchema: "guamaframework_media",
                principalTable: "images",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
