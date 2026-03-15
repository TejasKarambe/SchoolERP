using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.DTOs.AcademicYear;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("api/academic-years")]
    public class AcademicYearsController : ControllerBase
    {
        private readonly IAcademicYearService _service;

        public AcademicYearsController(IAcademicYearService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAcademicYearDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAcademicYearDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result) return NotFound();

            return Ok();
        }
    }
}
