using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SG.WebApi.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Setor { get; set; }
        public string Funcao { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FullName { get; set; }

        public string Role { get; set; }
    }
}
