using System;

namespace ViagensMVC.Models
{
    public class ErrorViewModel
    {
        public string Mensagem { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string msg)
        {
            Mensagem = msg;
        }
    }
}
