using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG.WebApi.Dto
{
    public class UpdateUserRoleDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Delete { get; set; }
        public string RoleNew { get; set; }
    }
}
