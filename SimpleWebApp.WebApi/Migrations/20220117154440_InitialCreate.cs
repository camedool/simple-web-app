using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebApp.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<long>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<long>(type: "INTEGER", nullable: false),
                    WarehouseId = table.Column<long>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<long>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "T-Shirt", "Clothes" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Android phone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mobile phone", "Digital" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pain killer", "HealthCare" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dark chocolate", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chocolate", "Food" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "General package 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good 1", "General" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 6L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "General package 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good 2", "General" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 7L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good 3", "General" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 8L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good 4", "General" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 9L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Notebook", "Digital" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Created", "Description", "Modified", "Name", "Type" },
                values: new object[] { 10L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In a package of 12", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water", "Food" });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Created", "Location", "Modified", "Name" },
                values: new object[] { 1L, 5000L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prague", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CZ 1" });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Created", "Location", "Modified", "Name" },
                values: new object[] { 2L, 3000L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brno", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CZ 2" });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Created", "Location", "Modified", "Name" },
                values: new object[] { 3L, 4000L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "London", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GB 1" });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "Created", "Description", "ItemId", "Modified", "Quantity", "WarehouseId" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basic", 10L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200L, 1L });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "Created", "Description", "ItemId", "Modified", "Quantity", "WarehouseId" },
                values: new object[] { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "For fashion week", 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000L, 2L });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "Created", "Description", "ItemId", "Modified", "Quantity", "WarehouseId" },
                values: new object[] { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "For sale with 50% discount", 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ItemId",
                table: "Inventories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_WarehouseId",
                table: "Inventories",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
