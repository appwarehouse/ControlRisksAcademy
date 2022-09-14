using ControlRisksAcademy.Models;
using ControlRisksAcademy.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlRisksAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEnrollmentController : ControllerBase
    {
        private readonly IStudentEnrollment _studentEnrollmentService;

        public StudentEnrollmentController(IStudentEnrollment studentEnrollmentService)
        {
            _studentEnrollmentService = studentEnrollmentService;
        }

        [HttpGet("list/{studentId}")]
        public async Task<ActionResult<List<StudentEnrolledCourses>>> GetStudentEnrollmentAsync(int studentId)
        {
            var result = await _studentEnrollmentService.GetByIdAsync(studentId);
            return Ok(result);
        }

        [HttpPost("new/single")]
        public async Task<ActionResult<StudentEnrollment>> Post([FromBody] StudentEnrolledCourses value)
        {
            try
            {
                var dbEnrollment = await _studentEnrollmentService.AddStudentEnrollmentAsync(value);
                return Ok(dbEnrollment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not add enrollment.{ex.Message}");
            }
        }

        [HttpPost("new/multiple")]
        public async Task<ActionResult<List<StudentEnrollment>>> PostList([FromBody] List<StudentEnrolledCourses> value)
        {
            try
            {
                var dbEnrollment = await _studentEnrollmentService.AddStudentEnrollmentAsync(value);
                return Ok(dbEnrollment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not add enrollment.{ex.Message}");
            }
        }
    }
}