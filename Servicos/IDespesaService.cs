using Agro.API.Entidades;

public interface IDespesaService
{
    Task<IEnumerable<Despesa>> GetAllAsync();
    Task<Despesa> GetByIdAsync(Guid id);
    Task<Despesa> CreateAsync(Despesa despesa);
    Task<bool> UpdateAsync(Guid id, Despesa despesa);
    Task<bool> DeleteAsync(Guid id);
}