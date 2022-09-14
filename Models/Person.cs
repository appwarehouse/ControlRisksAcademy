using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Models
{
    public abstract class Person
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string EmailAddress { get; set; }

        public char Gender { get; set; }

        public DateTime Dob { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }
    }
}