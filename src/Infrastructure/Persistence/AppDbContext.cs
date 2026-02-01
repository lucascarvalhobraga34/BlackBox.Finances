using Domain.Entities;
using Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<LancamentoFatura> Lancamentos => Set<LancamentoFatura>();
        public DbSet<Cartao> Cartoes => Set<Cartao>();
        public DbSet<Fatura> Faturas => Set<Fatura>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LancamentoFaturaMap());
            modelBuilder.ApplyConfiguration(new CartaoMap());
            modelBuilder.ApplyConfiguration(new FaturaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new BancoMap());
        }
    }
}
