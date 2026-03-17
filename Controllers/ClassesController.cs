using Microsoft.AspNetCore.Mvc;
using SchoolApi.DTOs.Classes;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/classes")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _service;

        public ClassesController(IClassService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClassDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, UpdateClassDto dto)
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
