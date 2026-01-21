using Agro.API.Entidades;
using Agro.Dados;
using Microsoft.EntityFrameworkCore;

public class DespesaService : IDespesaService
{
    private readonly Contexto _context;

    public DespesaService(Contexto context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Despesa>> GetAllAsync()
    {
        return await _context.Despesas
            .Include(d => d.AnoSafra)
            .Include(d => d.DespesaCategoria)
            .ToListAsync();
    }

    public async Task<Despesa> GetByIdAsync(Guid id)
    {
        return await _context.Despesas
            .Include(d => d.AnoSafra)
            .Include(d => d.DespesaCategoria)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Despesa> CreateAsync(Despesa despesa)
    {
        _context.Despesas.Add(despesa);
        await _context.SaveChangesAsync();
        return despesa;
    }

    public async Task<bool> UpdateAsync(Guid id, Despesa despesa)
    {
        var existente = await _context.Despesas.FindAsync(id);
        if (existente == null) return false;

        existente.Descricao = despesa.Descricao;
        existente.DataRegistro = despesa.DataRegistro;
        existente.Valor = despesa.Valor;
        existente.AnoSafraId = despesa.AnoSafraId;
        existente.DespesaCategoriaId = despesa.DespesaCategoriaId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existente = await _context.Despesas.FindAsync(id);
        if (existente == null) return false;

        _context.Despesas.Remove(existente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Despesa>> GetByDataAtualAsync()
    {
        var startOfDay = DateTime.Today;
        var startOfNextDay = startOfDay.AddDays(1);

        return await _context.Despesas
            .Include(d => d.AnoSafra)
            .Include(d => d.DespesaCategoria)
            .Where(d => d.DataRegistro >= startOfDay && d.DataRegistro < startOfNextDay)
            .ToListAsync();
    }

    public async Task<IEnumerable<Despesa>> GetByFiltroAsync(DateTime dataInicio, DateTime dataFim, Guid? categoriaId, Guid? anoSafraId)
    {
        var start = dataInicio.Date;
        var endExclusive = dataFim.Date.AddDays(1);

        var query = _context.Despesas
            .Include(d => d.AnoSafra)
            .Include(d => d.DespesaCategoria)
            .Where(d => d.DataRegistro >= start && d.DataRegistro < endExclusive);

        if (categoriaId.HasValue)
        {
            query = query.Where(d => d.DespesaCategoriaId == categoriaId.Value);
        }

        if (anoSafraId.HasValue)
        {
            query = query.Where(d => d.AnoSafraId == anoSafraId.Value);
        }

        return await query.ToListAsync();
    }
}
