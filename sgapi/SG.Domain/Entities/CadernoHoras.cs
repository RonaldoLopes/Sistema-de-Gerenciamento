using SG.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SG.Domain.Entities
{
    public class CadernoHoras
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Data { get; set; }
        public TimeSpan? HorasDia { get; set; }
        public TimeSpan? Deslocamento { get; set; }
        public TimeSpan? HorasTrab { get; set; }
        public string AtvDia { get; set; }

        public int ProjetosId { get; set; }
        public virtual Projetos Projetos { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
