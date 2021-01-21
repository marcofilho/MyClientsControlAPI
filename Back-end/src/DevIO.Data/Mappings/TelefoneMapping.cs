using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class TelefoneMapping : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Numero)
                .IsRequired()
                .HasColumnType("varchar(14)");
            
            builder.ToTable("Telefones");
        }
    }
}