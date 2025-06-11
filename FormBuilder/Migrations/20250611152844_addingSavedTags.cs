using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class addingSavedTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_FormTemplates_FormTemplateId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_FormTemplateId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "FormTemplateId",
                table: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "SavedTags",
                table: "FormTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "SavedTags",
                value: null);

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "SavedTags",
                value: null);

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "SavedTags",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 19, 28, 43, 785, DateTimeKind.Local).AddTicks(7298));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 19, 28, 43, 785, DateTimeKind.Local).AddTicks(7308));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 19, 28, 43, 785, DateTimeKind.Local).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 19, 28, 43, 785, DateTimeKind.Local).AddTicks(7311));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 19, 28, 43, 785, DateTimeKind.Local).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 19, 28, 43, 785, DateTimeKind.Local).AddTicks(7313));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavedTags",
                table: "FormTemplates");

            migrationBuilder.AddColumn<int>(
                name: "FormTemplateId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FormTemplateId" },
                values: new object[] { new DateTime(2025, 6, 10, 19, 11, 19, 781, DateTimeKind.Local).AddTicks(1515), null });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FormTemplateId" },
                values: new object[] { new DateTime(2025, 6, 10, 19, 11, 19, 781, DateTimeKind.Local).AddTicks(1524), null });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FormTemplateId" },
                values: new object[] { new DateTime(2025, 6, 10, 19, 11, 19, 781, DateTimeKind.Local).AddTicks(1526), null });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FormTemplateId" },
                values: new object[] { new DateTime(2025, 6, 10, 19, 11, 19, 781, DateTimeKind.Local).AddTicks(1528), null });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FormTemplateId" },
                values: new object[] { new DateTime(2025, 6, 10, 19, 11, 19, 781, DateTimeKind.Local).AddTicks(1529), null });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FormTemplateId" },
                values: new object[] { new DateTime(2025, 6, 10, 19, 11, 19, 781, DateTimeKind.Local).AddTicks(1531), null });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_FormTemplateId",
                table: "Tags",
                column: "FormTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_FormTemplates_FormTemplateId",
                table: "Tags",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id");
        }
    }
}
