using ControlRisksAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Services
{
    public interface IStudentEnrollment
    {
        Task<StudentEnrolledCourses> AddStudentEnrollmentAsync(StudentEnrolledCourses model);

        Task<List<StudentEnrolledCourses>> AddStudentEnrollmentAsync(List<StudentEnrolledCourses> model);

        //Task<bool> UpdateStudentEnrollmentAsync(StudentEnrolledCourses model, int studentId);

        Task<List<StudentEnrolledCourses>> GetByIdAsync(int studentId);
    }
}