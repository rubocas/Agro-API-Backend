using Agro.API.Entidades;
using Agro.Dados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Agro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class LogsController : ControllerBase
    {
        private readonly Contexto _context;

        public LogsController(Contexto context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista os logs do sistema com paginação e filtros opcionais.
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Quantidade de registros por página (padrão: 20)</param>
        /// <param name="level">Filtro por nível de log (opcional)</param>
        /// <param name="startDate">Data inicial (opcional)</param>
        /// <param name="endDate">Data final (opcional)</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogEvento>>> GetLogs(
            int pageNumber = 1,
            int pageSize = 20,
            string? level = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var query = _context.LogEventos.AsQueryable();

            // Filtro por nível (INFO, WARNING, ERROR, etc.)
            if (!string.IsNullOrEmpty(level))
                query = query.Where(l => l.Level == level);

            // Filtro por data inicial
            if (startDate.HasValue)
                query = query.Where(l => l.TimeStamp >= startDate.Value);

            // Filtro por data final
            if (endDate.HasValue)
                query = query.Where(l => l.TimeStamp <= endDate.Value);

            // Ordena por data decrescente
            query = query.OrderByDescending(l => l.TimeStamp);

            // Paginação
            var totalRegistros = await query.CountAsync();
            var logs = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Retorna com metadados de paginação
            return Ok(new
            {
                TotalRegistros = totalRegistros,
                PaginaAtual = pageNumber,
                TamanhoPagina = pageSize,
                Logs = logs
            });
        }
    }
}