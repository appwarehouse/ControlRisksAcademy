using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Models
{
    public class Students : Person
    {
        [Required]
        [MaxLength(8)]
        public string StudentNumber { get; set; }

        public List<StudentEnrolledCourses> CoursesTaken { get; set; }
    }
}