using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Models
{
    public class Courses
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public Classrooms Classroom { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }
    }
}