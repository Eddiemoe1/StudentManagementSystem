using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnMarkStudentSubjectId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Marks_StudentSubjectId",
                table: "Marks");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_StudentSubjectId",
                table: "Marks",
                column: "StudentSubjectId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Marks_StudentSubjectId",
                table: "Marks");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_StudentSubjectId",
                table: "Marks",
                column: "StudentSubjectId");
        }
    }
}
