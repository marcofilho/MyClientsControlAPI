using Microsoft.EntityFrameworkCore;
using UsersIO.Business.Models;
using System.Linq;

namespace UsersIO.Data.Context
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<SocialMidia> SocialMidias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Set a limit data type string, if wasn't configurated before - Para o caso de não definirmos o tamanho de uma coluna string, que seja definida aqui
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.Relational().ColumnType = "varchar(100)";
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectDbContext).Assembly);

            //Desativate cascade delete relationship - Para o caso de um amigo ser excluído, que os jogos emprestados para ele não sejam excluídos também
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
