using System;

namespace UsersIO.Business.Models
{
    public class SocialMidia : Entity
    {
        public Guid UserId { get; set; }
        public string Value { get; set; }
        public SocialMidiaType SocialMidiaType { get; set; }
        public User User { get; set; }

    }
}
