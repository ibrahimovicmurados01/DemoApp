using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoApp.Entities.Migrations
{
    /// <inheritdoc />
    public partial class firstUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "Email", "HashedPassword", "LastSigninDate", "Modified", "Tombstoned", "Username" },
                values: new object[] { new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751"), new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6332), new TimeSpan(0, 0, 0, 0, 0)), "administrator@pa.local", "10000.UTSxiyDe7455x7TCeB9ELg==.PhkV6KCeskBhxM1vgtBUOzXreczKGiYyJYcWjNHQTss=", null, new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6332), new TimeSpan(0, 0, 0, 0, 0)), false, "Administrator" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Created", "Email", "FirstName", "LastName", "Modified", "PhoneNumber", "Tombstoned", "UserId" },
                values: new object[,]
                {
                    { new Guid("535c3d4e-d84e-11ec-9d64-0242ac120002"), new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6465), new TimeSpan(0, 0, 0, 0, 0)), "test@gmail.com", "Azer", "Halovic", new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6465), new TimeSpan(0, 0, 0, 0, 0)), "559001122", false, new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751") },
                    { new Guid("535c3d4e-d84e-11ec-9d64-0242ac120003"), new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6467), new TimeSpan(0, 0, 0, 0, 0)), "test3@gmail.com", "Veli", "Halovic", new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6468), new TimeSpan(0, 0, 0, 0, 0)), "559001122", false, new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("535c3d4e-d84e-11ec-9d64-0242ac120002"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("535c3d4e-d84e-11ec-9d64-0242ac120003"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751"));
        }
    }
}
