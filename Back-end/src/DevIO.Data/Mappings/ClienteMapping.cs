using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Rg)
                .IsRequired()
                .HasColumnType("varchar(7)");

            builder.Property(p => p.Cpf)
               .IsRequired()
               .HasColumnType("varchar(11)");

            builder.Property(p => p.DataNascimento)
               .IsRequired()
               .HasColumnType("datetime");

            // 1 : N => Cliente : Endereco
            builder.HasMany(f => f.Enderecos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(p => p.ClienteId);

            // 1 : N => Cliente : Telefone
            builder.HasMany(f => f.Telefones)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);

            // 1 : N => Cliente : MidiaSocial
            builder.HasMany(f => f.MidiasSociais)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);

            builder.ToTable("Clientes");
        }
    }
}