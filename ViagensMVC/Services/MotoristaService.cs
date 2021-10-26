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
    public class MotoristaService
    {
        private readonly ViagensMVCContext _context;

        public MotoristaService(ViagensMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Motorista>> FindAllAsync()
        {
            return await _context.Motoristas.ToListAsync();
        }

        public async Task InsertAsync(Motorista obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Motorista> FindByIdAsync(int id)
        {
            return await _context.Motoristas.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Motoristas.FindAsync(id);
                _context.Motoristas.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Motorista obj)
        {
            if ((!await _context.Motoristas.AnyAsync(x => x.Id == obj.Id)))
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
