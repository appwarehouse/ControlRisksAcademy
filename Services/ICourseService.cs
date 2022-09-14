using ControlRisksAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Services
{
    public interface ICourseService
    {
        //Add
        //Delete
        //Update
        Task<Courses> AddCourseAsync(Courses model);

        Task<bool> UpdateCourseAsync(Courses model, int id);

        Task<bool> DeactivateCourseAsync(int id);

        Task<Courses> GetByIdAsync(int id);

        Task<List<Courses>> ListAsync();
    }
}