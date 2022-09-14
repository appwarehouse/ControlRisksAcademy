using ControlRisksAcademy.DataAccess;
using ControlRisksAcademy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Services
{
    public class CourseService : ICourseService
    {
        private readonly AcademyDbContext _context;

        public CourseService(AcademyDbContext context)
        {
            _context = context;
        }

        public async Task<Courses> AddCourseAsync(Courses model)
        {
            try
            {
                var course = await _context.Courses.AnyAsync(x => x.Name.ToLower() != model.Name.ToLower());

                if (!course)
                {
                    await _context.Courses.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model;
                }
                throw new Exception("Duplicate record with the same name exists. Specify a new name.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeactivateCourseAsync(int id)
        {
            try
            {
                var course = await _context.Courses.SingleOrDefaultAsync(x => x.Id == id);

                if (course != null)
                {
                    if (course.Active)
                    {
                        //remove courses from this classroom
                        var teachers = _context.Teachers.Where(x => x.CourseTaught.Id == id).ToList();
                        teachers.ForEach(x => x.CourseTaught = null);
                    }

                    //remove the classroom
                    course.Active = !course.Active;
                    course.Classroom = null;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Courses> GetByIdAsync(int id)
        {
            return await _context.Courses.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Courses>> ListAsync()
        {
            return await _context.Courses.Include(x => x.Classroom).ToListAsync();
        }

        public async Task<bool> UpdateCourseAsync(Courses model, int id)
        {
            try
            {
                var course = await _context.Courses.SingleOrDefaultAsync(x => x.Id == id);

                if (course != null)
                {
                    course.Active = model.Active;
                    course.Name = model.Name;
                    course.Description = model.Description;
                    course.Classroom.Id = model.Classroom.Id;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}