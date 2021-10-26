using System;
using System.Collections.Generic;
using System.Linq;
using ViagensMVC.Models;
using ViagensMVC.Models.Enum;

namespace ViagensMVC.Data
{
    public class SeedingService
    {
        private ViagensMVCContext _context;

        public SeedingService(ViagensMVCContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Viagens.Any() || _context.Motoristas.Any() || _context.Veiculos.Any())
                return; //DB has been seeded

            var v1 = new Veiculo("Ford", "Padrão" );
            var v2 = new Veiculo("Mercedes",  "Elétrico");
            var v3 = new Veiculo("Volkswagen", "Padrão");
            var v4 = new Veiculo("Mercedes", "Padrão");

            var itens1 = new List<Item>();
            itens1.Add(new Item(v2, "Wifi"));
            itens1.Add(new Item(v2, "Tv"));
            itens1.Add(new Item(v2, "Ar condicionado"));
            itens1.Add(new Item(v2, "Carregador USB"));
            v2.ItensAdicionais = itens1;

            var itens2 = new List<Item>();
            itens2.Add(new Item(v4, "Luz de leitura"));
            itens2.Add(new Item(v4, "Fone de ouvido"));
            v4.ItensAdicionais = itens2;
            
            var m1 = new Motorista("João Costa");
            var m2 = new Motorista("Maria Fernanda");
            var m3 = new Motorista("Alexandre Gomes");
            var m4 = new Motorista("Matheus Souza");
            var m5 = new Motorista("Daniel Santos");
            var m6 = new Motorista("Carlos Henrique");
                                        
            var viagem1 = new Viagem("SBC - SP", new DateTime(2021, 09, 01, 18, 0, 0), new DateTime(2021, 09, 01, 20, 0, 0), 20.00, PosicaoEnum.Embarque, v1, m1, StatusEnum.Andamento);
            var viagem2 = new Viagem("SP - MG", new DateTime(2021, 09, 11, 10, 0, 0), new DateTime(2021, 09, 11, 21, 0, 0), 165.50, PosicaoEnum.Transito, v2, m2, StatusEnum.Pendente);
            var viagem3 = new Viagem("RJ - SP", new DateTime(2021, 10, 07, 22, 30, 0), new DateTime(2021, 10, 08, 1, 55, 0), 20.00, PosicaoEnum.Destino, v3, m3, StatusEnum.Encerrada);

            _context.Veiculos.AddRange(v1, v2, v3, v4);

            _context.Motoristas.AddRange(m1, m2, m3, m4, m5, m6);

            _context.Viagens.AddRange(viagem1, viagem2, viagem3);

            _context.SaveChanges();
        }
    }
}
