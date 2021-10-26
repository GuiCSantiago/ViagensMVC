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
    public class ItensService
    {
        private readonly ViagensMVCContext _context;

        public ItensService(ViagensMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> FindAllAsync()
        {
            return await _context.Itens.ToListAsync();
        }

        public async Task InsertAsync(Item obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Item> FindByIdAsync(int id)
        {
            return await _context.Itens.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Itens.FindAsync(id);
                _context.Itens.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Item obj)
        {
            if ((!await _context.Itens.AnyAsync(x => x.Id == obj.Id)))
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
