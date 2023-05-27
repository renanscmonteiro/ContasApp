using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Messages.Models
{
    /// <summary>
    /// Modelo de dados para o serviço de envio de email
    /// </summary>
    public class EmailMessageModel
    {
        public string? EmailDestinatario { get; set; }
        public string? Assunto { get; set; }
        public string? Corpo { get; set; }
    }
}