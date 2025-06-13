using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class fixingIssues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormTemplates_formTemplateId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Forms_FormId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_formTemplateId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "formTemplateId",
                table: "Answers");

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 33, 40, 494, DateTimeKind.Local).AddTicks(4099));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 33, 40, 494, DateTimeKind.Local).AddTicks(4112));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 33, 40, 494, DateTimeKind.Local).AddTicks(4114));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 33, 40, 494, DateTimeKind.Local).AddTicks(4115));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 33, 40, 494, DateTimeKind.Local).AddTicks(4116));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 33, 40, 494, DateTimeKind.Local).AddTicks(4117));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TemplateId",
                table: "Answers",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_FormTemplates_TemplateId",
                table: "Answers",
                column: "TemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Forms_FormId",
                table: "Answers",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormTemplates_TemplateId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Forms_FormId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TemplateId",
                table: "Answers");

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "Answers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "formTemplateId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 28, 16, 722, DateTimeKind.Local).AddTicks(5469));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 28, 16, 722, DateTimeKind.Local).AddTicks(5479));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 28, 16, 722, DateTimeKind.Local).AddTicks(5480));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 28, 16, 722, DateTimeKind.Local).AddTicks(5482));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 28, 16, 722, DateTimeKind.Local).AddTicks(5483));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 13, 23, 28, 16, 722, DateTimeKind.Local).AddTicks(5484));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_formTemplateId",
                table: "Answers",
                column: "formTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_FormTemplates_formTemplateId",
                table: "Answers",
                column: "formTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Forms_FormId",
                table: "Answers",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id");
        }
    }
}
