using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViagensMVC.Models.ViewModels
{
    public class MotoristaRotaViewModel
    {
        public IEnumerable<Motorista> Motoristas { get; set; } = new List<Motorista>();

        public string Rota { get; set; }
    }
}
