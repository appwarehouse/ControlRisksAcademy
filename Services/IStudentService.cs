using ControlRisksAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Services
{
    public interface IStudentService
    {
        Task<Students> AddStudentAsync(Students model);

        Task<bool> UpdateStudentAsync(Students model, int id);

        Task<bool> DeactivateStudentAsync(int id);

        Task<Students> GetByIdAsync(int id);

        Task<List<Students>> ListAsync();
    }
}