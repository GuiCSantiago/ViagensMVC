using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ViagensMVC.Models
{
    public class Item
    {
        public int Id { get; set; }

        public Veiculo Veiculo { get; set; }

        public int VeiculoId { get; set; }

        public string Descricao { get; set; }

        public Item()
        {
        }

        public Item(Veiculo veiculo, string descricao)
        {
            Veiculo = veiculo;
            Descricao = descricao;
        } 
    }
}
