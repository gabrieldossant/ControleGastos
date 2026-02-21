using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<PessoaModel> pessoas { get; set; }
        public DbSet<TransacaoModel> transacoes { get; set; }
        public DbSet<CategoriaModel> categorias { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PessoaModel>()
                .HasMany(p => p.Transacoes)
                .WithOne(t => t.Pessoa)
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoriaModel>()
                .HasMany(c => c.Transacoes)
                .WithOne(t => t.Categoria)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
