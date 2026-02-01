using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class CartaoMap : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            builder.ToTable("tblCartao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .IsRequired();

            builder.Property(x => x.IdUsuario)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();

            builder
            .HasOne(c => c.Usuario)
            .WithMany(u => u.Cartoes)
            .HasForeignKey(c => c.IdUsuario);

            builder
            .HasMany(c => c.Faturas)
            .WithOne(f => f.Cartao);

            builder
            .HasOne(c => c.Banco)
            .WithMany(b => b.Cartoes)
            .HasForeignKey(c => c.IdBanco);
        }
    }
}
