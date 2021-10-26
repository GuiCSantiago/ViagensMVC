using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViagensMVC.Models.ViewModels
{
    public class ViagemMotoristaViewModel
    {
        public ICollection<Motorista> Motoristas { get; set; }

        public Viagem Viagem { get; set; }
    }
}
