using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersIO.Business.Models;

namespace UsersIO.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {

        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //Primary Key
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Neighborhood).IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Cep).IsRequired().HasColumnType("varchar(8)");
            builder.Property(e => e.City).IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.Complement).IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.State).IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.PublicPlace).IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.Number).IsRequired().HasColumnType("varchar(50)");


            builder.ToTable("Adresses");
        }
    }
}
