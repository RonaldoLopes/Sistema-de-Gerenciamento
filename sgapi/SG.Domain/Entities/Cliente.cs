using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SG.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Clientes { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Numero { get; set; }
        public string Area { get; set; }

        public virtual ICollection<ContatoCliente> ContatoClientes { get; set; }
    }
}
