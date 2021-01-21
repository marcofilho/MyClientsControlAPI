using System;

namespace UsersIO.Business.Models
{
    public class Phone : Entity
    {
        public Guid UserId { get; set; }
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }
        public User User { get; set; }
    }
}
