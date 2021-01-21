using Microsoft.EntityFrameworkCore;
using UsersIO.Business.Interfaces;
using UsersIO.Business.Models;
using UsersIO.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersIO.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ProjectDbContext context) : base(context) { }

        public async Task<User> FindUserAdresses(Guid id)
        {
            return await context.Users.AsNoTracking()
                .Include(a => a.Adresses)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> FindUserPhones(Guid id)
        {
            return await context.Users.AsNoTracking()
                .Include(a => a.Phones)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> FindUserSocialMidias(Guid id)
        {
            return await context.Users.AsNoTracking()
                .Include(a => a.SocialMidias)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> FindUserComplete(Guid id)
        {
            return await context.Users.AsNoTracking()
                .Include(a => a.Adresses)
                .Include(a => a.Phones)
                .Include(a => a.SocialMidias)
                .FirstOrDefaultAsync(e => e.Id == id);

        }
        public async Task<List<User>> FindUsersComplete()
        {
            return await context.Users.AsNoTracking()
                .Include(a => a.Adresses)
                .Include(a => a.Phones)
                .Include(a => a.SocialMidias).ToListAsync();
        }
    }
}
