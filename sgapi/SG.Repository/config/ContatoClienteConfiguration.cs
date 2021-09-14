using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SG.Domain.Entities;

namespace SG.Repository.config
{
    class ContatoClienteConfiguration : IEntityTypeConfiguration<ContatoCliente>
    {
        public void Configure(EntityTypeBuilder<ContatoCliente> builder)
        {
            builder.HasKey(cc => cc.Id);
            builder.Property(cc => cc.Nome).HasMaxLength(45).IsRequired();
            builder.Property(cc => cc.EmailPrinc).HasMaxLength(65).IsRequired();
            builder.Property(cc => cc.EmailSec).HasMaxLength(65);
            builder.Property(cc => cc.FonePrinc).HasMaxLength(16).IsRequired();
            builder.Property(cc => cc.FoneSecun).HasMaxLength(16);
        }
    }
}

