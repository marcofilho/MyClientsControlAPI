using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersIO.Business.Models;

namespace UsersIO.Data.Mappings
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {

        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            //Primary Key
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Number).IsRequired().HasColumnType("varchar(50)");


            builder.ToTable("Phones");
        }
    }
}
