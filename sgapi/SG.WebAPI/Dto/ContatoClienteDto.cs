using System;
using System.Collections.Generic;

namespace SG.WebApi.Dto
{
    public class ContatoClienteDto
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string EmailPrinc { get; set; }
        public string EmailSec { get; set; }
        public string FonePrinc { get; set; }
        public string FoneSecun { get; set; }
        public List<ClienteDto> ClientesDTO { get; set; }

    }
}
