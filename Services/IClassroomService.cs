using ControlRisksAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Services
{
    public interface IClassroomService
    {
        Task<Classrooms> AddClassroomAsync(Classrooms model);

        Task<bool> UpdateclassroomAsync(Classrooms model, int id);

        Task<bool> DeleteClassroomAsync(int id);

        Task<Classrooms> GetByIdAsync(int id);

        Task<List<Classrooms>> ListAsync();
    }
}