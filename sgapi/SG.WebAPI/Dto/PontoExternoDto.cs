using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.WebApi.Dto
{
    public class PontoExternoDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Entrada { get; set; }
        public TimeSpan SaidaAlmoco { get; set; }
        public TimeSpan RetAlmoco { get; set; }
        public TimeSpan Saida { get; set; }
        public string Justificativa { get; set; }
        public virtual ICollection<ProjetosPieDto> ProjetosDtos { get; set; }
    }
}
