using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updateImageTableStatus1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RoomImages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba5f7a22-a09f-4823-85e0-45663c6a3009", "65f83e4c-bc31-41aa-ba57-174a063e38ed", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31774689-64a6-405f-a1e3-15efe439139e", "9bc3d47e-5ecc-4927-867e-04738b408f68", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "073b705b-c3e7-48a7-a44e-f97e375109d8", "cabc2a0a-286c-40f6-9003-eb30b16ee33d", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "073b705b-c3e7-48a7-a44e-f97e375109d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31774689-64a6-405f-a1e3-15efe439139e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba5f7a22-a09f-4823-85e0-45663c6a3009");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RoomImages");

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
    }
}
