using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class addingTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplates_AspNetUsers_UserId",
                table: "FormTemplates");

            migrationBuilder.RenameColumn(
                name: "Options",
                table: "Questions",
                newName: "OptionsJson");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FormTemplates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "FormTemplates",
                columns: new[] { "Id", "CreatedAt", "Description", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basic job application form.", "Job Application", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Template for event sign-ups.", "Event Registration", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Collect feedback from customers.", "Customer Feedback", null }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "OptionsJson", "TemplateId", "Text", "Type" },
                values: new object[,]
                {
                    { 1, null, 1, "Full Name", 0 },
                    { 2, null, 1, "Email Address", 0 },
                    { 3, null, 1, "Position Applying For", 5 },
                    { 4, null, 2, "Attendee Name", 0 },
                    { 5, null, 2, "Email", 0 },
                    { 6, null, 2, "Select Sessions", 5 },
                    { 7, null, 3, "Overall Satisfaction", 5 },
                    { 8, null, 3, "Comments", 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplates_AspNetUsers_UserId",
                table: "FormTemplates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplates_AspNetUsers_UserId",
                table: "FormTemplates");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "OptionsJson",
                table: "Questions",
                newName: "Options");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FormTemplates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplates_AspNetUsers_UserId",
                table: "FormTemplates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
