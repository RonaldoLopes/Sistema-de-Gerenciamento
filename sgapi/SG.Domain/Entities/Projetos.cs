using SG.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SG.Domain.Entities
{
    public class Projetos
    {
        public int Id { get; set; }

        public string CodProjeto { get; set; }
        public string Descricao { get; set; }

        public int RecursosPrev { get; set; }

        public int RecursosUtil { get; set; }

        public int MobilizaPrev { get; set; }

        public int MobilizaUtili { get; set; }

        public DateTime DataInicio { get; set; }

        public int HorasPrevDesen { get; set; }

        public int HorasUtilDesenv { get; set; }

        public int HorasPrevImplement { get; set; }

        public int HorasUtilImplement { get; set; }

        public bool? Concluido { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? DataConclusao { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        
    }
}
