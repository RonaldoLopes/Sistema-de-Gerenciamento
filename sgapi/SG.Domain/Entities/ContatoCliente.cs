using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SG.Domain.Entities
{
    public class ContatoCliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string EmailPrinc { get; set; }

        public string EmailSec { get; set; }

        public string FonePrinc { get; set; }
        public string FoneSecun { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Clientes { get; private set; }

    }
}
