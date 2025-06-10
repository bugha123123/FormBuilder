using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class addingConverstionForTemplateTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "JobApplication");

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "EventRegistration");

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "FeedbackSurvey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Job Application");

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Event Registration");

            migrationBuilder.UpdateData(
                table: "FormTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Customer Feedback");
        }
    }
}
