using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ViagensMVC.Models
{
    public class Motorista
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} campo obrigatório!")]
        [Display(Name = "Motorista")]
        public string Nome { get; set; }

        public Motorista()
        {
        }

        public Motorista(string nome)
        {
            Nome = nome;
        }
    }
}
