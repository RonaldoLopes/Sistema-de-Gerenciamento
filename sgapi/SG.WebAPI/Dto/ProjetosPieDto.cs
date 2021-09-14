using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SG.WebApi.Dto
{
    public class ProjetosPieDto
    {
        public string name { get; set; }
        public int y { get; set; }

        public int[] data { get; set; }

        public int[] RPrevistos { get; set; }
        public int[] RUtilizados { get; set; }

        public int[] MPrevistos { get; set; }
        public int[] MUtilizados { get; set; }

        public int[] HPrevistasD { get; set; }
        public int[] HUtilizadasD { get; set; }

        public int[] HPrevistasI { get; set; }
        public int[] HUtilizadasI { get; set; }

    }
}
