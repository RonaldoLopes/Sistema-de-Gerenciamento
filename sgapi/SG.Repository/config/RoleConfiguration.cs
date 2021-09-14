using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SG.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SG.Repository.config
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role () { Id = 1, Name = "Admin", NormalizedName = "ADMIN"},
                new Role() { Id = 2, Name = "Gestor", NormalizedName = "GESTOR" },
                new Role() { Id = 3, Name = "RH", NormalizedName = "RH" },
                new Role() { Id = 4, Name = "RUser", NormalizedName = "RUSER" },
                new Role() { Id = 5, Name = "Cliente", NormalizedName = "CLIENTE" }
                );
        }
    }
}
