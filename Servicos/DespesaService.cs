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
}
