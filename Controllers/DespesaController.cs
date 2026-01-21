using Agro.API.Entidades;
using Agro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agro.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaService _service;

        public DespesaController(IDespesaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var despesas = await _service.GetAllAsync();
            return Ok(despesas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var despesa = await _service.GetByIdAsync(id);
            if (despesa == null) return NotFound();
            return Ok(despesa);
        }

        [HttpGet("data-atual")]
        public async Task<IActionResult> GetByDataAtual()
        {
            var despesas = await _service.GetByDataAtualAsync();
            return Ok(despesas);
        }

        [HttpGet("filtro")]
        public async Task<IActionResult> GetByFiltro(
            [FromQuery] DateTime dataInicio,
            [FromQuery] DateTime dataFim,
            [FromQuery] Guid? categoriaId,
            [FromQuery] Guid? anoSafraId)
        {
            if (dataInicio > dataFim)
            {
                return BadRequest("A data de início não pode ser maior que a data de fim.");
            }

            var despesas = await _service.GetByFiltroAsync(dataInicio, dataFim, categoriaId, anoSafraId);
            return Ok(despesas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DespesaDto despesaDTO)
        {
            var despesaDto = new DespesaDto();
            var despesa = despesaDto.ConverterDespesaDTOEmDespesa(despesaDTO);
            var created = await _service.CreateAsync(despesa);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Despesa despesa)
        {
            var updated = await _service.UpdateAsync(id, despesa);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}