using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class LancamentoFaturaMap : IEntityTypeConfiguration<LancamentoFatura>
    {
        public void Configure(EntityTypeBuilder<LancamentoFatura> builder)
        {
            builder.ToTable("tblFaturaLancamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IdFatura)
                .IsRequired();

            builder.Property(x => x.DataLancamento)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.NumeroParcela)
                .IsRequired();

            builder.Property(x => x.TotalParcelas)
                .IsRequired();

            builder
                .HasOne(l => l.Fatura)
                .WithMany(f => f.Lancamentos)
                .HasForeignKey(l => l.IdFatura);
        }
    }
}
