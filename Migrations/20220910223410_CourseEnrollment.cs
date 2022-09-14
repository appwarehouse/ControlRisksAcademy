using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlRisksAcademy.Migrations
{
    public partial class CourseEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Students_StudentsId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudentsId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentsId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "StudentEnrolledCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<int>(type: "int", nullable: true),
                    coursesId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    yearEnrolled = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEnrolledCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEnrolledCourses_Courses_coursesId",
                        column: x => x.coursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentEnrolledCourses_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrolledCourses_coursesId",
                table: "StudentEnrolledCourses",
                column: "coursesId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrolledCourses_studentId",
                table: "StudentEnrolledCourses",
                column: "studentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentEnrolledCourses");

            migrationBuilder.AddColumn<int>(
                name: "StudentsId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudentsId",
                table: "Courses",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Students_StudentsId",
                table: "Courses",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
