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
    public class ViagemService
    {
        private readonly ViagensMVCContext _context;

        public ViagemService(ViagensMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Viagem>> FindAllByDateAsync(DateTime? dia)
        {
            //DateTime diaSemHora = dia.Value.Date;
            return await _context.Viagens.Where(v => v.DataHoraInicio.Date.Equals(dia.Value.Date)).ToListAsync();
        }

        public async Task<List<Viagem>> FindAllAsync()
        {
            return await _context.Viagens.Include(obj => obj.Veiculo).Include(obj => obj.Motorista).ToListAsync();
        }

        public async Task InsertAsync(Viagem obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Viagem> FindByIdAsync(int? id)
        {
            return await _context.Viagens.Include(obj => obj.Veiculo).Include(obj => obj.Motorista).FirstOrDefaultAsync(obj => obj.Id == id.Value);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Viagens.FindAsync(id);
                _context.Viagens.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Viagem obj)
        {
            if ((!await _context.Viagens.AnyAsync(x => x.Id == obj.Id)))
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
