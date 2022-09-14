using ControlRisksAcademy.DataAccess;
using ControlRisksAcademy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Services
{
    public class StudentService : IStudentService
    {
        private readonly AcademyDbContext _context;

        public StudentService(AcademyDbContext context)
        {
            _context = context;
        }

        public async Task<Students> AddStudentAsync(Students model)
        {
            try
            {
                var student = await _context.Students.
                    AnyAsync(x => x.StudentNumber.ToLower() != model.StudentNumber.ToLower());

                if (student)
                {
                    await _context.Students.AddAsync(model);
                    await _context.SaveChangesAsync();

                    model.StudentNumber = $"ST{model.Id.ToString().PadLeft(4, '0')}";
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

        public async Task<bool> DeactivateStudentAsync(int id)
        {
            try
            {
                var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);

                if (student != null)
                {
                    //remove the classroom
                    student.Active = !student.Active;
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

        public async Task<Students> GetByIdAsync(int id)
        {
            return await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Students>> ListAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<bool> UpdateStudentAsync(Students model, int id)
        {
            try
            {
                var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);

                if (student != null)
                {
                    student.Active = model.Active;
                    student.Name = model.Name;
                    student.Gender = model.Gender;
                    student.Dob = model.Dob;
                    student.EmailAddress = model.EmailAddress;
                    student.PhoneNumber = model.PhoneNumber;
                    student.StudentNumber = model.StudentNumber;
                    student.Surname = model.Surname;
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