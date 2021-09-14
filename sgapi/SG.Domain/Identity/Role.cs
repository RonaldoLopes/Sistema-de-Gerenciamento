using System.Collections.Generic;

namespace SG.Domain.Identity
{
    public class Role : Microsoft.AspNetCore.Identity.IdentityRole<int>
    {
        public virtual List<UserRole> UserRoles { get; set; }
    }
}
