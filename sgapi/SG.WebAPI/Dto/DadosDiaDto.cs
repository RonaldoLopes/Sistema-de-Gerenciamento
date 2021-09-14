using System;
using System.Collections.Generic;

namespace SG.WebApi.Dto
{
    public class DadosDiaDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan SaidaHotel { get; set; }
        public TimeSpan EntraFabrica { get; set; }
        public TimeSpan SaidaAlmo { get; set; }
        public TimeSpan SaidaLanche { get; set; }
        public TimeSpan RetorLanche { get; set; }
        public TimeSpan SaidaFabrica { get; set; }
        public TimeSpan ChegaHotel { get; set; }
        public string AtvDia { get; set; }

        public virtual ICollection<ProjetosPieDto> ProjetosDto { get; set; }
    }
}
