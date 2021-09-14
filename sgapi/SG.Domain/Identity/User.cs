using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "varchar(150)")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Setor { get; set; }

        [Column(TypeName = "varchar(80)")]
        public string Funcao { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
    }
}
