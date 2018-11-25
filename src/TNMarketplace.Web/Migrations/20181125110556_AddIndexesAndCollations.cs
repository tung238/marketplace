using Microsoft.EntityFrameworkCore.Migrations;

namespace TNMarketplace.Web.Migrations
{
    public partial class AddIndexesAndCollations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Listings",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Listings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.Sql("ALTER TABLE Listings ALTER COLUMN Title NVARCHAR(450) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL");
            migrationBuilder.Sql("ALTER TABLE Listings ALTER COLUMN Description NVARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL");
            migrationBuilder.Sql("ALTER TABLE Listings ALTER COLUMN Location NVARCHAR(250) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_ID_Slug_Ordering",
                table: "Regions",
                columns: new[] { "ID", "Slug", "Ordering" });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ID_Title_Location_Price_CreatedAt",
                table: "Listings",
                columns: new[] { "ID", "Title", "Location", "Price", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ID_Ordering_Slug",
                table: "Categories",
                columns: new[] { "ID", "Ordering", "Slug" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Regions_ID_Slug_Ordering",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Listings_ID_Title_Location_Price_CreatedAt",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ID_Ordering_Slug",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Listings",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Listings",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
