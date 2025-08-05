using Agro.API.Entidades;

namespace Agro.API.Servicos
{
    public interface IDespesaCategoriaService
    {
        Task<IEnumerable<DespesaCategoria>> GetAllAsync();
        Task<DespesaCategoria?> GetByIdAsync(Guid id);
        Task<DespesaCategoria> CreateAsync(DespesaCategoria categoria);
        Task<bool> UpdateAsync(Guid id, DespesaCategoria categoria);
        Task<bool> DeleteAsync(Guid id);
    }
}
