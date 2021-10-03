using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updateImageTableStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84dca7a7-8559-4e70-bea5-e555e50d7785");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9825ffc8-2e22-4898-ab20-78039ff71f68");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6f3f10e-c567-417a-9394-807e7d8436fa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91755379-0193-4700-8102-02081d483c1f", "7b3a7cb8-606e-4e84-9a09-9b211a5b99f8", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "66d4ec25-9721-47cd-a757-b311d148c33e", "f9f0db6d-c02c-4347-b4ad-eef3ddcdb2d8", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac9bf766-510d-4a5c-adad-b5b2425a9e08", "85f5e564-6712-43c5-8ced-de2d2cebb655", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66d4ec25-9721-47cd-a757-b311d148c33e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91755379-0193-4700-8102-02081d483c1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac9bf766-510d-4a5c-adad-b5b2425a9e08");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c6f3f10e-c567-417a-9394-807e7d8436fa", "54808671-bcfa-494e-9444-7bac17dd720a", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84dca7a7-8559-4e70-bea5-e555e50d7785", "bc5dbd42-89e7-47e9-a4cf-7ea124022cf6", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9825ffc8-2e22-4898-ab20-78039ff71f68", "e7510e1b-8b35-4732-86e8-c507ba94cc6c", "User", "USER" });
        }
    }
}
