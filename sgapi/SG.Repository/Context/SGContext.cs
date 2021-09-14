using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SG.Domain.Entities;
using SG.Domain.Identity;
using SG.Repository.config;

namespace SG.Repository.Context
{
    public class SGContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                                    UserRole, IdentityUserLogin<int>,
                                                    IdentityRoleClaim<int>,
                                                    IdentityUserToken<int>>
    {
        public SGContext(DbContextOptions<SGContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ContatoCliente> ContatoClientes { get; set; }
        public DbSet<Projetos> Projetos { get; set; }
        public DbSet<DadosDia> DadosDias { get; set; }
        public DbSet<CadernoHoras> CadernoHoras { get; set; }
        public DbSet<PontoExterno> PontoExternos { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new ContatoClienteConfiguration());
            modelBuilder.ApplyConfiguration(new ProjetosConfiguration());
            modelBuilder.ApplyConfiguration(new DadosDiaConfiguration());
            modelBuilder.ApplyConfiguration(new PontoExternoConfiguration());
            modelBuilder.ApplyConfiguration(new ProjetosConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

        }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));//desativa aviso sobre ASNoTracking
        }
    }
}
