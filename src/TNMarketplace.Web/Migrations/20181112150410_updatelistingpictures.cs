using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TNMarketplace.Web.Migrations
{
    public partial class updatelistingpictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingPictures_Pictures_PictureID",
                table: "ListingPictures");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_ListingPictures_PictureID",
                table: "ListingPictures");

            migrationBuilder.DropColumn(
                name: "PictureID",
                table: "ListingPictures");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ListingPictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "ListingPictures");

            migrationBuilder.AddColumn<int>(
                name: "PictureID",
                table: "ListingPictures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(maxLength: 40, nullable: false),
                    SeoFilename = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingPictures_PictureID",
                table: "ListingPictures",
                column: "PictureID");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingPictures_Pictures_PictureID",
                table: "ListingPictures",
                column: "PictureID",
                principalTable: "Pictures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
