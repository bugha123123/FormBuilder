using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class removingProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Tags");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 28, 14, 15, 28, 367, DateTimeKind.Local).AddTicks(8779));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 28, 14, 15, 28, 367, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 28, 14, 15, 28, 367, DateTimeKind.Local).AddTicks(8792));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 28, 14, 15, 28, 367, DateTimeKind.Local).AddTicks(8793));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 28, 14, 15, 28, 367, DateTimeKind.Local).AddTicks(8794));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 28, 14, 15, 28, 367, DateTimeKind.Local).AddTicks(8795));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "TemplateId" },
                values: new object[] { new DateTime(2025, 6, 28, 14, 0, 52, 114, DateTimeKind.Local).AddTicks(971), 0 });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "TemplateId" },
                values: new object[] { new DateTime(2025, 6, 28, 14, 0, 52, 114, DateTimeKind.Local).AddTicks(985), 0 });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "TemplateId" },
                values: new object[] { new DateTime(2025, 6, 28, 14, 0, 52, 114, DateTimeKind.Local).AddTicks(986), 0 });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "TemplateId" },
                values: new object[] { new DateTime(2025, 6, 28, 14, 0, 52, 114, DateTimeKind.Local).AddTicks(987), 0 });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "TemplateId" },
                values: new object[] { new DateTime(2025, 6, 28, 14, 0, 52, 114, DateTimeKind.Local).AddTicks(988), 0 });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "TemplateId" },
                values: new object[] { new DateTime(2025, 6, 28, 14, 0, 52, 114, DateTimeKind.Local).AddTicks(989), 0 });
        }
    }
}
