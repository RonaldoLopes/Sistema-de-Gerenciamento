using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SG.Domain.Entities;

namespace SG.Repository.config
{
    class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Clientes).HasMaxLength(45).IsRequired();
            builder.Property(c => c.Cep).HasMaxLength(20);
            builder.Property(c => c.Endereco).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Bairro).HasMaxLength(45);
            builder.Property(c => c.Cidade).HasMaxLength(45).IsRequired();
            builder.Property(c => c.Estado).HasMaxLength(2).IsRequired();
            builder.Property(c => c.Numero).HasMaxLength(10);
            builder.Property(c => c.Area).HasMaxLength(45).IsRequired();
        }
    }
}
