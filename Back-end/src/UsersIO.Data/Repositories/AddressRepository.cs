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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ProjectDbContext context) : base(context) { }

        public async Task<List<Address>> FindAdressesByUser(Guid userId)
        {
            return await context.Adresses.AsNoTracking().Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
