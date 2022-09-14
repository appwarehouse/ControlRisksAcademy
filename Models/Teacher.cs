using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Models
{
    public class Teacher : Person
    {
        public string Title { get; set; }
        public Courses CourseTaught { get; set; }
    }
}