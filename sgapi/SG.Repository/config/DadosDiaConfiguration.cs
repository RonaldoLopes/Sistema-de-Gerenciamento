using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SG.Domain.Entities;

namespace SG.Repository.config
{
    class DadosDiaConfiguration : IEntityTypeConfiguration<DadosDia>
    {
        public void Configure(EntityTypeBuilder<DadosDia> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Data).IsRequired();
        }
    }
}
