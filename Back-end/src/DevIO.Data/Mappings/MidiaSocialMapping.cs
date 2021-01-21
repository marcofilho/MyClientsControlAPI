using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class MidiaSocialMapping : IEntityTypeConfiguration<MidiaSocial>
    {
        public void Configure(EntityTypeBuilder<MidiaSocial> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("Telefones");
        }
    }
}