using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class asdgawdwadwafawdadWAD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: true),
                    LikedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LikeTargetType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_FormTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CommentId",
                table: "Likes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_TemplateId",
                table: "Likes",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormTemplates_formTemplateId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Forms_formId",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "Likes");

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
                value: new DateTime(2025, 6, 17, 15, 40, 59, 713, DateTimeKind.Local).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 17, 15, 40, 59, 713, DateTimeKind.Local).AddTicks(7468));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 17, 15, 40, 59, 713, DateTimeKind.Local).AddTicks(7469));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 17, 15, 40, 59, 713, DateTimeKind.Local).AddTicks(7470));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 17, 15, 40, 59, 713, DateTimeKind.Local).AddTicks(7471));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 17, 15, 40, 59, 713, DateTimeKind.Local).AddTicks(7472));

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
    }
}
