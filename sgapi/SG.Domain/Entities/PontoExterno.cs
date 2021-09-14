using SG.Domain.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG.Domain.Entities
{
    public class PontoExterno
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Data { get; set; }
        public TimeSpan? EntraFabrica { get; set; }
        public TimeSpan? SaidaAlmo { get; set; }
        public TimeSpan? RetorAlmo { get; set; }
        public TimeSpan? SaidaFabrica { get; set; }
        public string AtvDia { get; set; }

        public int ProjetosId { get; set; }
        public virtual Projetos Projetos { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
