using Agro.API.Entidades;

namespace Agro.API.Servicos
{
    public interface IAnoSafraService
    {
        Task<IEnumerable<AnoSafra>> GetAllAsync();
        Task<AnoSafra?> GetByIdAsync(Guid id);
        Task<AnoSafra> CreateAsync(AnoSafra anoSafra);
        Task<bool> UpdateAsync(Guid id, AnoSafra anoSafra);
        Task<bool> DeleteAsync(Guid id);
    }
}
