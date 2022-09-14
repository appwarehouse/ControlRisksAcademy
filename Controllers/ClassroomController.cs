using ControlRisksAcademy.Models;
using ControlRisksAcademy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlRisksAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Classrooms>>> GetClassroomsAsync()
        {
            var result = await _classroomService.ListAsync();
            return Ok(result);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<Classrooms>> Get(int id)
        {
            var result = await _classroomService.GetByIdAsync(id);
            if (result != null)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Classrooms>> Post([FromBody] Classrooms value)
        {
            try
            {
                var dbClassroom = await _classroomService.AddClassroomAsync(value);
                return Ok(dbClassroom);
            }
            catch (Exception ex)
            {
                return BadRequest($"Classroom record not created.{ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Classrooms value)
        {
            try
            {
                var dbClassroom = await _classroomService.UpdateclassroomAsync(value, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Classroom record not updated.{ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var deleted = await _classroomService.DeleteClassroomAsync(id);

                if (deleted)
                    return Ok("Classroom record deleted.");

                return BadRequest("Classroom record not deleted or record does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete classroom record. {ex.Message}");
            }
        }
    }
}