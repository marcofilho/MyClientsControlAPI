using UsersIO.Business.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UsersIO.Business.Interfaces
{
    public interface IPhoneRepository : IRepository<Phone>
    {
        Task<List<Phone>> FindPhonesByUser(Guid userId);
    }
}
