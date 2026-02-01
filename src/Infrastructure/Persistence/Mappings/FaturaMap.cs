using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class FaturaMap : IEntityTypeConfiguration<Fatura>
    {
        public void Configure(EntityTypeBuilder<Fatura> builder)
        {
            builder.ToTable("tblFatura");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IdCartao)
                .IsRequired();

            builder.Property(x => x.MesAno)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();

            builder
                .HasOne(f => f.Cartao)
                .WithMany(c => c.Faturas)
                .HasForeignKey(f => f.IdCartao);
        }
    }
}
