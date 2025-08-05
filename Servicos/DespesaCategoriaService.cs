using Agro.API.Entidades;
using Agro.Dados;
using Microsoft.EntityFrameworkCore;
using System;

namespace Agro.API.Servicos
{
    public class DespesaCategoriaService : IDespesaCategoriaService
    {
        private readonly Contexto _context;

        public DespesaCategoriaService(Contexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DespesaCategoria>> GetAllAsync()
        {
            return await _context.DespesasCategorias.AsNoTracking().ToListAsync();
        }

        public async Task<DespesaCategoria?> GetByIdAsync(Guid id)
        {
            return await _context.DespesasCategorias.FindAsync(id);
        }

        public async Task<DespesaCategoria> CreateAsync(DespesaCategoria categoria)
        {
            categoria.Id = Guid.NewGuid();
            _context.DespesasCategorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> UpdateAsync(Guid id, DespesaCategoria categoria)
        {
            var existente = await _context.DespesasCategorias.FindAsync(id);
            if (existente == null) return false;

            existente.Nome = categoria.Nome;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existente = await _context.DespesasCategorias.FindAsync(id);
            if (existente == null) return false;

            _context.DespesasCategorias.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
