using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updateImageTableStatus3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45a6051f-7c00-4613-bad5-3cdd7874bfdc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4bbf890-23e2-4cb8-85f5-cf149ae10f6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1777687-5861-4d2e-8770-05f987b0548f");

            migrationBuilder.AddColumn<string>(
                name: "CommentByUserName",
                table: "CommentedUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eed22a2a-664c-4290-8399-30bdf2e72014", "822f2036-1840-4333-abe5-3e696d767c0c", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f57ab28-12a4-477b-9f45-6cc3b0a86ae0", "8ec4dcb0-f134-49a7-b9da-14fd1e6b1466", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "732b21b1-33fd-4461-ab64-5130ac7215dd", "bc7baec6-42f9-4e93-8b31-04bff83be088", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "732b21b1-33fd-4461-ab64-5130ac7215dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f57ab28-12a4-477b-9f45-6cc3b0a86ae0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eed22a2a-664c-4290-8399-30bdf2e72014");

            migrationBuilder.DropColumn(
                name: "CommentByUserName",
                table: "CommentedUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45a6051f-7c00-4613-bad5-3cdd7874bfdc", "7039cc7d-a89f-4999-9b63-f0d058492fa9", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4bbf890-23e2-4cb8-85f5-cf149ae10f6f", "cc0ae489-f367-42fc-8620-08682c6b5448", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f1777687-5861-4d2e-8770-05f987b0548f", "016e49dd-2d64-4f0a-9d9f-29c06b425d28", "User", "USER" });
        }
    }
}
