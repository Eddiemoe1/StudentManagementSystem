using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class modifyingstudenttableandusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "StudentSubjects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_UserId",
                table: "StudentSubjects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Users_UserId",
                table: "StudentSubjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Users_UserId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_UserId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "StudentNo",
                table: "Students");
        }
    }
}
