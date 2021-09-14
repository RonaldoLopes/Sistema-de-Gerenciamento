using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.WebApi.Dto
{
    public class CadernoHorasDto
    {

        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HorasDia { get; set; }
        public TimeSpan Deslocamento { get; set; }
        public TimeSpan HorasTrab { get; set; }

        public virtual ICollection<ProjetosPieDto> ProjetosDto { get; set; }
    }
}
