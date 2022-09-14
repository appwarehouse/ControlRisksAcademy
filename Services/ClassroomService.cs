using ControlRisksAcademy.DataAccess;
using ControlRisksAcademy.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Models
{
    public class ClassroomService : IClassroomService

    {
        private readonly AcademyDbContext _context;

        public ClassroomService(AcademyDbContext context)
        {
            _context = context;
        }

        public async Task<Classrooms> AddClassroomAsync(Classrooms model)
        {
            try
            {
                var classroom = await _context.Classrooms.AnyAsync(x => x.Name.ToLower() == model.Name.ToLower());

                if (!classroom)
                {
                    await _context.Classrooms.AddAsync(model);
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

        public async Task<bool> DeleteClassroomAsync(int id)
        {
            try
            {
                var classroom = await _context.Classrooms.SingleOrDefaultAsync(x => x.Id == id);

                if (classroom != null)
                {
                    //remove courses from this classroom
                    var courses = _context.Courses.Where(x => x.Classroom == classroom).ToList();
                    courses.ForEach(x => x.Classroom = null);

                    //remove the classroom
                    _context.Classrooms.Remove(classroom);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to delete. Record linked to other records.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Classrooms> GetByIdAsync(int id)
        {
            return await _context.Classrooms.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Classrooms>> ListAsync()
        {
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<bool> UpdateclassroomAsync(Classrooms model, int id)
        {
            try
            {
                var classroom = await _context.Classrooms.SingleOrDefaultAsync(x => x.Id == id);

                if (classroom != null)
                {
                    classroom.Active = model.Active;
                    classroom.Name = model.Name;
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