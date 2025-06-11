using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class asdhawdhwad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: null);

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedUsers",
                value: null);

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedUsers",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 47, 46, 12, DateTimeKind.Local).AddTicks(1805));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 47, 46, 12, DateTimeKind.Local).AddTicks(1816));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 47, 46, 12, DateTimeKind.Local).AddTicks(1818));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 47, 46, 12, DateTimeKind.Local).AddTicks(1819));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 47, 46, 12, DateTimeKind.Local).AddTicks(1820));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 47, 46, 12, DateTimeKind.Local).AddTicks(1821));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2025, 6, 11, 23, 44, 36, 814, DateTimeKind.Local).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 44, 36, 814, DateTimeKind.Local).AddTicks(9155));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 44, 36, 814, DateTimeKind.Local).AddTicks(9156));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 44, 36, 814, DateTimeKind.Local).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 44, 36, 814, DateTimeKind.Local).AddTicks(9158));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 11, 23, 44, 36, 814, DateTimeKind.Local).AddTicks(9159));

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
    }
}
