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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Teacher>>> GetTeachersAsync()
        {
            var result = await _teacherService.ListAsync();
            return Ok(result);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<Teacher>> Get(int id)
        {
            var result = await _teacherService.GetByIdAsync(id);
            if (result != null)
                return Ok(result);

            return NotFound(result);
        }
    }
}