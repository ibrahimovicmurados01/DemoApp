using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApp.Entities.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("535c3d4e-d84e-11ec-9d64-0242ac120002"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 25, 10, 21, 35, 442, DateTimeKind.Unspecified).AddTicks(8922), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 7, 25, 10, 21, 35, 442, DateTimeKind.Unspecified).AddTicks(8923), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("535c3d4e-d84e-11ec-9d64-0242ac120003"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 25, 10, 21, 35, 442, DateTimeKind.Unspecified).AddTicks(8925), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 7, 25, 10, 21, 35, 442, DateTimeKind.Unspecified).AddTicks(8925), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751"),
                columns: new[] { "Created", "FirstName", "HashedPassword", "LastName", "Modified", "Username" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 25, 10, 21, 35, 442, DateTimeKind.Unspecified).AddTicks(8757), new TimeSpan(0, 0, 0, 0, 0)), null, "$2a$11$nIC0rs6cIFQVKOzEiQpweexL9GZZpm1E1mpHMIrMZVnodYtBYD5.i", null, new DateTimeOffset(new DateTime(2023, 7, 25, 10, 21, 35, 442, DateTimeKind.Unspecified).AddTicks(8758), new TimeSpan(0, 0, 0, 0, 0)), "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("535c3d4e-d84e-11ec-9d64-0242ac120002"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6465), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6465), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("535c3d4e-d84e-11ec-9d64-0242ac120003"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6467), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6468), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751"),
                columns: new[] { "Created", "HashedPassword", "Modified", "Username" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6332), new TimeSpan(0, 0, 0, 0, 0)), "10000.UTSxiyDe7455x7TCeB9ELg==.PhkV6KCeskBhxM1vgtBUOzXreczKGiYyJYcWjNHQTss=", new DateTimeOffset(new DateTime(2023, 7, 22, 14, 21, 30, 200, DateTimeKind.Unspecified).AddTicks(6332), new TimeSpan(0, 0, 0, 0, 0)), "Administrator" });
        }
    }
}
