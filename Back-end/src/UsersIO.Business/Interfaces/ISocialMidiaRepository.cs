using UsersIO.Business.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UsersIO.Business.Interfaces
{
    public interface ISocialMidiaRepository : IRepository<SocialMidia>
    {
        Task<List<SocialMidia>> FindSocialMidiasByUser(Guid userId);
    }
}
