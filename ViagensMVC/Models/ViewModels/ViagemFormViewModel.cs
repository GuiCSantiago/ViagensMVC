using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViagensMVC.Models.ViewModels
{
    public class ViagemFormViewModel
    {
        public Viagem Viagem { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; }

        public ICollection<Motorista> Motoristas { get; set; }

    }
}
