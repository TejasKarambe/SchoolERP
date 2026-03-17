using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.DTOs.Exams;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/exams")]
    [Authorize]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _service;

        public ExamsController(IExamService service)
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

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetByClass(int classId)
        {
            return Ok(await _service.GetByClassAsync(classId));
        }

        [HttpGet("academic-year/{academicYearId}")]
        public async Task<IActionResult> GetByAcademicYear(int academicYearId)
        {
            return Ok(await _service.GetByAcademicYearAsync(academicYearId));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateExamDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateExamDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // ── Exam Results ───────────────────────────────────────────────────────

        [HttpGet("{examId}/results")]
        public async Task<IActionResult> GetResultsByExam(int examId)
        {
            return Ok(await _service.GetResultsByExamAsync(examId));
        }

        [HttpGet("results/student/{studentId}")]
        public async Task<IActionResult> GetResultsByStudent(int studentId)
        {
            return Ok(await _service.GetResultsByStudentAsync(studentId));
        }

        [HttpPost("results")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateResult(CreateExamResultDto dto)
        {
            var result = await _service.CreateResultAsync(dto);
            return Ok(result);
        }

        [HttpPost("results/bulk")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> BulkCreateResults(BulkExamResultDto dto)
        {
            var results = await _service.BulkCreateResultsAsync(dto);
            return Ok(results);
        }

        [HttpPatch("results/{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateResult(int id, UpdateExamResultDto dto)
        {
            var result = await _service.UpdateResultAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("results/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _service.DeleteResultAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
