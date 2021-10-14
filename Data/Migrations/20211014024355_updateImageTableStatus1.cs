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
                keyValue: "a5ce6abb-4735-4f3b-af3c-74639dad1514");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2e910a5-c79f-4ad2-a1d8-472d20b29e07");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee9a6997-d070-4334-9f56-2734a23adf7e");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

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
    }
}
