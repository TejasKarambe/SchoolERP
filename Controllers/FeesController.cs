using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.DTOs.Fees;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/fees")]
    [Authorize]
    public class FeesController : ControllerBase
    {
        private readonly IFeeService _service;

        public FeesController(IFeeService service)
        {
            _service = service;
        }

        // ── Fee Structures ─────────────────────────────────────────────────────

        [HttpGet("structures")]
        public async Task<IActionResult> GetAllStructures()
        {
            return Ok(await _service.GetAllStructuresAsync());
        }

        [HttpGet("structures/{id}")]
        public async Task<IActionResult> GetStructureById(int id)
        {
            var data = await _service.GetStructureByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpGet("structures/program/{programId}")]
        public async Task<IActionResult> GetStructuresByProgram(int programId)
        {
            return Ok(await _service.GetStructuresByProgramAsync(programId));
        }

        [HttpPost("structures")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStructure(CreateFeeStructureDto dto)
        {
            var result = await _service.CreateStructureAsync(dto);
            return CreatedAtAction(nameof(GetStructureById), new { id = result.Id }, result);
        }

        [HttpPatch("structures/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStructure(int id, UpdateFeeStructureDto dto)
        {
            var result = await _service.UpdateStructureAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("structures/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStructure(int id)
        {
            var result = await _service.DeleteStructureAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // ── Fee Payments ──────────────────────────────────────────────────────

        [HttpGet("payments")]
        public async Task<IActionResult> GetAllPayments()
        {
            return Ok(await _service.GetAllPaymentsAsync());
        }

        [HttpGet("payments/{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var data = await _service.GetPaymentByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpGet("payments/student/{studentId}")]
        public async Task<IActionResult> GetPaymentsByStudent(int studentId)
        {
            return Ok(await _service.GetPaymentsByStudentAsync(studentId));
        }

        [HttpGet("payments/pending")]
        public async Task<IActionResult> GetPendingPayments()
        {
            return Ok(await _service.GetPendingPaymentsAsync());
        }

        [HttpPost("payments")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePayment(CreateFeePaymentDto dto)
        {
            var result = await _service.CreatePaymentAsync(dto);
            return CreatedAtAction(nameof(GetPaymentById), new { id = result.Id }, result);
        }

        [HttpPatch("payments/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePayment(int id, UpdateFeePaymentDto dto)
        {
            var result = await _service.UpdatePaymentAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("payments/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _service.DeletePaymentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
