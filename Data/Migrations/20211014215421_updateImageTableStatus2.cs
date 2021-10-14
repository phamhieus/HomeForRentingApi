using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updateImageTableStatus2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19a2388c-8d96-4966-a43d-4777b20db04b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dc84a82-ec5b-40bd-8be5-e5a5db5e7334");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f090759d-138b-4d59-a475-dfc99e065bb2");

            migrationBuilder.AlterColumn<string>(
                name: "EvaluatedUser",
                table: "CommentedUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<byte[]>(
                name: "EvaluatedUser",
                table: "CommentedUsers",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f090759d-138b-4d59-a475-dfc99e065bb2", "712fcf07-21d3-485b-83d5-aa9ec7810fa7", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19a2388c-8d96-4966-a43d-4777b20db04b", "0b87769d-2690-4f22-8672-20174d3723b0", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5dc84a82-ec5b-40bd-8be5-e5a5db5e7334", "468c971a-1468-4595-b99a-3cc40f1210af", "User", "USER" });
        }
    }
}
