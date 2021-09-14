using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SG.Domain.Entities;

namespace SG.Repository.config
{
    class CadernoHorasConfiguration : IEntityTypeConfiguration<CadernoHoras>
    {
        public void Configure(EntityTypeBuilder<CadernoHoras> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Data).IsRequired();
            builder.Property(c => c.HorasDia).IsRequired();
            builder.Property(c => c.Deslocamento).IsRequired();
            builder.Property(c => c.HorasTrab).IsRequired();
            builder.Property(c => c.AtvDia).IsRequired(false);
        }
    }
}