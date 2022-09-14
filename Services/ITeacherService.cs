using ControlRisksAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Services
{
    public interface ITeacherService
    {
        Task<Teacher> AddCourseAsync(Teacher model);

        Task<bool> UpdateCourseAsync(Teacher model, int id);

        Task<bool> DeleteCourseAsync(int id);

        Task<Teacher> GetByIdAsync(int id);

        Task<List<Teacher>> ListAsync();
    }
}