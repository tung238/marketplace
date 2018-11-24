using Microsoft.EntityFrameworkCore.Migrations;

namespace TNMarketplace.Web.Migrations
{
    public partial class changecollation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Listings ALTER COLUMN Title NVARCHAR(500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL");
            migrationBuilder.Sql("ALTER TABLE Listings ALTER COLUMN Description NVARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL");
            migrationBuilder.Sql("ALTER TABLE Listings ALTER COLUMN Location NVARCHAR(250) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
