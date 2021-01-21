using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersIO.Business.Models;

namespace UsersIO.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Primary Key
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).IsRequired().HasColumnType("varchar(100)");
            builder.Property(a => a.Cpf).IsRequired().HasColumnType("varchar(11)");
            builder.Property(a => a.BirthdayDate).IsRequired().HasColumnType("datetime");
            builder.Property(a => a.Rg).IsRequired().HasColumnType("varchar(7)");

            //Config Relationship 1 : N => User : Address
            builder.HasMany(a => a.Adresses).WithOne(e => e.User);

            //Config Relationship 1 : N => User : Phones
            builder.HasMany(a => a.Phones).WithOne(e => e.User);

            //Config Relationship 1 : N => User : SocialMidias
            builder.HasMany(a => a.SocialMidias).WithOne(e => e.User);

            builder.ToTable("Users");
        }
    }
}
