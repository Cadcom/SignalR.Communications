using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR.Data.Migrations
{
    public partial class DatabaseInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isLeftDoorOpen = table.Column<bool>(type: "bit", nullable: false),
                    isRightDoorOpen = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cars_Purchases_PurchaseID",
                        column: x => x.PurchaseID,
                        principalTable: "Purchases",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "ID", "CarType", "PurchaseID", "isLeftDoorOpen", "isRightDoorOpen" },
                values: new object[] { 1, "BMW", null, false, false });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "ID", "CarType", "PurchaseID", "isLeftDoorOpen", "isRightDoorOpen" },
                values: new object[] { 2, "Mercedes", null, false, false });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PurchaseID",
                table: "Cars",
                column: "PurchaseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Purchases");
        }
    }
}
