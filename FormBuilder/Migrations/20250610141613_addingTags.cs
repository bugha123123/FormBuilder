using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class addingTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "FormTemplateId", "Name", "TemplateId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 10, 18, 16, 12, 697, DateTimeKind.Local).AddTicks(8127), null, "HR", 1, null },
                    { 2, new DateTime(2025, 6, 10, 18, 16, 12, 697, DateTimeKind.Local).AddTicks(8137), null, "Recruitment", 1, null },
                    { 3, new DateTime(2025, 6, 10, 18, 16, 12, 697, DateTimeKind.Local).AddTicks(8139), null, "Event", 2, null },
                    { 4, new DateTime(2025, 6, 10, 18, 16, 12, 697, DateTimeKind.Local).AddTicks(8140), null, "Signup", 2, null },
                    { 5, new DateTime(2025, 6, 10, 18, 16, 12, 697, DateTimeKind.Local).AddTicks(8143), null, "Customer", 3, null },
                    { 6, new DateTime(2025, 6, 10, 18, 16, 12, 697, DateTimeKind.Local).AddTicks(8144), null, "Survey", 3, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tags",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
