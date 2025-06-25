using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class ashgdawdwadawdawd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormTemplateId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 56, 7, 613, DateTimeKind.Local).AddTicks(8867));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 56, 7, 613, DateTimeKind.Local).AddTicks(8877));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 56, 7, 613, DateTimeKind.Local).AddTicks(8878));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 56, 7, 613, DateTimeKind.Local).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 56, 7, 613, DateTimeKind.Local).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 56, 7, 613, DateTimeKind.Local).AddTicks(8881));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_FormTemplateId",
                table: "Answers",
                column: "FormTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_FormTemplates_FormTemplateId",
                table: "Answers",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormTemplates_FormTemplateId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_FormTemplateId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "FormTemplateId",
                table: "Answers");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 45, 4, 182, DateTimeKind.Local).AddTicks(2137));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 45, 4, 182, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 45, 4, 182, DateTimeKind.Local).AddTicks(2147));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 45, 4, 182, DateTimeKind.Local).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 45, 4, 182, DateTimeKind.Local).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 22, 45, 4, 182, DateTimeKind.Local).AddTicks(2150));
        }
    }
}
