using ControlRisksAcademy.Entities;
using ControlRisksAcademy.Models;
using ControlRisksAcademy.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.DataAccess
{
    public class AcademyDbContext : IdentityDbContext<ApplicationUser>
    {
        public AcademyDbContext(DbContextOptions<AcademyDbContext> options) : base(options)
        {
        }

        public DbSet<Students> Students { get; set; }
        public DbSet<Classrooms> Classrooms { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Courses> Courses { get; set; }

        public DbSet<StudentEnrolledCourses> StudentEnrolledCourses { get; set; }
    }
}