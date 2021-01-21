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
    public class PhoneRepository : Repository<Phone>, IPhoneRepository
    {
        public PhoneRepository(ProjectDbContext context) : base(context) { }

        public async Task<List<Phone>> FindPhonesByUser(Guid userId)
        {
            return await context.Phones.AsNoTracking().Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
