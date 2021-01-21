using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsersIO.Api.Data
{
    public class TokenDbContext : IdentityDbContext
    {
        public TokenDbContext(DbContextOptions<TokenDbContext> options)
            : base(options)
        {
        }
    }
}
