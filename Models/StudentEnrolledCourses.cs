using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Models
{
    public class StudentEnrolledCourses
    {
        public int Id { get; set; }
        public Students Student { get; set; }
        public Courses Sourses { get; set; }
        public bool Active { get; set; }
        public int YearEnrolled { get; set; }
    }
}