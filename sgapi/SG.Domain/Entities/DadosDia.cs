using SG.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SG.Domain.Entities
{
    public class DadosDia
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Data { get; set; }
        public TimeSpan? SaidaHotel { get; set; }
        public TimeSpan? EntraFabrica { get; set; }
        public TimeSpan? SaidaAlmo { get; set; }
        public TimeSpan? RetorAlmo { get; set; }
        public TimeSpan? SaidaLanche { get; set; }
        public TimeSpan? RetorLanche { get; set; }
        public TimeSpan? SaidaFabrica { get; set; }
        public TimeSpan? ChegaHotel { get; set; }
        public string AtvDia { get; set; }

        public bool? Interno { get; set; }
        public TimeSpan? HorasInterno { get; set; }
        

        public int ProjetosId { get; set; }
        public virtual Projetos Projetos { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
