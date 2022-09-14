using ControlRisksAcademy.Models;
using ControlRisksAcademy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Courses>>> GetCoursesAsync()
        {
            var result = await _courseService.ListAsync();
            return Ok(result);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<Courses>> Get(int id)
        {
            var result = await _courseService.GetByIdAsync(id);
            if (result != null)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Courses>> Post([FromBody] Courses value)
        {
            try
            {
                var dbClassroom = await _courseService.AddCourseAsync(value);
                return Ok(dbClassroom);
            }
            catch (Exception ex)
            {
                return BadRequest($"Course record not created.{ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Courses value)
        {
            try
            {
                var dbCourse = await _courseService.UpdateCourseAsync(value, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Course record not updated.{ex.Message}");
            }
        }

        [HttpDelete("deactivate/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var deleted = await _courseService.DeactivateCourseAsync(id);

                if (deleted)
                    return Ok("Course record deactivated.");

                return BadRequest("Course record not deactivated or record does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to deactivate course.. {ex.Message}");
            }
        }
    }
}