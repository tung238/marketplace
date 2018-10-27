using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TNMarketplace.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 250, nullable: true),
                    LastName = table.Column<string>(maxLength: 250, nullable: true),
                    Mobile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Parent = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Ordering = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContentPages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Slug = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Html = table.Column<string>(nullable: true),
                    Template = table.Column<string>(maxLength: 200, nullable: true),
                    Ordering = table.Column<int>(nullable: false),
                    Keywords = table.Column<string>(maxLength: 200, nullable: true),
                    UserID = table.Column<string>(maxLength: 128, nullable: true),
                    Published = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Slug = table.Column<string>(maxLength: 100, nullable: false),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    Body = table.Column<string>(nullable: false),
                    SendCopy = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ListingTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ButtonLabel = table.Column<string>(maxLength: 20, nullable: false),
                    PriceEnabled = table.Column<bool>(nullable: false),
                    PriceUnitLabel = table.Column<string>(maxLength: 20, nullable: true),
                    OrderTypeID = table.Column<int>(nullable: false),
                    OrderTypeLabel = table.Column<string>(maxLength: 20, nullable: true),
                    PaymentEnabled = table.Column<bool>(nullable: false),
                    ShippingEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MetaFields",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Placeholder = table.Column<string>(maxLength: 255, nullable: true),
                    ControlTypeID = table.Column<int>(nullable: false),
                    Options = table.Column<string>(nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Searchable = table.Column<bool>(nullable: false),
                    Ordering = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaFields", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    ClientId = table.Column<string>(nullable: false),
                    ClientSecret = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    ConsentType = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Permissions = table.Column<string>(nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    RedirectUris = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Properties = table.Column<string>(nullable: true),
                    Resources = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MimeType = table.Column<string>(maxLength: 40, nullable: false),
                    SeoFilename = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Slogan = table.Column<string>(maxLength: 255, nullable: false),
                    SearchPlaceHolder = table.Column<string>(maxLength: 255, nullable: false),
                    EmailContact = table.Column<string>(maxLength: 255, nullable: false),
                    Version = table.Column<string>(maxLength: 10, nullable: false),
                    Currency = table.Column<string>(fixedLength: true, maxLength: 3, nullable: false),
                    TransactionFeePercent = table.Column<double>(nullable: false),
                    TransactionMinimumSize = table.Column<double>(nullable: false),
                    TransactionMinimumFee = table.Column<double>(nullable: false),
                    SmtpHost = table.Column<string>(maxLength: 100, nullable: true),
                    SmtpPort = table.Column<int>(nullable: true),
                    SmtpUserName = table.Column<string>(maxLength: 100, nullable: true),
                    SmtpPassword = table.Column<string>(maxLength: 100, nullable: true),
                    SmtpSSL = table.Column<bool>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    EmailDisplayName = table.Column<string>(maxLength: 100, nullable: true),
                    AgreementRequired = table.Column<bool>(nullable: false),
                    AgreementLabel = table.Column<string>(maxLength: 100, nullable: true),
                    AgreementText = table.Column<string>(nullable: true),
                    SignupText = table.Column<string>(nullable: true),
                    EmailConfirmedRequired = table.Column<bool>(nullable: false),
                    Theme = table.Column<string>(maxLength: 250, nullable: false),
                    DateFormat = table.Column<string>(maxLength: 10, nullable: false),
                    TimeFormat = table.Column<string>(maxLength: 10, nullable: false),
                    ListingReviewEnabled = table.Column<bool>(nullable: false),
                    ListingReviewMaxPerDay = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentType = table.Column<string>(nullable: true),
                    Content = table.Column<byte[]>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPhotos_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryStats",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryID = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryStats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoryStats_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    CultureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryListingTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryID = table.Column<int>(nullable: false),
                    ListingTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryListingTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoryListingTypes_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryListingTypes_ListingTypes_ListingTypeID",
                        column: x => x.ListingTypeID,
                        principalTable: "ListingTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    ListingTypeID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(maxLength: 128, nullable: false),
                    Price = table.Column<double>(nullable: true),
                    Currency = table.Column<string>(fixedLength: true, maxLength: 3, nullable: true),
                    ContactName = table.Column<string>(maxLength: 200, nullable: false),
                    ContactEmail = table.Column<string>(maxLength: 200, nullable: false),
                    ContactPhone = table.Column<string>(maxLength: 50, nullable: true),
                    ShowPhone = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    ShowEmail = table.Column<bool>(nullable: false),
                    Premium = table.Column<bool>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    IP = table.Column<string>(maxLength: 50, nullable: false),
                    Location = table.Column<string>(maxLength: 250, nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Listings_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listings_ListingTypes_ListingTypeID",
                        column: x => x.ListingTypeID,
                        principalTable: "ListingTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listings_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetaCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryID = table.Column<int>(nullable: false),
                    FieldID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MetaCategories_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetaCategories_MetaFields_FieldID",
                        column: x => x.FieldID,
                        principalTable: "MetaFields",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Properties = table.Column<string>(nullable: true),
                    Scopes = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SettingDictionary",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Value = table.Column<string>(maxLength: 200, nullable: true),
                    SettingID = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingDictionary", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SettingDictionary_Settings_SettingID",
                        column: x => x.SettingID,
                        principalTable: "Settings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListingMeta",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ListingID = table.Column<int>(nullable: false),
                    FieldID = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingMeta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListingMeta_MetaFields_FieldID",
                        column: x => x.FieldID,
                        principalTable: "MetaFields",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingMeta_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListingPictures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ListingID = table.Column<int>(nullable: false),
                    PictureID = table.Column<int>(nullable: false),
                    Ordering = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingPictures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListingPictures_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingPictures_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListingStats",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountView = table.Column<int>(nullable: false),
                    CountSpam = table.Column<int>(nullable: false),
                    CountRepeated = table.Column<int>(nullable: false),
                    ListingID = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingStats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListingStats_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageThread",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(maxLength: 200, nullable: true),
                    ListingID = table.Column<int>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageThread", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MessageThread_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    ListingID = table.Column<int>(nullable: false),
                    ListingTypeID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    Currency = table.Column<string>(fixedLength: true, maxLength: 3, nullable: true),
                    ApplicationFee = table.Column<double>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    UserProvider = table.Column<string>(maxLength: 128, nullable: false),
                    UserReceiver = table.Column<string>(maxLength: 128, nullable: false),
                    PaymentPlugin = table.Column<string>(maxLength: 250, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserProvider",
                        column: x => x.UserProvider,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserReceiver",
                        column: x => x.UserReceiver,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(nullable: true),
                    AuthorizationId = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: true),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    ReferenceId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageThreadID = table.Column<int>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    UserFrom = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Message_MessageThread_MessageThreadID",
                        column: x => x.MessageThreadID,
                        principalTable: "MessageThread",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_UserFrom",
                        column: x => x.UserFrom,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageParticipant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageThreadID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageParticipant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MessageParticipant_MessageThread_MessageThreadID",
                        column: x => x.MessageThreadID,
                        principalTable: "MessageThread",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageParticipant_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListingReviews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Rating = table.Column<double>(nullable: false),
                    ListingID = table.Column<int>(nullable: true),
                    OrderID = table.Column<int>(nullable: true),
                    UserFrom = table.Column<string>(maxLength: 128, nullable: false),
                    UserTo = table.Column<string>(maxLength: 128, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Spam = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingReviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListingReviews_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingReviews_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListingReviews_AspNetUsers_UserFrom",
                        column: x => x.UserFrom,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListingReviews_AspNetUsers_UserTo",
                        column: x => x.UserTo,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageReadState",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(maxLength: 128, nullable: false),
                    ReadDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReadState", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MessageReadState_Message_MessageID",
                        column: x => x.MessageID,
                        principalTable: "Message",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReadState_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPhotos_ApplicationUserId",
                table: "ApplicationUserPhotos",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryListingTypes_CategoryID",
                table: "CategoryListingTypes",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryListingTypes_ListingTypeID",
                table: "CategoryListingTypes",
                column: "ListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryStats_CategoryID",
                table: "CategoryStats",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingMeta_FieldID",
                table: "ListingMeta",
                column: "FieldID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingMeta_ListingID",
                table: "ListingMeta",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingPictures_ListingID",
                table: "ListingPictures",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingPictures_PictureID",
                table: "ListingPictures",
                column: "PictureID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingReviews_ListingID",
                table: "ListingReviews",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingReviews_OrderID",
                table: "ListingReviews",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingReviews_UserFrom",
                table: "ListingReviews",
                column: "UserFrom");

            migrationBuilder.CreateIndex(
                name: "IX_ListingReviews_UserTo",
                table: "ListingReviews",
                column: "UserTo");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CategoryID",
                table: "Listings",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ListingTypeID",
                table: "Listings",
                column: "ListingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_UserID",
                table: "Listings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingStats_ListingID",
                table: "ListingStats",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_MessageThreadID",
                table: "Message",
                column: "MessageThreadID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserFrom",
                table: "Message",
                column: "UserFrom");

            migrationBuilder.CreateIndex(
                name: "IX_MessageParticipant_MessageThreadID",
                table: "MessageParticipant",
                column: "MessageThreadID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageParticipant_UserID",
                table: "MessageParticipant",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadState_MessageID",
                table: "MessageReadState",
                column: "MessageID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadState_UserID",
                table: "MessageReadState",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThread_ListingID",
                table: "MessageThread",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_MetaCategories_CategoryID",
                table: "MetaCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MetaCategories_FieldID",
                table: "MetaCategories",
                column: "FieldID");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                table: "OpenIddictTokens",
                column: "ReferenceId",
                unique: true,
                filter: "[ReferenceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ListingID",
                table: "Orders",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserProvider",
                table: "Orders",
                column: "UserProvider");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserReceiver",
                table: "Orders",
                column: "UserReceiver");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CultureId",
                table: "Resources",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingDictionary_SettingID",
                table: "SettingDictionary",
                column: "SettingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserPhotos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CategoryListingTypes");

            migrationBuilder.DropTable(
                name: "CategoryStats");

            migrationBuilder.DropTable(
                name: "ContentPages");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "ListingMeta");

            migrationBuilder.DropTable(
                name: "ListingPictures");

            migrationBuilder.DropTable(
                name: "ListingReviews");

            migrationBuilder.DropTable(
                name: "ListingStats");

            migrationBuilder.DropTable(
                name: "MessageParticipant");

            migrationBuilder.DropTable(
                name: "MessageReadState");

            migrationBuilder.DropTable(
                name: "MetaCategories");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "SettingDictionary");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "MetaFields");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "MessageThread");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ListingTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
