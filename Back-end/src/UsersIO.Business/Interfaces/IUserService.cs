using UsersIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace UsersIO.Business.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Remove(Guid id);
    }
}