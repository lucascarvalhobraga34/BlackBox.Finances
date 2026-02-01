using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class BancoMap : IEntityTypeConfiguration<Banco>
    {
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.ToTable("tblBanco");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Codigo)
               .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();

            builder
            .HasMany(b => b.Cartoes)
            .WithOne(c => c.Banco);
        }
    }
}
