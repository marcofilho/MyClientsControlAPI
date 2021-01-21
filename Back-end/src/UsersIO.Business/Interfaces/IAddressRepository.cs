using UsersIO.Business.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UsersIO.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<List<Address>> FindAdressesByUser(Guid amigoId);
    }
}
