using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Migrations
{
    /// <inheritdoc />
    public partial class fixinglIkes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 42, 51, 486, DateTimeKind.Local).AddTicks(4230));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 42, 51, 486, DateTimeKind.Local).AddTicks(4243));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 42, 51, 486, DateTimeKind.Local).AddTicks(4244));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 42, 51, 486, DateTimeKind.Local).AddTicks(4246));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 42, 51, 486, DateTimeKind.Local).AddTicks(4247));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 42, 51, 486, DateTimeKind.Local).AddTicks(4291));

            migrationBuilder.CreateIndex(
                name: "IX_Likes_FormId",
                table: "Likes",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Forms_FormId",
                table: "Likes",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Forms_FormId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_FormId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Likes");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 29, 8, 814, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 29, 8, 814, DateTimeKind.Local).AddTicks(2671));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 29, 8, 814, DateTimeKind.Local).AddTicks(2673));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 29, 8, 814, DateTimeKind.Local).AddTicks(2674));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 29, 8, 814, DateTimeKind.Local).AddTicks(2675));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 22, 0, 29, 8, 814, DateTimeKind.Local).AddTicks(2676));
        }
    }
}
