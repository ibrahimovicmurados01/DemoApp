using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApp.Entities.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTableForUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("535c3d4e-d84e-11ec-9d64-0242ac120002"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 8, 2, 7, 6, 46, 617, DateTimeKind.Unspecified).AddTicks(8438), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 8, 2, 7, 6, 46, 617, DateTimeKind.Unspecified).AddTicks(8439), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("535c3d4e-d84e-11ec-9d64-0242ac120003"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 8, 2, 7, 6, 46, 617, DateTimeKind.Unspecified).AddTicks(8441), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 8, 2, 7, 6, 46, 617, DateTimeKind.Unspecified).AddTicks(8442), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7bb5fe4-11d7-4e48-b9d5-1e4cf76fd751"),
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 8, 2, 7, 6, 46, 617, DateTimeKind.Unspecified).AddTicks(8267), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 8, 2, 7, 6, 46, 617, DateTimeKind.Unspecified).AddTicks(8267), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

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
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 25, 10, 21, 35, 442, DateTimeKind.Unspecified).AddTicks(8757), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 7, 25, 10, 21, 35, 442, DateTimeKind.Unspecified).AddTicks(8758), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
