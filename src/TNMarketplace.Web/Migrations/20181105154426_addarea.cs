using Microsoft.EntityFrameworkCore.Migrations;

namespace TNMarketplace.Web.Migrations
{
    public partial class addarea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Regions");

            migrationBuilder.AddColumn<int>(
                name: "AreaID",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "Listings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Slug = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 200, nullable: true),
                    NameWithType = table.Column<string>(maxLength: 400, nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: false),
                    PathWithType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Areas_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_AreaID",
                table: "Listings",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_RegionId",
                table: "Areas",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Areas_AreaID",
                table: "Listings",
                column: "AreaID",
                principalTable: "Areas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Areas_AreaID",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Listings_AreaID",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "AreaID",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Regions",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
