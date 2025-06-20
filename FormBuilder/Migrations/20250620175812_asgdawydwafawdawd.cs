using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class asgdawydwafawdawd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormTemplates_formTemplateId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Forms_formId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_formTemplateId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "formTemplateId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "formId",
                table: "Answers",
                newName: "FormId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_formId",
                table: "Answers",
                newName: "IX_Answers_FormId");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 58, 12, 520, DateTimeKind.Local).AddTicks(6752));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 58, 12, 520, DateTimeKind.Local).AddTicks(6764));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 58, 12, 520, DateTimeKind.Local).AddTicks(6765));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 58, 12, 520, DateTimeKind.Local).AddTicks(6766));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 58, 12, 520, DateTimeKind.Local).AddTicks(6767));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 58, 12, 520, DateTimeKind.Local).AddTicks(6768));

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Forms_FormId",
                table: "Answers",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.RenameColumn(
                name: "FormId",
                table: "Answers",
                newName: "formId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_FormId",
                table: "Answers",
                newName: "IX_Answers_formId");

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
                value: new DateTime(2025, 6, 20, 21, 57, 38, 997, DateTimeKind.Local).AddTicks(1805));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 57, 38, 997, DateTimeKind.Local).AddTicks(1815));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 57, 38, 997, DateTimeKind.Local).AddTicks(1816));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 57, 38, 997, DateTimeKind.Local).AddTicks(1817));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 57, 38, 997, DateTimeKind.Local).AddTicks(1818));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 21, 57, 38, 997, DateTimeKind.Local).AddTicks(1819));

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
                name: "FK_Answers_Forms_formId",
                table: "Answers",
                column: "formId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
