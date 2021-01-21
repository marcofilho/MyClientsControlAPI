using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersIO.Business.Models;

namespace UsersIO.Data.Mappings
{
    public class SocialMidiaMapping : IEntityTypeConfiguration<SocialMidia>
    {

        public void Configure(EntityTypeBuilder<SocialMidia> builder)
        {
            //Primary Key
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Value).IsRequired().HasColumnType("varchar(100)");

            builder.ToTable("SocialMidias");
        }
    }
}
