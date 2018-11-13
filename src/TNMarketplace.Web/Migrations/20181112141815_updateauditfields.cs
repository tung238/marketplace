using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TNMarketplace.Web.Migrations
{
    public partial class updateauditfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Settings",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Settings",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "SettingDictionary",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "SettingDictionary",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Orders",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Orders",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "MessageThread",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "MessageThread",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "MessageReadState",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Message",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Message",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "ListingStats",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "ListingStats",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Listings",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Listings",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "ListingReviews",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "EmailTemplates",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "EmailTemplates",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "ContentPages",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "ContentPages",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SettingDictionary",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SettingDictionary",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Regions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Regions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pictures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pictures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MetaFields",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MetaFields",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MetaFields",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MetaFields",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MetaCategories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MetaCategories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MetaCategories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MetaCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MessageThread",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MessageThread",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MessageReadState",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MessageReadState",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MessageReadState",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MessageParticipant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MessageParticipant",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MessageParticipant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MessageParticipant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Message",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Message",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ListingTypes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ListingTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ListingTypes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ListingTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ListingStats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ListingStats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ListingReviews",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ListingReviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ListingReviews",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ListingPictures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ListingPictures",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ListingPictures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ListingPictures",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ListingMeta",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ListingMeta",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ListingMeta",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ListingMeta",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EmailTemplates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "EmailTemplates",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cultures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Cultures",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cultures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Cultures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ContentPages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ContentPages",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CategoryStats",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CategoryStats",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CategoryStats",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CategoryStats",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Areas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Areas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Areas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Areas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationUserPhotos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ApplicationUserPhotos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ApplicationUserPhotos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ApplicationUserPhotos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SettingDictionary");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SettingDictionary");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MetaFields");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MetaFields");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MetaFields");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MetaFields");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MetaCategories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MetaCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MetaCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MetaCategories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MessageReadState");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MessageReadState");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MessageReadState");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MessageParticipant");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MessageParticipant");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MessageParticipant");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MessageParticipant");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ListingTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ListingTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ListingTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ListingTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ListingStats");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ListingStats");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ListingReviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ListingReviews");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ListingReviews");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ListingPictures");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ListingPictures");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ListingPictures");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ListingPictures");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ListingMeta");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ListingMeta");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ListingMeta");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ListingMeta");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cultures");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cultures");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cultures");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Cultures");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContentPages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ContentPages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CategoryStats");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CategoryStats");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CategoryStats");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CategoryStats");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApplicationUserPhotos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ApplicationUserPhotos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ApplicationUserPhotos");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ApplicationUserPhotos");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Settings",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Settings",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "SettingDictionary",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "SettingDictionary",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Orders",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Orders",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "MessageThread",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "MessageThread",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "MessageReadState",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Message",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Message",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ListingStats",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ListingStats",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Listings",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Listings",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ListingReviews",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "EmailTemplates",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EmailTemplates",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ContentPages",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ContentPages",
                newName: "Created");
        }
    }
}
