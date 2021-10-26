using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ViagensMVC.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} campo obrigatório!")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "{0} campo obrigatório!")]
        public string Modelo { get; set; }

        public ICollection<Item> ItensAdicionais { get; set; } = new List<Item>();

        public Veiculo()
        {
        }

        public Veiculo(string marca, string modelo)
        {
            Marca = marca;
            Modelo = modelo;
        }

        public void AddItem(Item item)
        {
            ItensAdicionais.Add(item);
        }
    }
}
