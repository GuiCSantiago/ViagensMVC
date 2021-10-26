using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ViagensMVC.Models.Enum;

namespace ViagensMVC.Models
{
    public class Viagem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} campo obrigatório!")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "({0}) tamanho deve estar entre {1} e {2}")]
        public string Rota { get; set; } // Local de início - Destino final

        [Required(ErrorMessage = "{0} campo obrigatório!")]
        [Display(Name = "Inicio")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataHoraInicio { get ; set; }

        [Required(ErrorMessage = "{0} campo obrigatório!")]
        [Display(Name = "Término")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataHoraFim { get; set; }

        [Required(ErrorMessage = "{0} campo obrigatório!")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "{0} campo obrigatório!")]
        public PosicaoEnum Posicao { get; set; }

        public Veiculo Veiculo { get; set; }

        [Display(Name = "Veículo")]
        public int VeiculoId { get; set; }

        public Motorista Motorista { get; set; }

        [Display(Name = "Motorista")]
        public int MotoristaId { get; set; }

        public StatusEnum Status { get; set; }

        public Viagem()
        {
        }

        public Viagem(string rota, DateTime dataHoraInicio, DateTime dataHoraFim, double valor, PosicaoEnum posicao, Veiculo veiculo, Motorista motorista, StatusEnum status)
        {
            Rota = rota;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Valor = valor;
            Posicao = posicao;
            Veiculo = veiculo;
            Motorista = motorista;
            Status = status;
        }

        public void UpdateStatus()
        {
            if (DataHoraFim < DateTime.Now)
                Status = StatusEnum.Encerrada;
            else if (DataHoraInicio > DateTime.Now)
                Status = StatusEnum.Pendente;
            else if (DataHoraInicio < DateTime.Now && DataHoraFim > DateTime.Now)
                Status = StatusEnum.Andamento;
        }
    }
}
