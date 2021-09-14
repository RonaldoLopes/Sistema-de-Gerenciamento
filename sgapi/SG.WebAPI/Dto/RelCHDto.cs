using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.WebApi.Dto
{
    public class RelCHDto
    {
        public DateTime Data { get; set; }
        public TimeSpan? HorasDia { get; set; }
        public TimeSpan? Deslocamento { get; set; }
        public TimeSpan? HorasTrab { get; set; }
        public string CodProjeto { get; set; }
        public int RecursosUtil { get; set; }
        public int MobilizaUtil { get; set; }
        public string AtvDia { get; set; }
        public int HorasPrevImplement { get; set; }
        public int HorasPrevDesenv { get; set; }
        public string UserName { get; set; }
    }
}
