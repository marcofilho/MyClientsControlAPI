using Microsoft.EntityFrameworkCore;
using UsersIO.Data.Context;
using System;
using System.Threading.Tasks;
using UsersIO.Business.Models;
using UsersIO.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UsersIO.Data.Repositories
{
    public class SocialMidiaRepository : Repository<SocialMidia>, ISocialMidiaRepository
    {
        public SocialMidiaRepository(ProjectDbContext context) : base(context) { }

        public async Task<List<SocialMidia>> FindSocialMidiasByUser(Guid userId)
        {
            return await context.SocialMidias.AsNoTracking().Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
