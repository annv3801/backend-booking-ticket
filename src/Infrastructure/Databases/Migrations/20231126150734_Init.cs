﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.EnsureSchema(
                name: "Category");

            migrationBuilder.EnsureSchema(
                name: "Film");

            migrationBuilder.EnsureSchema(
                name: "Food");

            migrationBuilder.EnsureSchema(
                name: "Group");

            migrationBuilder.EnsureSchema(
                name: "Room");

            migrationBuilder.EnsureSchema(
                name: "Scheduler");

            migrationBuilder.EnsureSchema(
                name: "Theater");

            migrationBuilder.EnsureSchema(
                name: "Ticket");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    AvatarPhoto = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FullName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PasswordChangeRequired = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    PasswordValidUntilDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PasswordHashTemporary = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "3"),
                    UserName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    SecurityStamp = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    LockoutEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "0"),
                    Otp = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true, defaultValueSql: "'000000'"),
                    OtpValidEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OtpCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "Group",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theaters",
                schema: "Theater",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TotalRating = table.Column<decimal>(type: "numeric", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<long>(type: "bigint", nullable: false),
                    Latitude = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "Ticket",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountLogin",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId1 = table.Column<long>(type: "bigint", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AccountLogin_Account_AccountId1",
                        column: x => x.AccountId1,
                        principalSchema: "Identity",
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountTokens",
                schema: "Identity",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTokens", x => new { x.LoginProvider, x.Name, x.AccountId });
                    table.ForeignKey(
                        name: "FK_AccountTokens_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountCategory",
                schema: "Identity",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCategory", x => new { x.AccountId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_AccountCategory_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Category",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    GroupEntityId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryIds = table.Column<List<long>>(type: "jsonb", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Director = table.Column<string>(type: "text", nullable: true),
                    Actor = table.Column<string>(type: "text", nullable: true),
                    Genre = table.Column<string>(type: "text", nullable: true),
                    Premiere = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<double>(type: "double precision", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: true),
                    Rated = table.Column<string>(type: "text", nullable: true),
                    Trailer = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    TotalRating = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Films_Groups_GroupEntityId",
                        column: x => x.GroupEntityId,
                        principalSchema: "Group",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                schema: "Food",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    GroupEntityId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Groups_GroupEntityId",
                        column: x => x.GroupEntityId,
                        principalSchema: "Group",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "Room",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    TheaterId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "Theater",
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountFavorites",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    TheaterId = table.Column<long>(type: "bigint", nullable: false),
                    FilmId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountFavorites_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountFavorites_Films_FilmId",
                        column: x => x.FilmId,
                        principalSchema: "Film",
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountFavorites_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "Theater",
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmFeedback",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilmId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmFeedback_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmFeedback_Films_FilmId",
                        column: x => x.FilmId,
                        principalSchema: "Film",
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedulers",
                schema: "Scheduler",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    FilmId = table.Column<long>(type: "bigint", nullable: false),
                    TheaterId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedulers_Films_FilmId",
                        column: x => x.FilmId,
                        principalSchema: "Film",
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedulers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "Room",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedulers_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "Theater",
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Account",
                columns: new[] { "Id", "AvatarPhoto", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "ModifiedBy", "ModifiedTime", "NormalizedEmail", "NormalizedUserName", "Otp", "OtpCount", "OtpValidEnd", "PasswordHash", "PasswordHashTemporary", "PasswordValidUntilDate", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "UserName" },
                values: new object[] { 921946681335811L, null, 921946681335812L, new DateTimeOffset(new DateTime(2023, 11, 26, 15, 7, 34, 494, DateTimeKind.Unspecified).AddTicks(6817), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "nva030801@gmail.com", true, "Nguyen Van An", true, true, null, 921946681335812L, new DateTimeOffset(new DateTime(2023, 11, 26, 15, 7, 34, 494, DateTimeKind.Unspecified).AddTicks(6805), new TimeSpan(0, 0, 0, 0, 0)), "NVA030801@GMAIL.COM", "NVA3801", "000000", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AMJoiJQ9xLazxisVPXx+lBDRw7wfWBerhXipsLpHNGLXGAAKIeCnwi5XhIRbTbqovA==", null, null, "0966093801", true, "20B3F053-768A-4EF6-8378-DC42FF4E87C4", 3, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Email",
                schema: "Identity",
                table: "Account",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Account_NormalizedEmail",
                schema: "Identity",
                table: "Account",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Account_PhoneNumber",
                schema: "Identity",
                table: "Account",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountCategory_CategoryId",
                schema: "Identity",
                table: "AccountCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFavorites_AccountId",
                schema: "Identity",
                table: "AccountFavorites",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFavorites_FilmId",
                schema: "Identity",
                table: "AccountFavorites",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFavorites_TheaterId",
                schema: "Identity",
                table: "AccountFavorites",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountLogin_AccountId1",
                schema: "Identity",
                table: "AccountLogin",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountLogin_LoginProvider_ProviderKey",
                schema: "Identity",
                table: "AccountLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTokens_AccountId",
                schema: "Identity",
                table: "AccountTokens",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTokens_LoginProvider_Name_AccountId",
                schema: "Identity",
                table: "AccountTokens",
                columns: new[] { "LoginProvider", "Name", "AccountId" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "Category",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmFeedback_AccountId",
                schema: "Film",
                table: "FilmFeedback",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmFeedback_FilmId",
                schema: "Film",
                table: "FilmFeedback",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_GroupEntityId",
                schema: "Film",
                table: "Films",
                column: "GroupEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Name",
                schema: "Film",
                table: "Films",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_GroupEntityId",
                schema: "Food",
                table: "Foods",
                column: "GroupEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_Title",
                schema: "Food",
                table: "Foods",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Title",
                schema: "Group",
                table: "Groups",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name",
                schema: "Room",
                table: "Rooms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TheaterId",
                schema: "Room",
                table: "Rooms",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulers_FilmId",
                schema: "Scheduler",
                table: "Schedulers",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulers_RoomId",
                schema: "Scheduler",
                table: "Schedulers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulers_TheaterId",
                schema: "Scheduler",
                table: "Schedulers",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Theaters_Name",
                schema: "Theater",
                table: "Theaters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Title",
                schema: "Ticket",
                table: "Tickets",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCategory",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AccountFavorites",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AccountLogin",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AccountTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "FilmFeedback",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Foods",
                schema: "Food");

            migrationBuilder.DropTable(
                name: "Schedulers",
                schema: "Scheduler");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "Ticket");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Category");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Films",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "Room");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "Group");

            migrationBuilder.DropTable(
                name: "Theaters",
                schema: "Theater");
        }
    }
}