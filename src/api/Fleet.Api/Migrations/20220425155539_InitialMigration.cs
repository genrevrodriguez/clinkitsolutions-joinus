using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IconPath = table.Column<string>(type: "TEXT", nullable: true),
                    ParentAssetCategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetCategories_AssetCategories_ParentAssetCategoryId",
                        column: x => x.ParentAssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fleets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fleets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AssetCategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetFleets",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "INTEGER", nullable: false),
                    FleetId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetFleets", x => new { x.AssetId, x.FleetId });
                    table.ForeignKey(
                        name: "FK_AssetFleets_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetFleets_Fleets_FleetId",
                        column: x => x.FleetId,
                        principalTable: "Fleets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetLogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FileId = table.Column<string>(type: "TEXT", nullable: true),
                    AssetId = table.Column<int>(type: "INTEGER", nullable: true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: true),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Location_Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetLogItems_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AssetCategories",
                columns: new[] { "Id", "IconPath", "Name", "ParentAssetCategoryId" },
                values: new object[] { 1, null, "Vehicle", null });

            migrationBuilder.CreateIndex(
                name: "IX_AssetCategories_ParentAssetCategoryId",
                table: "AssetCategories",
                column: "ParentAssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetFleets_FleetId",
                table: "AssetFleets",
                column: "FleetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLogItems_AssetId",
                table: "AssetLogItems",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetCategoryId",
                table: "Assets",
                column: "AssetCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetFleets");

            migrationBuilder.DropTable(
                name: "AssetLogItems");

            migrationBuilder.DropTable(
                name: "Fleets");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetCategories");
        }
    }
}
