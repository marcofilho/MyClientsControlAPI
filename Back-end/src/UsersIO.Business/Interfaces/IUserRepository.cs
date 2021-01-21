using UsersIO.Business.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UsersIO.Business.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUserAdresses(Guid id);
        Task<User> FindUserPhones(Guid id);
        Task<User> FindUserSocialMidias(Guid id);
        Task<User> FindUserComplete(Guid id);
        Task<List<User>> FindUsersComplete();

    }
}
