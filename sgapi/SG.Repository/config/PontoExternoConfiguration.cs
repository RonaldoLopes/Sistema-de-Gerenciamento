using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SG.Domain.Entities;
namespace SG.Repository.config
{
    class PontoExternoConfiguration : IEntityTypeConfiguration<PontoExterno>
    {
        public void Configure(EntityTypeBuilder<PontoExterno> builder)
        {
            builder.HasKey(pe => pe.Id);
            builder.Property(pe => pe.Data).IsRequired();
            builder.Property(pe => pe.AtvDia).HasMaxLength(255).IsRequired();
        }
    }
}
