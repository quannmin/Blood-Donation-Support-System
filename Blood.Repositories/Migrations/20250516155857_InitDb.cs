using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blood.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "BloodCompatibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorBloodTypeId = table.Column<int>(type: "int", nullable: false),
                    RecipientBloodTypeId = table.Column<int>(type: "int", nullable: false),
                    TransfusionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompatibilityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodCompatibilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodCompatibilities_BloodTypes_DonorBloodTypeId",
                        column: x => x.DonorBloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodCompatibilities_BloodTypes_RecipientBloodTypeId",
                        column: x => x.RecipientBloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonorProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BloodTypeId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HealthStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastDonationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextAvailableDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DonationCount = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsEmergencyAvailable = table.Column<bool>(type: "bit", nullable: false),
                    PreferredDonationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonorProfiles_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BloodTypeId = table.Column<int>(type: "int", nullable: false),
                    MedicalCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emergency = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipientProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipientProfiles_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BloodRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    BloodTypeId = table.Column<int>(type: "int", nullable: false),
                    TransfusionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Urgency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FulfilledUnits = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodRequests_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodRequests_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodRequests_RecipientProfiles_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "RecipientProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonationRequestMatching",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    DonorId = table.Column<int>(type: "int", nullable: false),
                    MatchingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationRequestMatching", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationRequestMatching_BloodRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "BloodRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationRequestMatching_DonorProfiles_DonorId",
                        column: x => x.DonorId,
                        principalTable: "DonorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    BloodRequestId = table.Column<int>(type: "int", nullable: false),
                    DonorId = table.Column<int>(type: "int", nullable: false),
                    DonorProfileId = table.Column<int>(type: "int", nullable: false),
                    NotificationMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyNotifications_BloodRequests_BloodRequestId",
                        column: x => x.BloodRequestId,
                        principalTable: "BloodRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmergencyNotifications_DonorProfiles_DonorProfileId",
                        column: x => x.DonorProfileId,
                        principalTable: "DonorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyResponders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    DonorId = table.Column<int>(type: "int", nullable: false),
                    ResponseStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyResponders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyResponders_BloodRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "BloodRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmergencyResponders_DonorProfiles_DonorId",
                        column: x => x.DonorId,
                        principalTable: "DonorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodDonations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    HealthCheckId = table.Column<int>(type: "int", nullable: true),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransfusionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    InventoryAdded = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodDonations_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BloodDonations_BloodRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "BloodRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BloodDonations_DonorProfiles_DonorId",
                        column: x => x.DonorId,
                        principalTable: "DonorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodTypeId = table.Column<int>(type: "int", nullable: false),
                    DonationId = table.Column<int>(type: "int", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransfusionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityML = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StorageLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: true),
                    DiscardedReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tested = table.Column<bool>(type: "bit", nullable: false),
                    TestResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodInventory_BloodDonations_DonationId",
                        column: x => x.DonationId,
                        principalTable: "BloodDonations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodInventory_BloodRequests_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "BloodRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BloodInventory_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonationProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    DonorId = table.Column<int>(type: "int", nullable: true),
                    InventoryId = table.Column<int>(type: "int", nullable: true),
                    ProcessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HealthCheckResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonationCompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessingCompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransfusionCompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationProcesses_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonationProcesses_BloodInventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "BloodInventory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonationProcesses_BloodRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "BloodRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationProcesses_DonorProfiles_DonorId",
                        column: x => x.DonorId,
                        principalTable: "DonorProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BloodProcessings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    DonationId = table.Column<int>(type: "int", nullable: false),
                    ProcessingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SeparatedComponents = table.Column<bool>(type: "bit", nullable: false),
                    TestResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessingStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessedBy = table.Column<int>(type: "int", nullable: false),
                    ProcessorId = table.Column<int>(type: "int", nullable: false),
                    AddedToInventory = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodProcessings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodProcessings_AspNetUsers_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodProcessings_BloodDonations_DonationId",
                        column: x => x.DonationId,
                        principalTable: "BloodDonations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodProcessings_DonationProcesses_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "DonationProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: true),
                    Hemoglobin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pulse = table.Column<int>(type: "int", nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsEligible = table.Column<bool>(type: "bit", nullable: false),
                    ReasonIfIneligible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedBy = table.Column<int>(type: "int", nullable: false),
                    PerformerId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthChecks_AspNetUsers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthChecks_DonationProcesses_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "DonationProcesses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedBy = table.Column<int>(type: "int", nullable: true),
                    PerformerId = table.Column<int>(type: "int", nullable: false),
                    ActionDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessLogs_AspNetUsers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessLogs_DonationProcesses_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "DonationProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_BloodCompatibilities_DonorBloodTypeId",
                table: "BloodCompatibilities",
                column: "DonorBloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodCompatibilities_RecipientBloodTypeId",
                table: "BloodCompatibilities",
                column: "RecipientBloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_DonorId",
                table: "BloodDonations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_HealthCheckId",
                table: "BloodDonations",
                column: "HealthCheckId",
                unique: true,
                filter: "[HealthCheckId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_RequestId",
                table: "BloodDonations",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_StaffId",
                table: "BloodDonations",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodInventory_BloodTypeId",
                table: "BloodInventory",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodInventory_DonationId",
                table: "BloodInventory",
                column: "DonationId",
                unique: true,
                filter: "[DonationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BloodInventory_ReservationId",
                table: "BloodInventory",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodProcessings_DonationId",
                table: "BloodProcessings",
                column: "DonationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodProcessings_ProcessId",
                table: "BloodProcessings",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodProcessings_ProcessorId",
                table: "BloodProcessings",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_BloodTypeId",
                table: "BloodRequests",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_RecipientId",
                table: "BloodRequests",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_StaffId",
                table: "BloodRequests",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationProcesses_DonorId",
                table: "DonationProcesses",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationProcesses_InventoryId",
                table: "DonationProcesses",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationProcesses_RequestId",
                table: "DonationProcesses",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationProcesses_StaffId",
                table: "DonationProcesses",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequestMatching_DonorId",
                table: "DonationRequestMatching",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequestMatching_RequestId",
                table: "DonationRequestMatching",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorProfiles_BloodTypeId",
                table: "DonorProfiles",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorProfiles_UserId",
                table: "DonorProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyNotifications_BloodRequestId",
                table: "EmergencyNotifications",
                column: "BloodRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyNotifications_DonorProfileId",
                table: "EmergencyNotifications",
                column: "DonorProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyResponders_DonorId",
                table: "EmergencyResponders",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyResponders_RequestId",
                table: "EmergencyResponders",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthChecks_PerformerId",
                table: "HealthChecks",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthChecks_ProcessId",
                table: "HealthChecks",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessLogs_PerformerId",
                table: "ProcessLogs",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessLogs_ProcessId",
                table: "ProcessLogs",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientProfiles_BloodTypeId",
                table: "RecipientProfiles",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientProfiles_UserId",
                table: "RecipientProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodDonations_HealthChecks_HealthCheckId",
                table: "BloodDonations",
                column: "HealthCheckId",
                principalTable: "HealthChecks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodDonations_AspNetUsers_StaffId",
                table: "BloodDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodRequests_AspNetUsers_StaffId",
                table: "BloodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationProcesses_AspNetUsers_StaffId",
                table: "DonationProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorProfiles_AspNetUsers_UserId",
                table: "DonorProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthChecks_AspNetUsers_PerformerId",
                table: "HealthChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipientProfiles_AspNetUsers_UserId",
                table: "RecipientProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodInventory_BloodTypes_BloodTypeId",
                table: "BloodInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodRequests_BloodTypes_BloodTypeId",
                table: "BloodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorProfiles_BloodTypes_BloodTypeId",
                table: "DonorProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipientProfiles_BloodTypes_BloodTypeId",
                table: "RecipientProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodDonations_BloodRequests_RequestId",
                table: "BloodDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodInventory_BloodRequests_ReservationId",
                table: "BloodInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationProcesses_BloodRequests_RequestId",
                table: "DonationProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodDonations_DonorProfiles_DonorId",
                table: "BloodDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_DonationProcesses_DonorProfiles_DonorId",
                table: "DonationProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodDonations_HealthChecks_HealthCheckId",
                table: "BloodDonations");

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
                name: "BloodCompatibilities");

            migrationBuilder.DropTable(
                name: "BloodProcessings");

            migrationBuilder.DropTable(
                name: "DonationRequestMatching");

            migrationBuilder.DropTable(
                name: "EmergencyNotifications");

            migrationBuilder.DropTable(
                name: "EmergencyResponders");

            migrationBuilder.DropTable(
                name: "ProcessLogs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BloodTypes");

            migrationBuilder.DropTable(
                name: "BloodRequests");

            migrationBuilder.DropTable(
                name: "RecipientProfiles");

            migrationBuilder.DropTable(
                name: "DonorProfiles");

            migrationBuilder.DropTable(
                name: "HealthChecks");

            migrationBuilder.DropTable(
                name: "DonationProcesses");

            migrationBuilder.DropTable(
                name: "BloodInventory");

            migrationBuilder.DropTable(
                name: "BloodDonations");
        }
    }
}
