using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroups", x => x.Id);
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodGroupId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastDonationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_BloodGroups_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BloodCompatibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorBloodGroupId = table.Column<int>(type: "int", nullable: false),
                    RecipientBloodGroupId = table.Column<int>(type: "int", nullable: false),
                    BloodComponent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompatible = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_BloodCompatibilities_BloodGroups_DonorBloodGroupId",
                        column: x => x.DonorBloodGroupId,
                        principalTable: "BloodGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodCompatibilities_BloodGroups_RecipientBloodGroupId",
                        column: x => x.RecipientBloodGroupId,
                        principalTable: "BloodGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BloodUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodGroupId = table.Column<int>(type: "int", nullable: false),
                    BloodComponent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodUnits_BloodGroups_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroups",
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
                name: "DonorAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorAvailabilities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodGroupId = table.Column<int>(type: "int", nullable: false),
                    BloodComponent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedById = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FulfilledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestSource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodUnitId = table.Column<int>(type: "int", nullable: true),
                    QuantityFromStock = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_BloodRequests_AspNetUsers_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodRequests_BloodGroups_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodRequests_BloodUnits_BloodUnitId",
                        column: x => x.BloodUnitId,
                        principalTable: "BloodUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BloodRequestId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donations_BloodRequests_BloodRequestId",
                        column: x => x.BloodRequestId,
                        principalTable: "BloodRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "LastUpdatedBy", "LastUpdatedTime", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1809), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Quản trị viên", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1810), new TimeSpan(0, 7, 0, 0, 0)), "Admin", "ADMIN" },
                    { 2, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1814), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Bác sĩ", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1815), new TimeSpan(0, 7, 0, 0, 0)), "Doctor", "DOCTOR" },
                    { 3, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1816), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Người dùng", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1817), new TimeSpan(0, 7, 0, 0, 0)), "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarUrl", "BloodGroupId", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DateOfBirth", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "FullName", "Gender", "LastDonationDate", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, null, null, "766b830c-159d-4c42-9e3f-0db4ed16ae14", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1839), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, "admin@example.com", true, "Quản trị viên", null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1840), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEPkIS7jo1Npd0Ew+68fjD/qwi8P8DhS/kB2U55PpLTyGR45sRlEb3d0sVwqYI6YhzQ==", null, false, null, new DateTimeOffset(new DateTime(2025, 6, 20, 12, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1912), new TimeSpan(0, 0, 0, 0, 0)), "12923ab2-7d39-4d47-93d8-2a053950bd24", true, false, "admin" },
                    { 2, 0, null, null, null, "19ded8ff-92d9-46dd-8d4d-a72bd142c3b9", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 257, DateTimeKind.Unspecified).AddTicks(172), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, "doctor@example.com", true, "Bác sĩ", null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 257, DateTimeKind.Unspecified).AddTicks(203), new TimeSpan(0, 7, 0, 0, 0)), false, null, "DOCTOR@EXAMPLE.COM", "DOCTOR", "AQAAAAIAAYagAAAAEPun1J2cxN596vs4iA/5/dBtGvXsg05/ovxlufpnbG7FMrs079vr2vuFlVSr8yrhNQ==", null, false, null, new DateTimeOffset(new DateTime(2025, 6, 20, 12, 42, 57, 257, DateTimeKind.Unspecified).AddTicks(257), new TimeSpan(0, 0, 0, 0, 0)), "bdc37f98-9e97-446c-bdac-723a686d0598", true, false, "doctor" },
                    { 3, 0, null, null, null, "f90a5c93-28e5-4fd3-9a28-a29ad2f2cd55", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 313, DateTimeKind.Unspecified).AddTicks(1958), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, "user@example.com", true, "Người dùng", null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 313, DateTimeKind.Unspecified).AddTicks(1998), new TimeSpan(0, 7, 0, 0, 0)), false, null, "USER@EXAMPLE.COM", "USER", "AQAAAAIAAYagAAAAEEDiJnpw5DvKzBrFCA9tPsqsM2kafjlDdi0vZ7tbvBYTmHs3Iy2TOCbu73DZpZ5l6Q==", null, false, null, new DateTimeOffset(new DateTime(2025, 6, 20, 12, 42, 57, 313, DateTimeKind.Unspecified).AddTicks(2173), new TimeSpan(0, 0, 0, 0, 0)), "bda2d726-5ed6-4d4e-a255-50626bffaf9b", true, false, "user" },
                    { 4, 0, null, null, null, "a15762c2-0fe9-4dae-8613-ec8a8e3c5d0f", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2441), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, "john@example.com", true, "John Doe", null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2477), new TimeSpan(0, 7, 0, 0, 0)), false, null, "JOHN@EXAMPLE.COM", "JOHN", "AQAAAAIAAYagAAAAECGLNDWa8AFT50vF8ERF8FezeJ8BrBEctoKlbg5ctUrJLsFwiNL+KsF2Zb/tSi0FdQ==", null, false, null, new DateTimeOffset(new DateTime(2025, 6, 20, 12, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2536), new TimeSpan(0, 0, 0, 0, 0)), "bfe4871c-0b8d-418e-8049-20e9c69c1ba8", true, false, "john" },
                    { 5, 0, null, null, null, "d9bfa333-8f44-4053-97de-d6c79d52b80b", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2551), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, "jane@example.com", true, "Jane Smith", null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2552), new TimeSpan(0, 7, 0, 0, 0)), false, null, "JANE@EXAMPLE.COM", "JANE", "AQAAAAIAAYagAAAAENC+2q0W4Yl5167O49s6YK4EQEVLuot076ytvM2oNbHLXWkVogqbZcc9GSyvhwyRpQ==", null, false, null, new DateTimeOffset(new DateTime(2025, 6, 20, 12, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2650), new TimeSpan(0, 0, 0, 0, 0)), "b8dd8d49-8310-4ef8-b4d2-2159d420bdc1", true, false, "jane" },
                    { 6, 0, null, null, null, "a02327c0-c703-48ab-b70c-d9527a5780eb", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2653), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, "alice@example.com", true, "Alice Nguyen", null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2654), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ALICE@EXAMPLE.COM", "ALICE", "AQAAAAIAAYagAAAAENV+JexyEv5NmNwTqbzgKwn778bw+tU/bInIZCuujsO3SP3+QlX0TNxtzm1rbmoveg==", null, false, null, new DateTimeOffset(new DateTime(2025, 6, 20, 12, 42, 57, 371, DateTimeKind.Unspecified).AddTicks(2663), new TimeSpan(0, 0, 0, 0, 0)), "397d4080-3a68-465b-9fce-181e3dd7a17e", true, false, "alice" }
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Author", "Content", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "ImageUrl", "LastUpdatedBy", "LastUpdatedTime", "Title" },
                values: new object[,]
                {
                    { 1, "Admin", "Hiến máu là một hành động cao cả, giúp cứu sống nhiều người bệnh cần truyền máu.", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7172), new TimeSpan(0, 7, 0, 0, 0)), null, null, "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Fworld%20blood%20donor%20day%20social%20media%20template.png?alt=media&token=bc11e9bd-1eac-415b-8c70-20c17fcd340a", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7224), new TimeSpan(0, 7, 0, 0, 0)), "Tầm quan trọng của hiến máu" },
                    { 2, "Bác sĩ Nguyễn Văn A", "Trước khi đi hiến máu, bạn cần ăn nhẹ, ngủ đủ và không uống rượu bia.", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7228), new TimeSpan(0, 7, 0, 0, 0)), null, null, "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Fworld%20blood%20donor%20day%20social%20media%20template.png?alt=media&token=bc11e9bd-1eac-415b-8c70-20c17fcd340a", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7229), new TimeSpan(0, 7, 0, 0, 0)), "Những điều cần biết khi đi hiến máu" }
                });

            migrationBuilder.InsertData(
                table: "BloodGroups",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1451), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1488), new TimeSpan(0, 7, 0, 0, 0)), "A+" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1493), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1493), new TimeSpan(0, 7, 0, 0, 0)), "A-" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1495), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1495), new TimeSpan(0, 7, 0, 0, 0)), "B+" },
                    { 4, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1496), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1497), new TimeSpan(0, 7, 0, 0, 0)), "B-" },
                    { 5, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1498), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1498), new TimeSpan(0, 7, 0, 0, 0)), "AB+" },
                    { 6, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1499), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1499), new TimeSpan(0, 7, 0, 0, 0)), "AB-" },
                    { 7, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1500), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1501), new TimeSpan(0, 7, 0, 0, 0)), "O+" },
                    { 8, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1502), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1502), new TimeSpan(0, 7, 0, 0, 0)), "O-" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "BloodCompatibilities",
                columns: new[] { "Id", "BloodComponent", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "DonorBloodGroupId", "IsCompatible", "LastUpdatedBy", "LastUpdatedTime", "RecipientBloodGroupId" },
                values: new object[,]
                {
                    { 1, "WholeBlood", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1677), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1678), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 2, "WholeBlood", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1683), new TimeSpan(0, 7, 0, 0, 0)), null, null, 7, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1683), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 3, "WholeBlood", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1685), new TimeSpan(0, 7, 0, 0, 0)), null, null, 1, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1685), new TimeSpan(0, 7, 0, 0, 0)), 5 },
                    { 4, "WholeBlood", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1686), new TimeSpan(0, 7, 0, 0, 0)), null, null, 2, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1687), new TimeSpan(0, 7, 0, 0, 0)), 6 },
                    { 5, "WholeBlood", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1688), new TimeSpan(0, 7, 0, 0, 0)), null, null, 3, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1689), new TimeSpan(0, 7, 0, 0, 0)), 5 },
                    { 6, "WholeBlood", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1690), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1690), new TimeSpan(0, 7, 0, 0, 0)), 8 },
                    { 7, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1691), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1692), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 8, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1693), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1693), new TimeSpan(0, 7, 0, 0, 0)), 2 },
                    { 9, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1694), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1695), new TimeSpan(0, 7, 0, 0, 0)), 3 },
                    { 10, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1696), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1696), new TimeSpan(0, 7, 0, 0, 0)), 4 },
                    { 11, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1697), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1698), new TimeSpan(0, 7, 0, 0, 0)), 5 },
                    { 12, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1699), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1699), new TimeSpan(0, 7, 0, 0, 0)), 6 },
                    { 13, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1700), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1701), new TimeSpan(0, 7, 0, 0, 0)), 7 },
                    { 14, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1702), new TimeSpan(0, 7, 0, 0, 0)), null, null, 8, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1702), new TimeSpan(0, 7, 0, 0, 0)), 8 },
                    { 15, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1703), new TimeSpan(0, 7, 0, 0, 0)), null, null, 7, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1704), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 16, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1746), new TimeSpan(0, 7, 0, 0, 0)), null, null, 7, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1746), new TimeSpan(0, 7, 0, 0, 0)), 3 },
                    { 17, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1747), new TimeSpan(0, 7, 0, 0, 0)), null, null, 7, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1748), new TimeSpan(0, 7, 0, 0, 0)), 5 },
                    { 18, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1749), new TimeSpan(0, 7, 0, 0, 0)), null, null, 7, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1750), new TimeSpan(0, 7, 0, 0, 0)), 7 },
                    { 19, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1751), new TimeSpan(0, 7, 0, 0, 0)), null, null, 1, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1751), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 20, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1752), new TimeSpan(0, 7, 0, 0, 0)), null, null, 1, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1753), new TimeSpan(0, 7, 0, 0, 0)), 5 },
                    { 21, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1754), new TimeSpan(0, 7, 0, 0, 0)), null, null, 2, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1754), new TimeSpan(0, 7, 0, 0, 0)), 2 },
                    { 22, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1755), new TimeSpan(0, 7, 0, 0, 0)), null, null, 2, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1755), new TimeSpan(0, 7, 0, 0, 0)), 6 },
                    { 23, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1757), new TimeSpan(0, 7, 0, 0, 0)), null, null, 3, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1757), new TimeSpan(0, 7, 0, 0, 0)), 3 },
                    { 24, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1758), new TimeSpan(0, 7, 0, 0, 0)), null, null, 3, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1758), new TimeSpan(0, 7, 0, 0, 0)), 5 },
                    { 25, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1760), new TimeSpan(0, 7, 0, 0, 0)), null, null, 4, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1760), new TimeSpan(0, 7, 0, 0, 0)), 4 },
                    { 26, "RedBloodCells", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1761), new TimeSpan(0, 7, 0, 0, 0)), null, null, 4, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1761), new TimeSpan(0, 7, 0, 0, 0)), 6 },
                    { 27, "Plasma", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1762), new TimeSpan(0, 7, 0, 0, 0)), null, null, 5, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1763), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 28, "Plasma", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1764), new TimeSpan(0, 7, 0, 0, 0)), null, null, 6, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1764), new TimeSpan(0, 7, 0, 0, 0)), 2 },
                    { 29, "Plasma", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1765), new TimeSpan(0, 7, 0, 0, 0)), null, null, 5, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1766), new TimeSpan(0, 7, 0, 0, 0)), 3 },
                    { 30, "Plasma", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1767), new TimeSpan(0, 7, 0, 0, 0)), null, null, 6, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1767), new TimeSpan(0, 7, 0, 0, 0)), 4 },
                    { 31, "Plasma", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1768), new TimeSpan(0, 7, 0, 0, 0)), null, null, 1, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1769), new TimeSpan(0, 7, 0, 0, 0)), 8 },
                    { 32, "Plasma", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 7, 0, 0, 0)), null, null, 5, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1770), new TimeSpan(0, 7, 0, 0, 0)), 7 },
                    { 33, "Plasma", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1771), new TimeSpan(0, 7, 0, 0, 0)), null, null, 7, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1772), new TimeSpan(0, 7, 0, 0, 0)), 8 },
                    { 34, "Platelets", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1773), new TimeSpan(0, 7, 0, 0, 0)), null, null, 5, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1773), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 35, "Platelets", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1774), new TimeSpan(0, 7, 0, 0, 0)), null, null, 5, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1774), new TimeSpan(0, 7, 0, 0, 0)), 3 },
                    { 36, "Platelets", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1775), new TimeSpan(0, 7, 0, 0, 0)), null, null, 5, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1776), new TimeSpan(0, 7, 0, 0, 0)), 5 },
                    { 37, "Platelets", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1777), new TimeSpan(0, 7, 0, 0, 0)), null, null, 5, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1777), new TimeSpan(0, 7, 0, 0, 0)), 7 },
                    { 38, "Platelets", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1780), new TimeSpan(0, 7, 0, 0, 0)), null, null, 6, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1780), new TimeSpan(0, 7, 0, 0, 0)), 2 },
                    { 39, "Platelets", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1781), new TimeSpan(0, 7, 0, 0, 0)), null, null, 6, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1782), new TimeSpan(0, 7, 0, 0, 0)), 6 },
                    { 40, "Platelets", null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1783), new TimeSpan(0, 7, 0, 0, 0)), null, null, 6, true, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 203, DateTimeKind.Unspecified).AddTicks(1783), new TimeSpan(0, 7, 0, 0, 0)), 4 }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "BloodComponent", "BloodGroupId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "ExpiryDate", "LastUpdatedBy", "LastUpdatedTime", "Quantity" },
                values: new object[,]
                {
                    { 1, "WholeBlood", 1, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7301), new TimeSpan(0, 7, 0, 0, 0)), null, null, new DateTime(2025, 10, 19, 19, 42, 57, 545, DateTimeKind.Local).AddTicks(7304), null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7303), new TimeSpan(0, 7, 0, 0, 0)), 10 },
                    { 2, "Plasma", 2, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7316), new TimeSpan(0, 7, 0, 0, 0)), null, null, new DateTime(2025, 10, 19, 19, 42, 57, 545, DateTimeKind.Local).AddTicks(7318), null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7317), new TimeSpan(0, 7, 0, 0, 0)), 5 },
                    { 3, "RedBloodCells", 3, null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7320), new TimeSpan(0, 7, 0, 0, 0)), null, null, new DateTime(2025, 10, 19, 19, 42, 57, 545, DateTimeKind.Local).AddTicks(7321), null, new DateTimeOffset(new DateTime(2025, 6, 19, 19, 42, 57, 545, DateTimeKind.Unspecified).AddTicks(7320), new TimeSpan(0, 7, 0, 0, 0)), 8 }
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
                name: "IX_AspNetUsers_BloodGroupId",
                table: "AspNetUsers",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BloodCompatibilities_DonorBloodGroupId",
                table: "BloodCompatibilities",
                column: "DonorBloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodCompatibilities_RecipientBloodGroupId",
                table: "BloodCompatibilities",
                column: "RecipientBloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_BloodGroupId",
                table: "BloodRequests",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_BloodUnitId",
                table: "BloodRequests",
                column: "BloodUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_RequestedById",
                table: "BloodRequests",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_BloodUnits_BloodGroupId",
                table: "BloodUnits",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_BloodRequestId",
                table: "Donations",
                column: "BloodRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserId",
                table: "Donations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorAvailabilities_UserId",
                table: "DonorAvailabilities",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "BloodCompatibilities");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "DonorAvailabilities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BloodRequests");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BloodUnits");

            migrationBuilder.DropTable(
                name: "BloodGroups");
        }
    }
}
