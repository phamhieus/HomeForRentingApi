using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CreateNewTableAward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "103ff0e0-2afc-4433-b0f6-fc543ab2087a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c604ab2-322b-4eca-8168-450557caaa2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54f387ae-a9a5-4e21-87bf-4d8967f8efa1");

            migrationBuilder.CreateTable(
                name: "AwardAreas",
                columns: table => new
                {
                    AreaCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    AreaName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    AreaType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    ProvinceCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardAreas", x => x.AreaCode);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2e910a5-c79f-4ad2-a1d8-472d20b29e07", "5ec9436e-5906-4317-b410-0d12fab2ed8e", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee9a6997-d070-4334-9f56-2734a23adf7e", "83d6d17c-6bfc-47e7-9920-bde9429326e2", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5ce6abb-4735-4f3b-af3c-74639dad1514", "5671cd8b-35b1-4317-bee3-d87ab3636b8a", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardAreas");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5ce6abb-4735-4f3b-af3c-74639dad1514");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2e910a5-c79f-4ad2-a1d8-472d20b29e07");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee9a6997-d070-4334-9f56-2734a23adf7e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "103ff0e0-2afc-4433-b0f6-fc543ab2087a", "00cc1c77-693b-4719-abd3-105611fbddbb", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c604ab2-322b-4eca-8168-450557caaa2b", "f24074ba-ffc1-4671-a0a9-b9a03aa0f595", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54f387ae-a9a5-4e21-87bf-4d8967f8efa1", "6b14dc40-6475-4d11-a5b1-90e5bf7b742e", "User", "USER" });
        }
    }
}
