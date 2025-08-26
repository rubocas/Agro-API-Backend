using Agro.API.Entidades;
using Agro.Dados;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;

namespace Agro.API.Servicos
{
    public class AnoSafraService : IAnoSafraService
    {
        private readonly Contexto _context;

        public AnoSafraService(Contexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnoSafra>> GetAllAsync()
        {
            return await _context.AnoSafras.AsNoTracking().OrderBy(n => n.AnoSafras).ToListAsync();
        }

        public async Task<AnoSafra?> GetByIdAsync(Guid id)
        {
            return await _context.AnoSafras.FindAsync(id);
        }

        public async Task<AnoSafra> CreateAsync(AnoSafra anoSafra)
        {
            anoSafra.Id = Guid.NewGuid();

            var existeCategoria = _context.AnoSafras.FirstOrDefault(c => c.AnoSafras == anoSafra.AnoSafras);
            if(existeCategoria != null)
            {
                return null;
            }

            _context.AnoSafras.Add(anoSafra);
            await _context.SaveChangesAsync();
            return anoSafra;
        }

        public async Task<bool> UpdateAsync(Guid id, AnoSafra anoSafra)
        {
            var existente = await _context.AnoSafras.FindAsync(id);
            if (existente == null) return false;

            existente.AnoSafras = anoSafra.AnoSafras;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existente = await _context.AnoSafras.FindAsync(id);
            if (existente == null) return false;

            _context.AnoSafras.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
