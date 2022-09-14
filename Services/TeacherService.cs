using ControlRisksAcademy.DataAccess;
using ControlRisksAcademy.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Models
{
    public class TeacherService : ITeacherService
    {
        private readonly AcademyDbContext _context;

        public TeacherService(AcademyDbContext context)
        {
            _context = context;
        }

        public Task<Teacher> AddCourseAsync(Teacher model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCourseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _context.Teachers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Teacher>> ListAsync()
        {
            return await _context.Teachers.Include(x => x.CourseTaught).ToListAsync();
        }

        public Task<bool> UpdateCourseAsync(Teacher model, int id)
        {
            throw new NotImplementedException();
        }
    }
}