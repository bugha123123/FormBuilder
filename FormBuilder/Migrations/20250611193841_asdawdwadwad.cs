using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class asdawdwadwad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedUsers",
                table: "FormTemplates");

            migrationBuilder.AddColumn<int>(
                name: "FormTemplateId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 38, 41, 459, DateTimeKind.Local).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 38, 41, 459, DateTimeKind.Local).AddTicks(7305));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 38, 41, 459, DateTimeKind.Local).AddTicks(7306));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 38, 41, 459, DateTimeKind.Local).AddTicks(7307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 38, 41, 459, DateTimeKind.Local).AddTicks(7308));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 38, 41, 459, DateTimeKind.Local).AddTicks(7310));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FormTemplateId",
                table: "AspNetUsers",
                column: "FormTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FormTemplates_FormTemplateId",
                table: "AspNetUsers",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FormTemplates_FormTemplateId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FormTemplateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FormTemplateId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AssignedUsers",
                table: "FormTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedUsers",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedUsers",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedUsers",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 35, 43, 107, DateTimeKind.Local).AddTicks(7366));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 35, 43, 107, DateTimeKind.Local).AddTicks(7376));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 35, 43, 107, DateTimeKind.Local).AddTicks(7377));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 35, 43, 107, DateTimeKind.Local).AddTicks(7379));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 35, 43, 107, DateTimeKind.Local).AddTicks(7380));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 35, 43, 107, DateTimeKind.Local).AddTicks(7381));
        }
    }
}
