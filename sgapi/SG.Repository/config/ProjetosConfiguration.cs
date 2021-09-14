using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SG.Domain.Entities;

namespace SG.Repository.config
{
    class ProjetosConfiguration : IEntityTypeConfiguration<Projetos>
    {
        public void Configure(EntityTypeBuilder<Projetos> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CodProjeto).IsRequired().HasMaxLength(5);
            builder.Property(p => p.Descricao).IsRequired(false).HasMaxLength(100);
            builder.Property(p => p.RecursosPrev).IsRequired();
            builder.Property(p => p.RecursosUtil).IsRequired();
            builder.Property(p => p.MobilizaPrev).IsRequired();
            builder.Property(p => p.DataInicio).IsRequired();
            builder.Property(p => p.HorasPrevDesen).IsRequired();
            builder.Property(p => p.HorasUtilDesenv).IsRequired();
            builder.Property(p => p.HorasPrevImplement).IsRequired();
            builder.Property(p => p.HorasUtilImplement).IsRequired();
            builder.Property(p => p.Concluido).HasDefaultValue(false);
        }

        
    }
}