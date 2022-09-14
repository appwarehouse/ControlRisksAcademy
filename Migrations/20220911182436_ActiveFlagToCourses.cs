using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlRisksAcademy.Migrations
{
    public partial class ActiveFlagToCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrolledCourses_Courses_coursesId",
                table: "StudentEnrolledCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrolledCourses_Students_studentId",
                table: "StudentEnrolledCourses");

            migrationBuilder.RenameColumn(
                name: "yearEnrolled",
                table: "StudentEnrolledCourses",
                newName: "YearEnrolled");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "StudentEnrolledCourses",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "coursesId",
                table: "StudentEnrolledCourses",
                newName: "SoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEnrolledCourses_studentId",
                table: "StudentEnrolledCourses",
                newName: "IX_StudentEnrolledCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEnrolledCourses_coursesId",
                table: "StudentEnrolledCourses",
                newName: "IX_StudentEnrolledCourses_SoursesId");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrolledCourses_Courses_SoursesId",
                table: "StudentEnrolledCourses",
                column: "SoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrolledCourses_Students_StudentId",
                table: "StudentEnrolledCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrolledCourses_Courses_SoursesId",
                table: "StudentEnrolledCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrolledCourses_Students_StudentId",
                table: "StudentEnrolledCourses");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "YearEnrolled",
                table: "StudentEnrolledCourses",
                newName: "yearEnrolled");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentEnrolledCourses",
                newName: "studentId");

            migrationBuilder.RenameColumn(
                name: "SoursesId",
                table: "StudentEnrolledCourses",
                newName: "coursesId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEnrolledCourses_StudentId",
                table: "StudentEnrolledCourses",
                newName: "IX_StudentEnrolledCourses_studentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEnrolledCourses_SoursesId",
                table: "StudentEnrolledCourses",
                newName: "IX_StudentEnrolledCourses_coursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrolledCourses_Courses_coursesId",
                table: "StudentEnrolledCourses",
                column: "coursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrolledCourses_Students_studentId",
                table: "StudentEnrolledCourses",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
