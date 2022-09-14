using ControlRisksAcademy.Models;
using ControlRisksAcademy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlRisksAcademy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Students>>> GetStudentsAsync()
        {
            var result = await _studentService.ListAsync();
            return Ok(result);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<Students>> Get(int id)
        {
            var result = await _studentService.GetByIdAsync(id);
            if (result != null)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Students>> Post([FromBody] Students value)
        {
            try
            {
                var dbStudent = await _studentService.AddStudentAsync(value);
                return Ok(dbStudent);
            }
            catch (Exception ex)
            {
                return BadRequest($"Student record not created.{ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Students value)
        {
            try
            {
                var dbStudent = await _studentService.UpdateStudentAsync(value, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Student record not updated.{ex.Message}");
            }
        }

        [HttpDelete("deactivate/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var deleted = await _studentService.DeactivateStudentAsync(id);

                if (deleted)
                    return Ok("Student record deactivated.");

                return BadRequest("Student record not deactivated or record does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to deactivate course.. {ex.Message}");
            }
        }
    }
}