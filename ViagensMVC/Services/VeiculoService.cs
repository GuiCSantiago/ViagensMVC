using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViagensMVC.Data;
using ViagensMVC.Models;
using ViagensMVC.Services.Exceptions;

namespace ViagensMVC.Services
{
    public class VeiculoService
    {
        private readonly ViagensMVCContext _context;

        public VeiculoService(ViagensMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Veiculo>> FindAllAsync()
        {
            return await _context.Veiculos.Include(obj => obj.ItensAdicionais).ToListAsync();
        }

        public async Task InsertAsync(Veiculo obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Veiculo> FindByIdAsync(int id)
        {
            return await _context.Veiculos.Include(obj => obj.ItensAdicionais).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Veiculos.FindAsync(id);
                _context.Veiculos.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Veiculo obj)
        {
            if ((!await _context.Veiculos.AnyAsync(x => x.Id == obj.Id)))
                throw new NotFoundException("Id não encontrado");
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
