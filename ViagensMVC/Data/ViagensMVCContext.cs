using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViagensMVC.Models;

namespace ViagensMVC.Data
{
    public class ViagensMVCContext : DbContext
    {
        public ViagensMVCContext(DbContextOptions<ViagensMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Viagem> Viagens { get; set; }

        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Motorista> Motoristas { get; set; }

        public DbSet<Item> Itens { get; set; }
    }
}
