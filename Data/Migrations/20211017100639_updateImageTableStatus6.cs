using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updateImageTableStatus6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10f3e2d7-4f3f-4def-926a-ea5c0d8206a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47a4c9ae-b051-4650-9ed0-666af63e6f06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2461ea1-6325-4277-b30a-216ca638694e");

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "Notifications",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b4c675ee-9a81-45a3-84c8-132c5f23c4d0", "3952eb16-384a-4004-9bc8-b41ea089e772", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "969fd4c6-2f20-4f46-a432-5b55cbc804ea", "16daeef0-22d5-409d-9b35-b4f0a9698dbd", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93c62ad3-ec84-450a-8e87-8ab9a1c34b9a", "094af4bb-84d7-4630-a6a4-7eae2347c369", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c62ad3-ec84-450a-8e87-8ab9a1c34b9a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "969fd4c6-2f20-4f46-a432-5b55cbc804ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4c675ee-9a81-45a3-84c8-132c5f23c4d0");

            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "Notifications");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47a4c9ae-b051-4650-9ed0-666af63e6f06", "78fa8d56-9852-49f7-af83-8fbe010b3722", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2461ea1-6325-4277-b30a-216ca638694e", "6e8a11d2-cccc-4343-a5af-907074b40bd3", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10f3e2d7-4f3f-4def-926a-ea5c0d8206a2", "a8e80572-fa17-498c-8312-d9810171f63e", "User", "USER" });
        }
    }
}
