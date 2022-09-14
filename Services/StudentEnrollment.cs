using ControlRisksAcademy.DataAccess;
using ControlRisksAcademy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Services
{
    public class StudentEnrollment : IStudentEnrollment
    {
        private readonly AcademyDbContext _context;

        public StudentEnrollment(AcademyDbContext context)
        {
            _context = context;
        }

        public async Task<StudentEnrolledCourses> AddStudentEnrollmentAsync(StudentEnrolledCourses model)
        {
            var enrollment = await CreateStudentEnrollmentAsync(model);
            return enrollment;
        }

        public async Task<List<StudentEnrolledCourses>> AddStudentEnrollmentAsync(List<StudentEnrolledCourses> model)
        {
            List<StudentEnrolledCourses> newEnroll = new();
            model.ForEach(async x =>
            {
                var enrollment = await CreateStudentEnrollmentAsync(x);
                newEnroll.Add(enrollment);
            });

            return newEnroll;
        }

        public async Task<List<StudentEnrolledCourses>> GetByIdAsync(int studentId)
        {
            var enrollment = await _context.StudentEnrolledCourses
                                            .Where(x => x.Student.Id == studentId)
                                            .ToListAsync();

            return enrollment;
        }

        private async Task<StudentEnrolledCourses> CreateStudentEnrollmentAsync(StudentEnrolledCourses model)
        {
            try
            {
                var enrollment = await _context.StudentEnrolledCourses.
                                                SingleOrDefaultAsync(x => x.Student.Id == model.Student.Id &&
                                                x.YearEnrolled == model.YearEnrolled &&
                                                x.Sourses.Id == model.Sourses.Id);

                if (enrollment != null && enrollment.Active == false)
                    enrollment.Active = true;
                else
                    await _context.StudentEnrolledCourses.AddAsync(model);

                await _context.SaveChangesAsync();
                return enrollment;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}