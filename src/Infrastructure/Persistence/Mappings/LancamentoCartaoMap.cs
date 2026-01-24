using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class LancamentoCartaoMap : IEntityTypeConfiguration<LancamentoCartao>
    {
        public void Configure(EntityTypeBuilder<LancamentoCartao> builder)
        {
            builder.ToTable("LancamentosCartao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Data)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.OwnsOne(x => x.Parcela, p =>
            {
                p.Property(x => x.Numero)
                    .HasColumnName("NumeroParcela");

                p.Property(x => x.Total)
                    .HasColumnName("TotalParcelas");
            });
        }
    }
}
